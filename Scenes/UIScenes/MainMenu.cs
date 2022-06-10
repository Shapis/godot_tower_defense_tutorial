using Godot;
using System;

public partial class MainMenu : Control
{
    [Signal] public event Action<Node>? OnNewGamePressed;
    [Signal] public event Action<Node>? OnSettingsPressed;
    [Signal] public event Action<Node>? OnShopPressed;
    [Signal] public event Action<Node>? OnAboutPressed;
    [Signal] public event Action<Node>? OnQuitPressed;

    public void OnBtnNewGamePressed()
    {
        OnNewGamePressed?.Invoke(this);
    }

    public void OnBtnSettingsPressed()
    {
        OnSettingsPressed?.Invoke(this);
    }

    public void OnBtnShopPressed()
    {
        OnShopPressed?.Invoke(this);
    }

    public void OnBtnAboutPressed()
    {
        OnAboutPressed?.Invoke(this);
    }

    public void OnBtnQuitPressed()
    {
        OnQuitPressed?.Invoke(this);
    }
}
