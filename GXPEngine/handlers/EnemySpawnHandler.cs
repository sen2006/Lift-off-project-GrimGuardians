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
    int evenLargerSpawnWeight = 1;

    long time;
    int currentSpawnInterval;
    Random random;
    public EnemySpawnHandler() {
        currentSpawnInterval = minSpawnInterval;
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
        int spawnWeight = random.Next(smallSpawnWeight + mediumSpawnWeight + largeSpawnWeight + evenLargerSpawnWeight);
        MyGame game = MyGame.GetGame();
        if (spawnWeight < smallSpawnWeight)
        {
            SmallEnemy smallEnemy = new SmallEnemy(-250, random.Next(game.height - 500) + 200, 20, 1, 1, 5, 100, true, 8, 1);
            SoundHandler.small_sound.play();
            game.AddChild(smallEnemy);
            return;
        }
        spawnWeight -= smallSpawnWeight;

        if (spawnWeight < mediumSpawnWeight)
        {
            MediumEnemy mediumEnemy = new MediumEnemy(-250, random.Next(300, 300) + 200, 10, 3, 1, random.Next(5, 10), 200, true, 4, 1);
            SoundHandler.medium_sound.play();
            game.AddChild(mediumEnemy);
            return;
        }
        spawnWeight -= mediumSpawnWeight;

        if (spawnWeight < largeSpawnWeight)
        {
            LargeEnemy largeEnemy = new LargeEnemy(-350, random.Next(250, 350) + 200, 5, 5, 1, random.Next(5, 10), 400, true, 7, 1);
            SoundHandler.large_sound.play();
            game.AddChild(largeEnemy);
            return;
        }
        spawnWeight -= largeSpawnWeight;

        if (spawnWeight < evenLargerSpawnWeight)
        {
            BossEnemy bossEnemy = new BossEnemy(-500, random.Next(250, 350) + 200, 3, 12, 1, random.Next(5, 10), 2000, true, 4, 1);
            SoundHandler.boss_sound.play();
            game.AddChild(bossEnemy);
            return;
        }

        throw new Exception("failed spawn");
    }
}
