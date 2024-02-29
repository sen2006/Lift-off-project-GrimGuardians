using GXPEngine;
using System;

public class Shootable : AnimationSprite
{
    Sprite enemyHealthBarFrame;
    Sprite enemyHealthBar;
    
    public float health;
    protected float speed;

    protected int maxHealth;
    protected int points;

    protected bool showHealthBar;

    protected EasyDraw healthBar;

    int overTimeDamageTimer;
    float damagePerSec;
    int enemyDamage;
    float enemyAttackSpeed;
    int timeBetweenAttacks;

    int counter;
    int frame;

    public Shootable(String texture, int startX, int startY, float speed, int health = 1, int enemyDamage = 1, int enemyAttackSpeed = 1, int points = 100, bool showHealthBar = true, int animationCols = 1, int animationRows = 1, int frames = -1) : base(texture, animationCols, animationRows, frames, false, true)
    {
        x = startX;
        y = startY;
        this.speed = speed;
        this.health = health;
        this.maxHealth = health;
        this.showHealthBar = showHealthBar;
        this.points = points;
        this.enemyDamage = enemyDamage;
        this.enemyAttackSpeed = enemyAttackSpeed;
        this.timeBetweenAttacks = 5;
        healthBar = new EasyDraw(this.width, 20);

        scale = 0.3f;
        enemyHealthBarFrame = new Sprite("assets/sprites/UI/enemyFrame.png");
        enemyHealthBar = new Sprite("assets/sprites/UI/EhealthBar.png");
        if (speed < 0) Mirror(true, false);

    }

    public virtual void Update()
    {
        float deltaTime = Time.deltaTime / 1000f;
        enemyAttackSpeed -= deltaTime;
        x += speed * Time.deltaTime / 60f;
        damageOverTime();
        playAnimation(); 
        hitPlayer();

        checkForOffScreen();
    }

    void checkForOffScreen()
    {
        if (speed > 0 && x > MyGame.GetGame().width + (width / 2))
        {
            this.LateDestroy();
            enemyHealthBarFrame.LateDestroy();
            enemyHealthBar.LateDestroy();
        }
        if (speed < 0 && x < -(width / 2))
        {
            this.LateDestroy();
            enemyHealthBarFrame.LateDestroy();
            enemyHealthBar.LateDestroy();
        }
    }

    public virtual void playAnimation()
    {
        counter++;
        if (counter > 18)
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

    public virtual void renderHealthBar(int offSetX, int offSetY)
    {
        if (!this.game.HasChild(enemyHealthBarFrame)) { this.game.AddChild(enemyHealthBarFrame); }
        if (!this.game.HasChild(enemyHealthBar)) { this.game.AddChild(enemyHealthBar); }

        enemyHealthBarFrame.scale = 0.20f;
        enemyHealthBarFrame.SetXY(this.x + offSetX, this.y - offSetY);

        float healthFraction = health / maxHealth;
        enemyHealthBar.scaleX = Mathf.Max(0f, healthFraction * 0.2f);
        enemyHealthBar.scaleY = 0.20f;
        
        enemyHealthBar.SetXY(this.x + offSetX + 4, this.y - offSetY + 3);
    }

    public virtual float hit(float damage)
    {
        health = Math.Max(health - damage, 0);
        if (health <= 0)
        {
            this.kill();
            pointReward(points);
        }
        return health;
    }

    public virtual void hitPlayer()
    {
        if (enemyAttackSpeed <= 0)
        {
            PlayerHealthHandler.takeDamage(enemyDamage);
            enemyAttackSpeed = timeBetweenAttacks;
        }
    }

    public virtual void setOvertimeDamage(float damagePerSec, int forSec)
    {
        this.damagePerSec = damagePerSec;
        overTimeDamageTimer = forSec * 1000;
    }

    public virtual void damageOverTime()
    {
        int deltaTime = Time.deltaTime;
        if (health > 0 && overTimeDamageTimer > 0)
        {
            hit(damagePerSec * Math.Min(deltaTime, overTimeDamageTimer) / 1000f);
        }

        overTimeDamageTimer = Math.Max(overTimeDamageTimer - deltaTime, 0);
        //Console.WriteLine($"timer: {overTimeDamageTimer}");
    }

    public virtual void kill()
    {
        enemyHealthBarFrame.LateDestroy();
        enemyHealthBar.LateDestroy();
        this.LateDestroy();
        MyGame.GetControlerHandler().GetCursor().addkillCount();
        Console.WriteLine(" " + MyGame.GetControlerHandler().GetCursor().GetKillCount());

    }

    public virtual void pointReward(int points)
    {
        Points rewardText = new Points(x, y, this.width, 50, points);
        this.game.AddChild(rewardText);
        //Console.WriteLine("Point text spawned");
    }
}

