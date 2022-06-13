using Godot;
using System;

public partial class GunT1 : BaseTower
{
    protected override void Shoot(BaseEnemy target)
    {
        target.OnHit(Damage);
        AnimationPlayer?.Play("Fire");
    }
}