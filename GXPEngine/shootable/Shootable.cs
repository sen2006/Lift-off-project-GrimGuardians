using GXPEngine;
using System;

public class Shootable : AnimationSprite
{
    float health;
    float speed;

    int maxHealth;
    int points;

    bool showHealthBar;

    EasyDraw healthBar;
    ControllerHandler controllerHandler;

    int overTimeDamageTimer;
    int damagePerSec;

    private int counter;
    private int frame;

    public Shootable(String texture, int startX, int startY, float speed, ControllerHandler controllerHandler,  int health = 1, int points = 100, bool showHealthBar = true, int animationCols = 1, int animationRows = 1, int frames = -1) : base(texture, animationCols, animationRows, frames, false, true)
    {
        x = startX;
        y = startY;
        this.speed = speed;
        this.health = health;
        this.maxHealth = health;
        this.showHealthBar = showHealthBar;
        this.points = points;
        this.controllerHandler = controllerHandler;
        healthBar = new EasyDraw(this.width, 20);

        scale = 0.3f;
    }

    void Update()
    {

        x += speed * Time.deltaTime / 60f;
        if (showHealthBar) { renderHealthBar(); }
        damageOverTime();

        counter++;
        if (counter > 10)
        {
            counter = 0;
            frame++;
            if (frame == frameCount)
            {
                frame = 0;
            }
            SetFrame(frame);
        }
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

    public void setOvertimeDamage(int damagePerSec, int forSec)
    {
        this.damagePerSec = damagePerSec;
        overTimeDamageTimer = forSec * 1000;
    }

    public void damageOverTime()
    {
        int deltaTime = Time.deltaTime;
        if (health > 0 && overTimeDamageTimer > 0)
        {
            hit(damagePerSec * Math.Min(deltaTime, overTimeDamageTimer) / 1000f);
        }

        overTimeDamageTimer = Math.Max(overTimeDamageTimer - deltaTime, 0);
        //Console.WriteLine($"timer: {overTimeDamageTimer}");
    }

    public void kill()
    {
        healthBar.LateDestroy();
        this.LateDestroy();
        MyGame.GetControlerHandler().GetCursor().addkill();
        Console.WriteLine(" " + MyGame.GetControlerHandler().GetCursor().GetKillCount());
    }

    public void pointReward(int points)
    {
        Points rewardText = new Points(x, y, this.width, 50, 50);
        this.game.AddChild(rewardText);
        //Console.WriteLine("Point text spawned");
    }
}

