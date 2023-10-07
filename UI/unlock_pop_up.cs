using Godot;
using System;

public partial class unlock_pop_up : TextureRect
{
	private RichTextLabel techDescription;
	private TextureRect techThumbnail;
	private float ySize;
	private Timer timerTempsLeve;
    private Timer timerTempsHold;
    private Timer timerTempsBaisse;
	private bool playing;
	private State stateLocal;
    float posYInitiale;

    enum State {
		Up,Hold,Down
	}

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{

		techDescription = GetNode<RichTextLabel>("border/mainBackground/HBoxContainer/VBoxContainer/techDescription");
		techThumbnail = GetNode<TextureRect>("border / mainBackground / HBoxContainer / techThumbnail");
		ySize = GetNode<TextureRect>("border").Size.Y;
		timerTempsLeve = GetNode<Timer>("tempsLeve");
        timerTempsHold = GetNode<Timer>("tempsHold");
        timerTempsBaisse = GetNode<Timer>("tempsBaisse");
		posYInitiale = Position.Y + ySize;
		stateLocal = State.Hold;

		timerTempsLeve.Timeout += ToHold;
		timerTempsHold.Timeout += ToDown;
		timerTempsBaisse.Timeout += DontMove;
		Animation();

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(stateLocal == State.Up)
		{
			Position = new Vector2(Position.X,  - ((float)(timerTempsLeve.WaitTime - timerTempsLeve.TimeLeft) * ySize) + posYInitiale);
		}
		else
		{
			if(stateLocal == State.Down)
			{
                Position = new Vector2(Position.X,  (- ySize + (float)(timerTempsBaisse.WaitTime - timerTempsBaisse.TimeLeft) * ySize) + posYInitiale);
            }
		}
	}

	public void Animation()
	{
		
		ToUp();
	}

	private void ToUp()
	{
		stateLocal= State.Up;
        timerTempsLeve.Start();
    }
	private void ToDown()
	{
        stateLocal = State.Down;
        timerTempsBaisse.Start();
    }
	private void ToHold()
	{

        stateLocal = State.Hold;
        timerTempsHold.Start();
    }

	private void DontMove()
	{
		stateLocal = State.Hold;
	}

	public void ChangeText(string text)
	{
		techDescription.Text = text;
	}

	public void ChangeImage(Image image)
	{
		techThumbnail.Texture = ImageTexture.CreateFromImage(image);
	}
}
