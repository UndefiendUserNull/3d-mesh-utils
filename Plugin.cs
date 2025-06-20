#if TOOLS
using EIODE.Utils;
using Godot;

namespace EIODE.Addons;

[Tool]
public partial class Plugin : EditorPlugin
{
    private Control _dock;
    public const string MESH_UTILS_MAIN_DOCK_SCENE = "res://addons/3d-mesh-utils/MainDock.tscn";


    public override void _EnterTree()
    {
        _dock = ResourceLoader.Load<PackedScene>(MESH_UTILS_MAIN_DOCK_SCENE).Instantiate<Control>();
        AddControlToDock(DockSlot.LeftUr, _dock);
    }

    public override void _ExitTree()
    {
        RemoveControlFromDocks(_dock);
        QueueFree();
    }
}
#endif
