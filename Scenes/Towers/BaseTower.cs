using Godot;
using System;

public abstract partial class BaseTower : Node2D
{
    public override void _PhysicsProcess(float delta)
    {
        Turn();
    }

    private void Turn()
    {
        Vector2 enemy_position = GetGlobalMousePosition();
        GetNode<Sprite2D>("Turret").LookAt(enemy_position);
    }
}
