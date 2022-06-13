using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Linq;
public abstract partial class BaseEnemy : PathFollow2D
{
    [Export] public float Speed { get; private set; } = 100f;
    [Export] private float _health = 100f;
    [Export] private NodePath? _characterBody2DPath;
    private CharacterBody2D? _characterBody2D;
    private TextureProgressBar? _healthBar;
    private Vector2 _healthBarOffset = new Vector2(-30, 28);

    public sealed override void _Ready()
    {
        _characterBody2D = GetNode<CharacterBody2D>(_characterBody2DPath);
        _healthBar = GetChildren().FirstOrDefault(x => x is TextureProgressBar) as TextureProgressBar;
        if (_healthBar is null)
        {
            GD.PrintErr(this, "Health bar not found");
            return;
        }
        _healthBar.MaxValue = _health;
        _healthBar.Value = _health;
        _healthBar.TopLevel = true;
        __Ready();
    }

    protected virtual void __Ready()
    {

    }

    public sealed override void _PhysicsProcess(float delta)
    {
        Move(delta);
    }

    private void Move(float delta)
    {
        Offset += Speed * delta;
        _healthBar!.Position = GlobalPosition + _healthBarOffset;

    }

    public void OnHit(float Damage)
    {
        _health -= Damage;
        if (_healthBar is null)
        {
            GD.PrintErr(this, "Health bar not found");
            return;
        }
        _healthBar.Value = _health;
        __OnHit();
        if (_health <= 0)
        {
            Die();
        }
    }

    private async void Die()
    {
        if (_characterBody2D is null)
        {
            GD.PrintErr(this, "Character body not found");
            return;
        }
        _characterBody2D.QueueFree();
        await ToSignal(GetTree().CreateTimer(0.2f), "timeout");
        QueueFree();
    }

    protected virtual void __OnHit()
    {

    }
}