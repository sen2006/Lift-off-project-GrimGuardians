using GXPEngine;
using GXPEngine.Core;
using System;

public class Shootable : Sprite
{
    float health;
    float speed;

    int maxHealth;

    bool showHealthBar;

    EasyDraw healthBar;

    public Shootable(Texture2D texture, int startX, int startY, float speed, int health = 1, bool showHealthBar = true) : base (texture, true)
    {
        x = startX;
        y = startY;
        this.speed = speed;
        this.health = health;
        this.maxHealth = health;
        this.showHealthBar = showHealthBar;

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

    public float hit(int damage)
    {
        health = Math.Max(health - damage, 0);
        if (health <= 0) 
        {
            this.kill();
            this.pointReward(50);
        }
        return health;
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

