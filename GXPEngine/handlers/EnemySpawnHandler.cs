using GXPEngine;
using GXPEngine.Core;
using System;

public class EnemySpawnHandler : GameObject
{
    // TODO make this different over time in a controlable way
    // settings
    int minSpawnInterval = 800;
    int maxSpawnInterval = 2000;

    int smallSpawnWeight = 5;
    int mediumSpawnWeight = 3;
    int largeSpawnWeight = 2;

    long time;
    int currentSpawnInterval;
    Random random;
    ControllerHandler controllerHandler;
    public EnemySpawnHandler(ControllerHandler controllerHandler) {
        currentSpawnInterval = minSpawnInterval;
        this.controllerHandler = controllerHandler;
        random = new Random();
    }

    void Update()
    {
        if (MyGame.GetControlerHandler().isCalibrated())
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
            SmallEnemy smallEnemy = new SmallEnemy("assets/debug/small_enemy_spider.png", -100, random.Next(game.height - 400) + 200, 25, controllerHandler , 1, 1, 5, 20, true, 8, 1);
            game.AddChild(smallEnemy);
            return;
        }
        spawnWeight -= smallSpawnWeight;

        if (spawnWeight < mediumSpawnWeight)
        {
            MediumEnemy shootable = new MediumEnemy("assets/debug/circle.png", -100, random.Next(game.height - 400) + 200, 15, controllerHandler, 3, 1, 5);
            game.AddChild(shootable);
            return;
        }
        spawnWeight -= mediumSpawnWeight;

        if (spawnWeight < largeSpawnWeight)
        {
            LargeEnemy LargeEnemy = new LargeEnemy("assets/debug/large_monster_slime.png", -100, random.Next(game.height - 400) + 200, 5, controllerHandler, 5, 1, 5, 100, true, 7, 1);
            game.AddChild(LargeEnemy);
            return;
        }

        //TO DO: implement boss enemy
        throw new Exception("failed spawn");

    }
}

