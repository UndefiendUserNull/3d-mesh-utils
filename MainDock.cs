using Godot;

namespace EIODE.Addons;
#if TOOLS

[Tool]
public partial class MainDock : Control
{
    [Export] public Button button_changeAllTextures = null;
    [Export] public PackedScene _changeAllTexturesScene = null;
    private Control _instantiated_changeAllTextures = null;

    public override void _EnterTree()
    {
        button_changeAllTextures.Pressed += ChangeAllTexturesScenePressed;
    }

    public override void _ExitTree()
    {
        button_changeAllTextures.Pressed -= ChangeAllTexturesScenePressed;
    }

    private void ChangeAllTexturesScenePressed()
    {
        _instantiated_changeAllTextures = _changeAllTexturesScene.Instantiate<Control>();
        GetTree().Root.AddChild(_instantiated_changeAllTextures);
    }
}

#endif