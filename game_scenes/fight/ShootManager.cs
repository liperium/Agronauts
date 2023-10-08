using Godot;
using System;

public partial class ShootManager : Area2D
{
    [Export] public PackedScene potatoBulletPrefab;
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (@event is InputEventMouseButton)
        {
            if (((InputEventMouseButton)@event).ButtonIndex == MouseButton.Left && @event.IsPressed())
            {
                GD.Print("SHOOT!");
                TryShoot();
            }
        }
    }

    private bool CanShoot()
    {
        return true; //GameState.instance.numbers.cookedPotatoCount.GetValue() > 0;
    }
    
    private void TryShoot()
    {
        if (CanShoot())
        {
            //TODO Spawn bullet and set scale on nb potatoes
            Shoot();
            GD.Print("SPAWN BULLEt");
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

        return (float)(Math.Sqrt(Math.Max(nbPotats, 1)) * 0.5f);
    }
}
