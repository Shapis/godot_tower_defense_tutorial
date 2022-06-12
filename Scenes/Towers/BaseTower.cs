using Godot;
using System;

public abstract partial class BaseTower : Node2D
{

    [Export] public float Range { get; private set; } = 600f;
    [Export] public float RateOfFire { get; private set; } = 1f;
    [Export] public float Damage { get; private set; } = 20f;


    public sealed override void _PhysicsProcess(float delta)
    {
        Turn();
    }


    private void Turn()
    {
        Vector2 enemy_position = GetGlobalMousePosition();
        GetNode<Sprite2D>("Turret").LookAt(enemy_position);
    }
}
