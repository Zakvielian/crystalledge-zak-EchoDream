using Content.Shared.Examine;
using Content.Shared.Hands.EntitySystems;
using Robust.Shared.Utility;

namespace Content.Shared._CE.Traits;

public sealed partial class ReligionTraitSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ReligionTraitComponent, ExaminedEvent>(OnExamine);
    }

    private void OnExamine(Entity<ReligionTraitComponent> ent, ref ExaminedEvent args)
    {
        var msg = FormattedMessage.FromMarkupOrThrow(Loc.GetString($"{ent.Comp.Text}"));
        msg.Pop();

        args.PushMessage(msg, -10);
    }
}
