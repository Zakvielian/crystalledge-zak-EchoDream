using Content.Shared._CE.Health;
using Content.Shared._CE.Stamina;
using Content.Shared.Damage.Components;
using Content.Shared.EntityEffects.Effects.StatusEffects;
using Content.Shared.Examine;
using Content.Shared.FixedPoint;
using Content.Shared.Mobs;
using Content.Shared.Mobs.Components;
using Content.Shared.Mobs.Systems;
using Content.Shared.Movement.Systems;
using Content.Shared.Nutrition.Components;
using Content.Shared.Nutrition.EntitySystems;
using Robust.Shared.Utility;

namespace Content.Shared._CE.Traits;

public sealed partial class ThinnessTraitSystem : EntitySystem
{
    [Dependency] private CEStaminaSystem _staminaSystem = default!;
    [Dependency] private CEMobStateSystem _mobStateSystem = default!;
    [Dependency] private HungerSystem _hungerSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ThinnessTraitComponent, MapInitEvent>(OnMapInit);

        SubscribeLocalEvent<ThinnessTraitComponent, CECalculateMaxStaminaEvent>(OnCalculateMaxStaminaEvent);

        SubscribeLocalEvent<ThinnessTraitComponent, CECalculateMaxHealthEvent>(OnCalculateMaxHealth);

        SubscribeLocalEvent<ThinnessTraitComponent, ExaminedEvent>(OnExamine);
    }

    private void OnMapInit(Entity<ThinnessTraitComponent> ent, ref MapInitEvent args)
    {
        if (!TryComp<HungerComponent>(ent, out var hungerComponent))
            return;

        _hungerSystem.ChangeBaseDecayRate(ent, hungerComponent.BaseDecayRate * ent.Comp.HungerMod, hungerComponent);

        _mobStateSystem.RefreshMaxHealth(ent);

        _staminaSystem.RefreshMaxStamina(ent);
    }

    private void OnCalculateMaxStaminaEvent(Entity<ThinnessTraitComponent> ent, ref CECalculateMaxStaminaEvent args)
    {
        args.FlatModifier += ent.Comp.StaminaFlatMod;
    }

    private void OnExamine(Entity<ThinnessTraitComponent> ent, ref ExaminedEvent args)
    {
        FormattedMessage msg = new FormattedMessage();

        msg.PushColor(Color.FromHex("#e6ff07b1"));
        msg.AddText(Loc.GetString("trait-thinness-examine"));
        msg.Pop();

        args.PushMessage(msg, -10);
    }

    private void OnCalculateMaxHealth(Entity<ThinnessTraitComponent> ent, ref CECalculateMaxHealthEvent args)
    {
        args.FlatModifier += ent.Comp.HPChange;
    }
}
