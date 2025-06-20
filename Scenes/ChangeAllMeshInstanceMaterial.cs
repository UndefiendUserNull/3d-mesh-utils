using Godot;
using EIODE.Utils;

namespace EIODE.Addons;

#if TOOLS

[Tool]
public partial class ChangeAllMeshInstanceMaterial : Control
{
    [Export] public Button changeButton = null;
    [Export] public Button confirm = null;
    [Export] public TextureRect textureRect = null;
    [Export] public FileDialog fileDialog = null;

    private StandardMaterial3D _material = null;
    private Node _currentScene;

    public override void _Ready()
    {
        textureRect.Hide();
        fileDialog.Hide();

        changeButton.Pressed += ChangeButton_Pressed;
        confirm.Pressed += Confirm_Pressed;
        fileDialog.FileSelected += FileDialog_FileSelected;
    }
    public override void _ExitTree()
    {
        changeButton.Pressed -= ChangeButton_Pressed;
        confirm.Pressed -= Confirm_Pressed;
        fileDialog.FileSelected -= FileDialog_FileSelected;
    }

    public override void _Process(double delta)
    {
        if (Input.IsKeyPressed(Key.Escape))
        {
            QueueFree();
        }
    }

    private void Confirm_Pressed()
    {
        if (_material == null)
        {
            GD.PrintErr("Couldn't load material.");
            return;
        }

        _currentScene = EditorInterface.Singleton.GetEditedSceneRoot();
        var nodesFound = NodeUtils.GetAllChildren<Node>(_currentScene);

        GD.Print($"Children found {nodesFound.Count}.");

        foreach (var child in nodesFound)
        {
            if (child is MeshInstance3D childMesh)
            {
                childMesh.Mesh.SurfaceSetMaterial(0, _material);
                GD.Print($"Changed material for {childMesh.Name}.");
            }
        }



        QueueFree();
    }

    private void FileDialog_FileSelected(string path)
    {
        if (ResourceLoader.Exists(path))
        {
            _material = ResourceLoader.Load<StandardMaterial3D>(path);
            textureRect.Show();
            textureRect.Texture = _material.AlbedoTexture;
            GD.Print("Material loaded.");
        }
        else
            GD.PrintErr("Texture couldn't be loaded.");
    }

    private void ChangeButton_Pressed()
    {
        fileDialog.Show();
    }

}
#endif