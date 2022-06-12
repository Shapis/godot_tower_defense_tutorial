using Godot;
using System;
using System.Runtime.CompilerServices;
public abstract partial class BaseEnemy : PathFollow2D
{
    [Export] public float Speed { get; private set; } = 100f;

    public sealed override void _Ready()
    {

    }

    public sealed override void _PhysicsProcess(float delta)
    {
        Move(delta);
    }

    private void Move(float delta)
    {
        Offset += Speed * delta;
    }
}