using Content.Shared._CE.Health;
using Content.Shared._CE.Health.Components;
using Content.Shared.Damage.Components;
using Content.Shared.EntityEffects.Effects.StatusEffects;
using Content.Shared.Examine;
using Content.Shared.FixedPoint;
using Content.Shared.Hands.EntitySystems;
using Content.Shared.Mobs;
using Content.Shared.Mobs.Components;
using Content.Shared.Mobs.Systems;
using Content.Shared.Movement.Systems;
using Content.Shared.Nutrition.Components;
using Content.Shared.Nutrition.EntitySystems;
using Robust.Shared.Utility;

namespace Content.Shared._CE.Traits;

public sealed partial class OverweightTraitSystem : EntitySystem
{
    [Dependency] private CEMobStateSystem _mobStateSystem = default!;
    [Dependency] private HungerSystem _hungerSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<OverweightTraitComponent, MapInitEvent>(OnMapInit);

        SubscribeLocalEvent<OverweightTraitComponent, RefreshMovementSpeedModifiersEvent>(OnRefreshMoveSpeed);

        SubscribeLocalEvent<OverweightTraitComponent, CECalculateMaxHealthEvent>(OnCalculateMaxHealth);

        SubscribeLocalEvent<OverweightTraitComponent, ExaminedEvent>(OnExamine);
    }

    private void OnMapInit(Entity<OverweightTraitComponent> ent, ref MapInitEvent args)
    {
        if (!TryComp<HungerComponent>(ent, out var hungerComponent))
            return;

        _hungerSystem.ChangeBaseDecayRate(ent, hungerComponent.BaseDecayRate * ent.Comp.HungerMod, hungerComponent);

        _mobStateSystem.RefreshMaxHealth(ent);
    }

    private void OnRefreshMoveSpeed(Entity<OverweightTraitComponent> ent, ref RefreshMovementSpeedModifiersEvent args)
    {
        args.ModifySpeed(ent.Comp.SpeedMod);
    }

    private void OnExamine(Entity<OverweightTraitComponent> ent, ref ExaminedEvent args)
    {
        FormattedMessage msg = new FormattedMessage();

        msg.PushColor(Color.FromHex("#fffb07aa"));
        msg.AddText(Loc.GetString("trait-overweight-examine"));
        msg.Pop();

        args.PushMessage(msg, -10);
    }

    private void OnCalculateMaxHealth(Entity<OverweightTraitComponent> ent, ref CECalculateMaxHealthEvent args)
    {
        args.FlatModifier += ent.Comp.HPChange;
    }
}
