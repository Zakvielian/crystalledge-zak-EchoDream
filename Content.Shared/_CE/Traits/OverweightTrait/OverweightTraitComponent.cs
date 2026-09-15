using Content.Shared._CE.Tag;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._CE.Traits;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class OverweightTraitComponent : Component
{
    [DataField, AutoNetworkedField]
    public float SpeedMod = 0.95f;

    [DataField, AutoNetworkedField]
    public int HPChange = 10;

    [DataField, AutoNetworkedField]
    public float HungerMod = 1.2f;
}
