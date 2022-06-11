using Godot;
using System;

public partial class UI : CanvasLayer
{
    [Export] private NodePath? _towerPreviewPath;
    private Control? _towerPreview;
    private Node2D? _dragTower;

    float _towerRange = 4;

    public override void _Ready()
    {
        _towerPreview = GetNode<Control>(_towerPreviewPath);

    }

    internal void SetTowerPreview(string towerType, Vector2 globalMousePosition)
    {
        _dragTower = GD.Load<PackedScene>("res://Scenes/Towers/" + towerType + ".tscn").Instantiate() as Node2D;

        if (_dragTower is null)
        {
            GD.PrintErr(this, "Failed to load tower");
            return;
        }

        _dragTower.Modulate = new Color("1eff0096");

        Sprite2D rangeTexture = new Sprite2D();
        float scaling = 600 / 600;
        rangeTexture.Scale = new Vector2(scaling, scaling);





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
