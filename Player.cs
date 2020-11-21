using Godot;
using System;

public class Player : KinematicBody2D
{
	[Export]
	public int Speed = 300;
	private Vector2 _screenSize;
	public AnimatedSprite animatedSprite;
	public AnimatedSprite staminaSprite;
	public Vector2 staminaFull;
	public Vector2 staminaEmpty;
	public Vector2 staminaCooldown;
	public Vector2 staminaDown;
	public Vector2 staminaUp;

	public override void _Ready()
	{
		_screenSize = GetViewport().Size;
		staminaFull = new Vector2(2, 2);
		staminaEmpty = new Vector2(1.6f, 1.6f);
		staminaCooldown = new Vector2(1.61f, 1.61f);
		staminaDown = new Vector2(0.001f, 0.001f);
		staminaUp = new Vector2(0.001f, 0.001f);
		animatedSprite = GetNode<AnimatedSprite>("PlayerSprite");
		staminaSprite = GetNode<AnimatedSprite>("StaminaSprite");
	}

	public override void _Process(float delta)
	{
		Speed = 100;
		var velocity = new Vector2();
		
		velocity.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		velocity.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");

		if (Input.GetActionStrength("ui_sprint") == 1 && staminaSprite.Scale > staminaEmpty && velocity.Length() != 0)
		{
			if (staminaSprite.Scale > staminaCooldown)
			{
				Speed = 1000;
				staminaSprite.Scale -= staminaDown;
			}
		}
		
		if (staminaSprite.Scale < staminaFull && Input.GetActionStrength("ui_sprint") == 0 || 
			staminaSprite.Scale < staminaFull && Input.GetActionStrength("ui_sprint") == 1 && velocity.Length() == 0)
		{
			staminaSprite.Scale += staminaUp;
		}

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite.Play();
		}
		else
		{
			animatedSprite.Stop();
		}
		
		if (velocity.x != 0)
		{
			animatedSprite.FlipH = velocity.x > 0;
		}
		
		velocity = MoveAndSlide(velocity, new Vector2(0, -1));
	}

	private void _on_Button_button_down()
	{
		var healthDown = new Vector2(0.1f,0.1f);
		var healthEmpty = new Vector2(1.1f, 1.1f);
		var healthSprite = GetNode<AnimatedSprite>("HealthSprite");
		var staminaSprite = GetNode<AnimatedSprite>("StaminaSprite");
		if (healthSprite.Scale > healthEmpty)
		{
			staminaSprite.Scale -= healthDown;
			healthSprite.Scale -= healthDown;
		}
	}
	
	private void _on_Button2_button_down()
	{
		var healthUp = new Vector2(0.1f, 0.1f);
		var healthFull = new Vector2(1.6f, 1.6f);
		var healthSprite = GetNode<AnimatedSprite>("HealthSprite");
		var staminaSprite = GetNode<AnimatedSprite>("StaminaSprite");
		if (healthSprite.Scale < healthFull)
		{
			staminaSprite.Scale += healthUp;
			healthSprite.Scale += healthUp;
		}
	}
}
