using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Linq;
public abstract partial class BaseEnemy : PathFollow2D
{
    [Export] public float Speed { get; private set; } = 100f;
    [Export] private float _health = 100f;
    private TextureProgressBar? _healthBar;
    private Vector2 _healthBarOffset = new Vector2(-30, 28);

    public sealed override void _Ready()
    {
        _healthBar = GetChildren().FirstOrDefault(x => x is TextureProgressBar) as TextureProgressBar;
        if (_healthBar is null)
        {
            GD.PrintErr(this, "Health bar not found");
            return;
        }
        _healthBar.MaxValue = _health;
        _healthBar.Value = _health;
        _healthBar.TopLevel = true;
    }

    public sealed override void _PhysicsProcess(float delta)
    {
        Move(delta);
    }

    private void Move(float delta)
    {
        Offset += Speed * delta;
        _healthBar.Position = GlobalPosition + _healthBarOffset;

    }

    public void OnHit(float Damage)
    {
        GD.Print(this.Name, this.GetType().Name, "here");
        _health -= Damage;
        if (_healthBar is null)
        {
            GD.PrintErr(this, "Health bar not found");
            return;
        }
        _healthBar.Value = _health;
        if (_health <= 0)
        {
            QueueFree();
        }
    }
}