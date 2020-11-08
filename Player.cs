using Godot;
using System;

public class Player : Area2D
{
	[Export]
	public int Speed = 400;
	private Vector2 _screenSize;

	public override void _Ready()
	{
		_screenSize = GetViewport().Size;
	}

	public override void _Process(float delta)
	{
		var velocity = new Vector2();
		
		velocity.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		velocity.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
		}
		
		Position += velocity * delta;
		//Position = new Vector2(
		//	x: Mathf.Clamp(Position.x, 0, _screenSize.x),
		//	y: Mathf.Clamp(Position.y, 0, _screenSize.y)
		//);
	}
}
