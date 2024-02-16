using GXPEngine;
using GXPEngine.Core;
using System;

public class Shootable : AnimationSprite
{
    float health;
    float speed;

    int maxHealth;

    int points;

    bool showHealthBar;

    EasyDraw healthBar;

    int overTimeDamageTimer;
    int damagePerSec;

    public Shootable(String texture, int startX, int startY, float speed, int health = 1, int points = 100, bool showHealthBar = true, int animationCols = 1, int animationRows = 1, int frames = -1) : base(texture, animationCols, animationRows, frames, false, true)
    {
        x = startX;
        y = startY;
        this.speed = speed;
        this.health = health;
        this.maxHealth = health;
        this.showHealthBar = showHealthBar;
        this.points = points;

        healthBar = new EasyDraw(this.width, 30);

    }

    void Update()
    {

        x += speed * Time.deltaTime / 60f;
        if (showHealthBar) { renderHealthBar(); }
        damageOverTime();
    }

    void renderHealthBar()
    {
        if (!this.game.HasChild(healthBar)) { this.game.AddChild(healthBar); }

        healthBar.SetXY(this.x, this.y - healthBar.height);
        healthBar.Fill(255, 0, 0);
        healthBar.Rect(0, 0, this.width * 2, healthBar.height);
        healthBar.Fill(0, 255, 0);
        healthBar.Rect(0, 0, this.width * 2 * (health / maxHealth), healthBar.height);
    }

    public float hit(float damage)
    {
        health = Math.Max(health - damage, 0);
        if (health <= 0)
        {
            kill();
            pointReward(points);
        }
        return health;
    }

    public void setOvertimeDamage(int damagePerSec = 0, int forSec = 0)
    {
        this.damagePerSec = damagePerSec;
        overTimeDamageTimer = forSec;
    }

    public void damageOverTime()
    {
        int deltaTime = Time.deltaTime / 1000;
        overTimeDamageTimer -= deltaTime;
        if (overTimeDamageTimer <= 0)
        {
            //hit(damagePerSec * Math.Min(deltaTime, overTimeDamageTimer));
            // this is causing a double point spawn, because its getting hit twice
            //uncomment and try shotting either buckshot or slug
        }
        //Console.WriteLine($"timer: {overTimeDamageTimer}");
    }

    public void kill()
    {
        Console.Write("kill\n");
        healthBar.LateDestroy();
        this.LateDestroy();
    }

    public void pointReward(int points)
    {
        Points rewardText = new Points(x, y, this.width, 50, 50);
        this.game.AddChild(rewardText);
        //Console.WriteLine("Point text spawned");
    }
}

