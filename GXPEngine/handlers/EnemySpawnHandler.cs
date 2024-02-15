using GXPEngine;
using GXPEngine.Core;
using System;

public class EnemySpawnHandler : GameObject
{
    // TODO make this class
    
    // settings
    int minSpawnInterval = 800;
    int maxSpawnInterval = 2000;

    int smallSpawnWeight = 5;
    int mediumSpawnWeight = 3;
    int largeSpawnWeight = 2;

    long time;
    int currentSpawnInterval;
    Random random;
    public EnemySpawnHandler() {
        currentSpawnInterval = minSpawnInterval;
        random = new Random();
    }

    void Update()
    {
        if (MyGame.controlerHandler.isCalibrated())
        {
            time += Time.deltaTime;
            if (currentSpawnInterval <= 0)
            {
                spawn();
                currentSpawnInterval = (random.Next(maxSpawnInterval - minSpawnInterval)) + minSpawnInterval;
            }
            else
            {
                currentSpawnInterval -= Time.deltaTime;
            }
        }
    }

    void spawn()
    {
        int spawnWeight = random.Next(smallSpawnWeight + mediumSpawnWeight + largeSpawnWeight);
        MyGame game = MyGame.GetGame();
        if (spawnWeight < smallSpawnWeight)
        {
            Shootable shootable = new Shootable("assets/debug/circle.png", -100, random.Next(game.height - 400) + 200, 35, 1);
            game.AddChild(shootable);
            return;
        }
        spawnWeight -= smallSpawnWeight;

        if (spawnWeight < mediumSpawnWeight)
        {
            Shootable shootable = new Shootable("assets/debug/circle.png", -100, random.Next(game.height - 400) + 200, 25, 3);
            game.AddChild(shootable);
            return;
        }
        spawnWeight -= mediumSpawnWeight;

        if (spawnWeight < largeSpawnWeight)
        {
            Shootable shootable = new Shootable("assets/debug/circle.png", -100, random.Next(game.height - 400) + 200, 15, 5);
            game.AddChild(shootable);
            return;
        }
        throw new Exception("failed spawn");

    }
}

