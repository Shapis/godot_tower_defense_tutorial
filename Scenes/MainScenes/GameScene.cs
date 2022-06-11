using Godot;
using System;

public partial class GameScene : Node2D
{
    TileMap? _map;
    private bool _isBuildModeActive = false;
    private bool _isBuildValid = false;
    private Vector2 _buildLocation;
    private string? _buildType;

    public override void _Ready()
    {
        _map = GetNode<TileMap>("Map");



    }

    public override void _Process(float delta)
    {
        if (_isBuildModeActive)
        {
            UpdateTowerPreview();
        }

    }

    public override void _UnhandledInput(InputEvent inputEvent)
    {

        if (inputEvent.IsActionReleased("ui_cancel") && _isBuildModeActive)
        {
            GD.Print("here");
            CancelBuildMode();
        }
        if (inputEvent.IsActionReleased("ui_accept") && _isBuildModeActive)
        {
            // VerifyAndBuild();
            // CancelBuildMode();
        }
    }

    private void InitiateBuildMode(string towerType)
    {
        _buildType = towerType + "T1";
        _isBuildModeActive = true;
        GetNode<UI>("UI").SetTowerPreview(_buildType, GetGlobalMousePosition());

    }

    private void UpdateTowerPreview()
    {
        Vector2 mousePosition = GetGlobalMousePosition();

        if (_map is null)
        {
            GD.PrintErr("Map is null");
            return;
        }

        var currentTile = _map.WorldToMap(mousePosition);
        var tilePosition = _map.MapToWorld(currentTile);

        bool isCellATile = false;
        bool isCellBlocked = false;

        if (_map.GetCellSourceId(0, currentTile, true) != -1)
        {
            isCellATile = true;
        }
        for (int i = 1; i <= 3; i++)
        {
            if (_map.GetCellSourceId(i, currentTile, true) != -1)
            {
                isCellBlocked = true;
            }
        }

        if (!isCellATile)
        {
            GetNode<UI>("UI").UpdateTowerPreview(mousePosition, "ff2031b8");
            _isBuildValid = false;
        }
        else if (!isCellBlocked)
        {

            GetNode<UI>("UI").UpdateTowerPreview(tilePosition, "1eff0096");
            _isBuildValid = true;
            _buildLocation = tilePosition;
        }
        else
        {
            GetNode<UI>("UI").UpdateTowerPreview(tilePosition, "ff2031b8");
            _isBuildValid = false;
        }
    }

    private void CancelBuildMode()
    {
        _isBuildModeActive = false;
        _isBuildValid = false;
        GetNode<UI>("UI").RemoveDragTower();

    }

    private void VerifyAndBuild()
    {
        if (_isBuildValid)
        {
            Node2D newTower = GD.Load<Node2D>("res://Scenes/Towers/" + _buildType + ".tscn");
            newTower.Position = _buildLocation;
            _map!.AddChild(newTower, true);

        }
    }

    private void OnBuildBtnPressed(string nodeName)
    {
        InitiateBuildMode(nodeName);
    }
}