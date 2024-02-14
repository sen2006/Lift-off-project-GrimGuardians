using System;
using GXPEngine;
using GXPEngine.Core;

public class MyGame : Game
{
    public static ControlerHandler controlerHandler;

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