using System;
using System.Collections;
using GXPEngine;
using GXPEngine.Core;

public class MyGame : Game {
	public static ControlerHandler controlerHandler;

	public MyGame() : base(1366, 768, false, true, -1, -1, false)
	{
		Shootable[] shootables = {
		new Shootable(new Texture2D("assets/debug/circle.png"),100,100, 10, 5),
		new Shootable(new Texture2D("assets/debug/circle.png"),100,200, 10, 5),
		new Shootable(new Texture2D("assets/debug/circle.png"),100,300, 10, 5)
		};
		foreach (Shootable shootable in shootables) { AddChild(shootable); }
		

	}

    ControlerHandler controlHandler;
    Cursor cursor;
    public MyGame() : base(800, 600, false, true, -1, -1, false)
    {


        cursor = new Cursor(new Vector2(width / 2, height / 2));
        AddChild(cursor);
        cursor.SetOrigin(32, 32);


    }

    void Update()
    {
        controlerHandler.Update();

        cursor.SetXY(360 - controlerHandler.getYaw(), 360 - controlerHandler.getPitch());

    }

    static void Main()
    {
        controlerHandler = new ControlerHandler(true); // Initiate the controlerHandler
        new MyGame().Start(); // Create a "MyGame" and start it
    }
}