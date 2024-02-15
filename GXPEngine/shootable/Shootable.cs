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

    int overTimeDamageTimer=0;
    int damagePerSec=0;

    public Shootable(String texture, int startX, int startY, float speed, int health = 1, int points = 100, bool showHealthBar = true, int animationCols = 1, int animationRows=1, int frames=-1) : base (texture, animationCols, animationRows, frames, false, true)
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
        x += speed * Time.deltaTime/60f;
        if (showHealthBar ) { renderHealthBar(); }
    }

    void renderHealthBar()
    {
        if (!this.game.HasChild(healthBar)) { this.game.AddChild(healthBar); }

        healthBar.SetXY(this.x, this.y - healthBar.height);
        healthBar.Fill(255,0,0);
        healthBar.Rect(0,0, this.width*2, healthBar.height);
        healthBar.Fill(0, 255, 0);
        healthBar.Rect(0,0, this.width*2*(health/maxHealth), healthBar.height);
    }

    public float hit(float damage)
    {
        health = Math.Max(health - damage, 0);
        if (health <= 0) 
        {
            this.kill();
            this.pointReward(points);
        }
        return health;
    }

    public void setOvertimeDamage(int damagePerSec = 0, int forSec = 0)
    {
        this.damagePerSec = damagePerSec;
        this.overTimeDamageTimer = forSec * 1000;
    }

    public void damageOverTime()
    {
        if (overTimeDamageTimer > 0)
        {
            int deltaTime = Time.deltaTime;
            hit(damagePerSec / 1000f * Math.Min(deltaTime, overTimeDamageTimer));
            overTimeDamageTimer -= deltaTime;
        }
    }

    public void kill()
    {
        healthBar.LateDestroy();
        this.LateDestroy();
    }

    public void pointReward(int points)
    {
        Points rewardText = new Points(x,y,this.width, 50, 50);
        this.game.AddChild(rewardText);
        //Console.WriteLine("Point text spawned");
    }
}

