using Godot;
using System;

public partial class BlueTank : BaseEnemy
{
    [Export] private PackedScene? _projectilePath;
    [Export] private NodePath? _projectileSpawnPointPath;
    private Position2D? _projectileSpawnPoint;
    private Random _rng = new Random();

    protected sealed override void __Ready()
    {
        _projectileSpawnPoint = GetNode<Position2D>(_projectileSpawnPointPath);
    }

    protected override void __OnHit()
    {
        Impact();
    }

    private void Impact()
    {
        if (_projectileSpawnPoint is null)
        {
            GD.PrintErr(this, "Projectile spawn point not found");
            return;
        }
        var newImpact = _projectilePath!.Instantiate<AnimatedSprite2D>();
        newImpact.Position = new Vector2(_rng.Next(-21, 15), _rng.Next(-20, 20));
        _projectileSpawnPoint.AddChild(newImpact);
    }
}
