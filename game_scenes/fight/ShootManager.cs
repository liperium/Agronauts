using Godot;
using System;

public partial class ShootManager : Node2D
{
    [Export] public PackedScene potatoBulletPrefab;
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (@event is InputEventMouseButton)
        {
            if (((InputEventMouseButton)@event).ButtonIndex == MouseButton.Left && @event.IsPressed())
            {
                TryShoot();
            }
        }
    }

    private bool CanShoot()
    {
        return FightManager.instance.IsPaused() == false; //GameState.instance.numbers.cookedPotatoCount.GetValue() > 0;
    }
    
    private void TryShoot()
    {
        if (CanShoot())
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (potatoBulletPrefab.Instantiate() is PotatoBullet bulletInstance)
        {
            float scale = GetBulletScale();
            bulletInstance.SetScale(new Vector2(scale, scale));
            ObjectSpawner.Spawn(bulletInstance, GetViewport().GetMousePosition());
        }
        else
        {
            GD.PrintErr("NULL ALIEN SPAWN! WTF");
        }
    }

    private float GetBulletScale()
    {
        long nbPotats = GameState.instance.numbers.cookedPotatoCount.GetValue();

        return Math.Clamp((float)(Math.Log10(nbPotats+1) * 0.5f),0.5f, 4f);
    }
}
