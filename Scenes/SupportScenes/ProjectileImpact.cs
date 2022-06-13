using Godot;
using System;

public partial class ProjectileImpact : AnimatedSprite2D
{
    // Called when the node enters the scene tree for the first time.
    public sealed override void _Ready()
    {
        Playing = true;
    }

    private void OnProjectileImpactAnimationFinished()
    {
        QueueFree();
        GD.Print(this.Name, this.GetType().Name, "here");
    }
}
