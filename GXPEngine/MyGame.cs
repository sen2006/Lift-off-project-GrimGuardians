using System;
using System.Collections;
using System.Collections.Generic;
using GXPEngine;
using GXPEngine.Core;

public class MyGame : Game {
    static MyGame game;
    public static MyGame GetGame() { return game; }

    public static ControlerHandler controlerHandler;

    public MyGame() : base(1366, 768, false, true, -1, -1, false)
	{
        AmmoTypeHandler.initializeClass();
		Shootable[] shootables = {
		new Shootable(new Texture2D("assets/debug/circle.png"),100,200, 10, 5),
		new Shootable(new Texture2D("assets/debug/circle.png"),100,300, 10, 5),
		new Shootable(new Texture2D("assets/debug/circle.png"),100,400, 10, 5)
		};
		foreach (Shootable shootable in shootables) { AddChild(shootable); }
        AddChild(controlerHandler);

    }
    
    void Update()
    {
        controlerHandler.Update();
    }

    static void Main()
    {
        controlerHandler = new ControlerHandler(); // Initiate the controlerHandler
        game = new MyGame();
        game.Start();
    }
}