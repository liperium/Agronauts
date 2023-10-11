using Godot;
using System;
using System.Data;

public partial class Tracteur : CharacterBody2D
{
	private float speed = 1000f;

	private Vector2 velocity = Vector2.Zero;

	private float rotationForce = 0.0f;

	public bool automatic = false;
	public Vector2 topLeftBound;
	public Vector2 bottomRightBound;
    private float step = -1;
    private int directionH = -1;
    private int directionV = 1;
    private int row = 0;
    private AutoState state = AutoState.MOVE_VERTICAL;
    private bool enabled = true;

    public float RotationSpeed { get => speed / 30f; }

    private Epandeuse epandeuse;

    public enum AutoState
    {
        SPIN_CHANGE_ROW,
        CHANGE_ROW,
        SPIN_BACK_VERTICAL,
        MOVE_VERTICAL,
    }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        RotationDegrees = 0;
        UpdateSpeed(GameState.instance.numbers.truckSpeed.GetValue());
        GameState.instance.numbers.truckSpeed.SetOnValueChanged(UpdateSpeed);
        epandeuse = GetNode<Epandeuse>("Epandeuse");
        if (automatic)
        {
            epandeuse.Hide();
            epandeuse.SetCollision(false);
        }
        else
        {
            HideTractor(HideManualTractor.IsHidden());
            HideManualTractor.SetOnHideCheck(HideTractor);
        }
        if (GameState.instance.upgrades.tractorSpreadSeedsUpgrade.acquired)
        {
            UpgradeEpandeur();
        }
        else
        {
            GameState.instance.upgrades.tractorSpreadSeedsUpgrade.SetOnBuyUpgrade(UpgradeEpandeur);
        }

        Timer timer = GetNode<Timer>("SoundTimer");
        timer.WaitTime = new Random().NextDouble()*3.0;
        AudioStreamPlayer2D streamPlayer2D = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        timer.Timeout += () =>
        {
            streamPlayer2D.Play();
            timer.QueueFree();
        };
    }



    public void UpdateSpeed(long speed)
    {
        this.speed = speed;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public override void _PhysicsProcess(double delta)
	{
        if (automatic)
        {
            AutoProcess(delta);
        }
        else
        {
            ManualProcess(delta);
        }
    }

    public void AutoProcess(double delta)
    {
        if(!enabled) return;
        if (step == -1) step = Mathf.Abs((bottomRightBound.X - topLeftBound.X)/FarmFieldMaster.TILE_PER_FF);
        switch (state)
        {
            case AutoState.SPIN_CHANGE_ROW:
                rotationForce =  directionV*directionH * RotationSpeed * (float)delta;
                Rotate(rotationForce);
                if(Mathf.Abs((Mathf.Abs(RotationDegrees) % 180) - 90) < 1f * speed / 40f)
                {
                    row += directionH;
                    state = AutoState.CHANGE_ROW;
                }
                break;
            case AutoState.CHANGE_ROW:
                velocity = speed * (float)delta * new Vector2(directionH, 0);
                MoveAndCollide(velocity);
                if(directionH < 0)
                {
                    if(Position.X <= topLeftBound.X + row * step)
                    {
                        Position = new Vector2(topLeftBound.X + row * step, Position.Y);
                        state = AutoState.SPIN_BACK_VERTICAL;
                    }
                }
                else
                {
                    if (Position.X >= topLeftBound.X + row * step)
                    {
                        Position = new Vector2(topLeftBound.X + row * step, Position.Y);
                        state = AutoState.SPIN_BACK_VERTICAL;
                    }
                }
                break;
            case AutoState.SPIN_BACK_VERTICAL:
                rotationForce = directionV * directionH * RotationSpeed * (float)delta;
                Rotate(rotationForce);
                
                if (Mathf.Abs((Mathf.Abs(RotationDegrees) % 180)) < 1f * speed / 40f)
                {
                    state = AutoState.MOVE_VERTICAL;
                }
                break;
            case AutoState.MOVE_VERTICAL:
                velocity = speed * (float)delta * new Vector2(0, directionV);
                MoveAndCollide(velocity);
                if (IsOutsideBounds())
                {
                    if (Position.X + step > bottomRightBound.X - step || Position.X - step < topLeftBound.X)
                    {
                        directionH = -directionH;
                    }
                    directionV = -directionV;
                    state = AutoState.SPIN_CHANGE_ROW;
                }
              
                break;
        }
        

    }

    private bool IsOutsideBounds()
    {
        return Position.Y < topLeftBound.Y || Position.Y > bottomRightBound.Y - step;
    }

	public void ManualProcess(double delta)
    {

        if (!enabled) return;
        
        if (Input.IsActionPressed("tracteur_front") || Input.IsActionPressed("tracteur_back"))
        {
            float front_back = Input.GetActionStrength("tracteur_front") - Input.GetActionStrength("tracteur_back");
            velocity = front_back * speed * 2 * Transform.Y * (float)delta;
            rotationForce = (Input.GetActionStrength("tracteur_right") - Input.GetActionStrength("tracteur_left")) * RotationSpeed * 3 * (float)delta;
            if (front_back >= 0)
            {
                Rotate(rotationForce);
            }
            else
            {
                Rotate(-rotationForce);
            }

            MoveAndCollide(velocity);
        }
        else if (Input.IsActionPressed("tracteur_right") || Input.IsActionPressed("tracteur_left"))
        {
            rotationForce = (Input.GetActionStrength("tracteur_right") - Input.GetActionStrength("tracteur_left")) * RotationSpeed * 3 * (float)delta;
            Rotate(rotationForce);
        }
    }

    public void UpgradeEpandeur()
    {
        epandeuse.Show();
        epandeuse.SetCollision(true);
        GameState.instance.upgrades.tractorSpreadSeedsUpgrade.ResetOnBuyUpgrade(UpgradeEpandeur);

    }

    public void HideTractor(bool hide)
    {
        if (hide)
        {
            Hide();
            enabled = false;
        }
        else
        {
            Show();
            enabled = true;
        }
    }

    public override void _ExitTree()
    {
        HideManualTractor.ResetOnHideCheck(HideTractor);
    }
}
