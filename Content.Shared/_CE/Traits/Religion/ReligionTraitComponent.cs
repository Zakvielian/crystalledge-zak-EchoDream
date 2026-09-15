using Robust.Shared.GameStates;

namespace Content.Shared._CE.Traits;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class ReligionTraitComponent : Component
{
    [DataField, AutoNetworkedField]
    public string Text = string.Empty;
}
