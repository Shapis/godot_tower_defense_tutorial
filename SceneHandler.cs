using Godot;
using System;

public partial class SceneHandler : Node
{
    private MainMenu? _mainMenu;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _mainMenu = GetNode<MainMenu>("MainMenu");
        _mainMenu.OnNewGamePressed += OnNewGamePressed;
        _mainMenu.OnSettingsPressed += OnSettingsPressed;
        _mainMenu.OnShopPressed += OnShopPressed;
        _mainMenu.OnAboutPressed += OnAboutPressed;
        _mainMenu.OnQuitPressed += OnQuitPressed;

    }

    public void OnNewGamePressed(Node sender)
    {
        _mainMenu?.QueueFree();
        var gameScene = GD.Load<PackedScene>("res://Scenes/MainScenes/GameScene.tscn").Instantiate();
        AddChild(gameScene);
    }


    private void OnSettingsPressed(Node obj)
    {

    }


    private void OnShopPressed(Node sender)
    {

    }

    private void OnAboutPressed(Node sender)
    {

    }

    private void OnQuitPressed(Node sender)
    {
        GetTree().Quit();
    }
}