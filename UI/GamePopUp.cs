using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class GamePopUp : Control
{
	private Label techDescription;
    private Label techTitle;
    private TextureRect techThumbnail;
	private float ySize;
	private Timer timerTempsLeve;
    private Timer timerTempsHold;
    private Timer timerTempsBaisse;
	private bool playing;
	private State stateLocal;
    float targetPosY;
	public static GamePopUp instance;
	private static Queue<GamePopUpInfo> queue = new Queue<GamePopUpInfo>();
	const int CANVAS_HEIGHT = 1080;

    enum State {
		Up,Hold,Down,Ready
	}

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		
		techDescription = GetNode<Label>("HBoxContainer/VBoxContainer/techDescription");
        techTitle = GetNode<Label>("HBoxContainer/VBoxContainer/techTitle");
        techThumbnail = GetNode<TextureRect>("HBoxContainer/techThumbnail");
		timerTempsLeve = GetNode<Timer>("tempsLeve");
        timerTempsHold = GetNode<Timer>("tempsHold");
        timerTempsBaisse = GetNode<Timer>("tempsBaisse");
		stateLocal = State.Ready;

		timerTempsLeve.Timeout += ToHold;
		timerTempsHold.Timeout += ToDown;
		timerTempsBaisse.Timeout += DontMove;
		instance = this;
		ContentChanged();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(stateLocal == State.Up)
		{
			Position = new Vector2(Position.X,  - ((float)(timerTempsLeve.WaitTime - timerTempsLeve.TimeLeft) * ySize) + targetPosY);
		}
		else
		{
			if(stateLocal == State.Down)
			{
                Position = new Vector2(Position.X,  (- ySize + (float)(timerTempsBaisse.WaitTime - timerTempsBaisse.TimeLeft) * ySize) + targetPosY);
            }
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (queue.Count > 0 && stateLocal == State.Ready)
		{
			GamePopUpInfo toShow = queue.Dequeue();
			ChangeToInfo(toShow);
		}
	}

	public void AddToQueue(GamePopUpInfo info)
	{
		queue.Enqueue(info);
	}

	private void StartAnimation()
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
		stateLocal = State.Ready;
	}

	public void ChangeText(string title, string description)//TODO remove
	{
		techTitle.Text = title;
		techDescription.Text = description;
	}

	public void ChangeToInfo(GamePopUpInfo info)
	{
		techTitle.Text = info.Title;
		techDescription.Text = info.Description;

		if (info.Image != null)
		{
			techThumbnail.Texture = info.Image;
			techThumbnail.Visible = true;
		}
		else
		{
			techThumbnail.Visible = false;
		}
		ContentChanged();
		StartAnimation();
	}
	private void ContentChanged()
	{
		ySize = Size.Y;
		targetPosY = CANVAS_HEIGHT;
		Position = new Vector2(Position.X, targetPosY+ySize);
	}
}
