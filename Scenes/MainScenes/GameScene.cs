using Godot;
using System;

public partial class GameScene : Node2D
{
    TileMap? _map;
    private bool _isBuildModeActive = false;
    private bool _isBuildValid = false;
    private Vector2 _buildLocation;
    private Vector2i _buildTile;
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
            CancelBuildMode();
        }
        if (inputEvent.IsActionReleased("ui_accept") && _isBuildModeActive)
        {
            VerifyAndBuild();
            CancelBuildMode();
        }
    }

    private void InitiateBuildMode(string towerType)
    {
        if (_isBuildModeActive)
        {
            CancelBuildMode();
        }
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

        bool isCellBlocked = false;
        for (int i = 1; i <= 3; i++)
        {
            if (_map.GetCellSourceId(i, currentTile, true) != -1)
            {
                isCellBlocked = true;
            }
        }

        if (!isCellBlocked)
        {
            _buildTile = currentTile;
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
            var newTower = GD.Load<PackedScene>("res://Scenes/Towers/" + _buildType + ".tscn").Instantiate() as Node2D;
            if (newTower is null)
            {
                GD.PrintErr("Failed to load tower");
                return;
            }
            newTower.Position = _buildLocation;
            _map!.AddChild(newTower, true);
            _map!.SetCell(3, _buildTile, 1, new Vector2i(0, 0));
        }
    }

    private void OnBuildBtnPressed(string nodeName)
    {
        InitiateBuildMode(nodeName);
    }
}