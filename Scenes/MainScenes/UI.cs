using Godot;
using System;

public partial class UI : CanvasLayer
{
    [Export] private NodePath? _towerPreviewPath;
    private Control? _towerPreview;
    private Node2D? _dragTower;

    public override void _Ready()
    {
        _towerPreview = GetNode<Control>(_towerPreviewPath);

    }

    internal void SetTowerPreview(string towerType, Vector2 globalMousePosition)
    {
        _dragTower = GD.Load<PackedScene>("res://Scenes/Towers/" + towerType + ".tscn").Instantiate() as Node2D;

        if (_dragTower is null)
        {
            GD.PrintErr("Failed to load tower");
            return;
        }

        _dragTower.Modulate = new Color("1eff0096");
        _towerPreview!.AddChild(_dragTower, true);
        _towerPreview.Position = globalMousePosition;
    }

    internal void RemoveDragTower()
    {
        _towerPreview!.RemoveChild(_dragTower);
    }

    internal void UpdateTowerPreview(Vector2 tilePosition, string colorHex)
    {
        _towerPreview!.Position = tilePosition;

        if (_dragTower is null)
        {
            GD.PrintErr("Drag tower is null");
            return;
        }
        if (_dragTower.Modulate != new Color(colorHex))
        {
            _dragTower.Modulate = new Color(colorHex);
        }
    }
}
