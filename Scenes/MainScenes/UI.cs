using Godot;
using System;

public partial class UI : CanvasLayer
{
    [Export] private NodePath? _towerPreviewPath;
    private Control? _towerPreview;
    [Export] private Texture2D? _rangeOverlayTexture;
    private BaseTower? _dragTower;
    private Sprite2D? _rangeTexture;

    public override void _Ready()
    {
        _towerPreview = GetNode<Control>(_towerPreviewPath);

    }

    internal void SetTowerPreview(string towerType, Vector2 globalMousePosition)
    {
        _dragTower = GD.Load<PackedScene>("res://Scenes/Towers/" + towerType + ".tscn").Instantiate() as BaseTower;

        if (_dragTower is null)
        {
            GD.PrintErr(this, "Failed to load tower");
            return;
        }

        _dragTower.Modulate = new Color("1eff0096");

        _rangeTexture = new Sprite2D();
        float scaling = _dragTower.Range / 600f;
        _rangeTexture.Scale = new Vector2(scaling, scaling);
        _rangeTexture.Texture = _rangeOverlayTexture;
        _rangeTexture.Modulate = new Color("1eff0096");

        if (_towerPreview is null)
        {
            GD.PrintErr(this, "Tower preview is null");
            return;
        }

        _towerPreview.AddChild(_rangeTexture);
        _towerPreview.Position = globalMousePosition;
        _towerPreview.AddChild(_dragTower, true);
    }

    internal void RemoveDragTower()
    {
        if (_towerPreview is null)
        {
            GD.PrintErr(this, "Tower preview is null");
            return;
        }
        _towerPreview.RemoveChild(_dragTower);
        _towerPreview.RemoveChild(_rangeTexture);
    }

    internal void UpdateTowerPreview(Vector2 tilePosition, string colorHex)
    {
        _towerPreview!.Position = tilePosition;

        if (_dragTower is null)
        {
            GD.PrintErr(this, "Drag tower is null");
            return;
        }
        if (_dragTower.Modulate != new Color(colorHex))
        {
            _dragTower.Modulate = new Color(colorHex);
            if (_rangeTexture is null)
            {
                GD.PrintErr(this, "Range texture is null");
                return;
            }
            _rangeTexture.Modulate = new Color(colorHex);
        }
    }
}
