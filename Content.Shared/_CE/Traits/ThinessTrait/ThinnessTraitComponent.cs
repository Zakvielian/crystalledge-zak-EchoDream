using Content.Shared._CE.Tag;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._CE.Traits;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class ThinnessTraitComponent : Component
{
    [DataField, AutoNetworkedField]
    public float StaminaFlatMod = 5f;

    [DataField, AutoNetworkedField]
    public int HPChange = -5;

    [DataField, AutoNetworkedField]
    public float HungerMod = 0.8f;
}
