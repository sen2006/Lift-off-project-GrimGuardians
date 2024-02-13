using System;
using GXPEngine;

public class MyGame : Game {
	public static ControlerHandler controlerHandler;
	public MyGame() : base(800, 600, false, true, -1, -1, false) {
	}

	static void Main() {
		controlerHandler = new ControlerHandler(false); // Initiate the controlerHandler
        new MyGame().Start(); // Create a "MyGame" and start it
    }
}