using GXPEngine;
using GXPEngine.Core;
using System;
using System.Diagnostics;

public class EnemySpawnHandler : GameObject
{
    // TODO make this different over time in a controlable way
    // settings
    static int minSpawnInterval = 900;
    static int maxSpawnInterval = 4000;

    float smallSpawnWeight = 5;
    float mediumSpawnWeight = 3;
    float largeSpawnWeight = 2;
    float BossSpawnWeight = 1;

    static int spawnIncreaseInterval = 20000;

    static float smallSpawnWeightIncrease = .1f;
    static float mediumSpawnWeightIncrease = .5f;
    static float largeSpawnWeightIncrease = 1f;
    static float BossSpawnWeightIncrease = .5f;

    static int maxSpawnIntervalDecrease = 1;

    // variables

    int panoramaSpeed;

    
    int currentSpawnInterval;
    int spawnIncreaseTimer = spawnIncreaseInterval;
    Random random;
    public EnemySpawnHandler()
    {
        currentSpawnInterval = minSpawnInterval;
        random = new Random();
    }

    void Update()
    {
        if (MyGame.GetControlerHandler().isCalibrated() && MyGame.IsRunning())
        {
            panoramaSpeed = MyGame.GetPanoramaHandler().getSpeed();
            // try spawnind when time hits 0
            if (currentSpawnInterval <= 0)
            {
                spawn();
                currentSpawnInterval = (random.Next(maxSpawnInterval - minSpawnInterval)) + minSpawnInterval; // reset timer to a random number
            }
            else
            {
                currentSpawnInterval -= Time.deltaTime;
            }

            // increase the spawn interval
            if (spawnIncreaseTimer <= 0)
            {
                smallSpawnWeight += smallSpawnWeightIncrease;
                mediumSpawnWeight += mediumSpawnWeightIncrease;
                largeSpawnWeight += largeSpawnWeightIncrease;
                BossSpawnWeight += BossSpawnWeightIncrease;

                maxSpawnInterval = Math.Max(maxSpawnInterval - maxSpawnIntervalDecrease, minSpawnInterval+200);

                Console.WriteLine("new spanwrates (small)" + smallSpawnWeight + " (med)" + mediumSpawnWeight + " (large)" + largeSpawnWeight + " (boss)" + BossSpawnWeight);

                spawnIncreaseTimer = spawnIncreaseInterval;
            }
            else
            {
                spawnIncreaseTimer -= Time.deltaTime;
            }
        }
    }

    void spawn()
    {
        float spawnWeight = (float)random.NextDouble() * (smallSpawnWeight + mediumSpawnWeight + largeSpawnWeight + BossSpawnWeight);
        MyGame game = MyGame.GetGame();
        int speedMultiplier = panoramaSpeed > 0 ? 1 : -1;
        if (spawnWeight < smallSpawnWeight)
        {
            SmallEnemy smallEnemy = new SmallEnemy(panoramaSpeed > 0 ? -250 : MyGame.GetGame().width+250, random.Next(game.height - 500) + 200, 20 * speedMultiplier, 1, 1, 5, 100, true);
            SoundHandler.small_sound.play();
            game.AddChild(smallEnemy);
            return;
        }
        spawnWeight -= smallSpawnWeight;

        if (spawnWeight < mediumSpawnWeight)
        {
            MediumEnemy mediumEnemy = new MediumEnemy(panoramaSpeed > 0 ? -250 : MyGame.GetGame().width+250, random.Next(300, 300) + 200, 10 * speedMultiplier, 3, 1, 0.5f, 200, true);
            SoundHandler.medium_sound.play();
            game.AddChild(mediumEnemy);
            return;
        }
        spawnWeight -= mediumSpawnWeight;

        if (spawnWeight < largeSpawnWeight)
        {
            LargeEnemy largeEnemy = new LargeEnemy(panoramaSpeed > 0 ? -350 : MyGame.GetGame().width+350, random.Next(250, 350) + 200, 5 * speedMultiplier, 5, 1, 0.5f, 400, true);
            SoundHandler.large_sound.play();
            game.AddChild(largeEnemy);
            return;
        }
        spawnWeight -= largeSpawnWeight;

        if (spawnWeight < BossSpawnWeight)
        {
            BossEnemy bossEnemy = new BossEnemy(panoramaSpeed > 0 ? -500 : MyGame.GetGame().width + 500, random.Next(250, 350) + 200, 3 * speedMultiplier, 12, 1, 0.5f, 2000, true);
            SoundHandler.boss_sound.play();
            game.AddChild(bossEnemy);
            return;
        }

        throw new Exception("failed spawn");
    }
}
