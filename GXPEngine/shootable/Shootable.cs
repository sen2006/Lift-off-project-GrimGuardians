using GXPEngine;
using System;

public enum EnemyState
{
    Move,
    Attack,
    TakeDamage,
    Death
}

public class Shootable : GameObject
{
    Sprite enemyHealthBarFrame;
    Sprite enemyHealthBar;

    public float health;
    protected float speed;

    protected int maxHealth;
    protected int points;

    protected bool showHealthBar;

    protected EasyDraw healthBar;

    private int overTimeDamageTimer;
    private float damagePerSec;
    private int enemyDamage;
    private float enemyAttackSpeed;
    private float timeBetweenAttacks;
    private float enemyAttackTimer;

    int counter;
    int frame;

    private AnimationSprite currentAnimation = null;

    protected AnimationSprite moveAnimationSprite;
    protected AnimationSprite attackAnimationSprite;
    protected AnimationSprite takeDamageAnimationSprite;
    protected AnimationSprite deathAnimationSprite;

    private EnemyState enemyState;

    public Shootable(int startX, int startY, float speed, int health = 1, int enemyDamage = 1, float enemyAttackSpeed = 1, int points = 100, bool showHealthBar = true)
    {
        x = startX;
        y = startY;
        this.speed = speed;
        this.health = health;
        this.maxHealth = health;
        this.showHealthBar = showHealthBar;
        this.points = points;
        this.enemyDamage = enemyDamage;
        this.timeBetweenAttacks = 1f / enemyAttackSpeed;
        this.enemyAttackTimer = timeBetweenAttacks;

        enemyState = EnemyState.Move;


        scale = 0.3f;
        enemyHealthBarFrame = new Sprite("assets/sprites/UI/enemyFrame.png");
        enemyHealthBar = new Sprite("assets/sprites/UI/EhealthBar.png");
        //if (speed < 0) Mirror(true, false);

        enemyHealthBar = new Sprite("assets/sprites/UI/EhealthBar.png");
        healthBar = new EasyDraw(enemyHealthBar.width, 20);
    }

    public virtual void Update()
    {
        if (enemyState != EnemyState.Death)
        {
            if (enemyState == EnemyState.Move)
            {
                float deltaTime = Time.deltaTime / 1000f;
                enemyAttackTimer -= deltaTime;
                x += speed * Time.deltaTime / 60f;
            }

            damageOverTime();
            hitPlayer();
            checkForOffScreen();
        }
        playAnimation();
    }

    void checkForOffScreen()
    {
        if (speed > 0 && x > MyGame.GetGame().width + (moveAnimationSprite.width / 2f)) { this.LateDestroy(); }
        if (speed < 0 && x < -(moveAnimationSprite.width / 2f)) { this.LateDestroy();}
            enemyHealthBarFrame.LateDestroy();
            enemyHealthBar.LateDestroy();
    }

    public virtual void playAnimation()
    {
        AnimationSprite prevAnimation = currentAnimation;
        switch (enemyState)
        {
            case EnemyState.Move:
                currentAnimation = moveAnimationSprite;
                break;
            case EnemyState.Attack:
                currentAnimation = attackAnimationSprite;
                break;
            case EnemyState.TakeDamage:
                currentAnimation = takeDamageAnimationSprite;
                break;
            case EnemyState.Death:
                currentAnimation = deathAnimationSprite;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (currentAnimation != prevAnimation)
        {
            if (prevAnimation != null)
            {
                prevAnimation.visible = false;
            }

            currentAnimation.visible = true;
            counter = 0;
            frame = 0;
        }

        if (counter > 18)
        {
            counter = 0;
            if (frame == currentAnimation.frameCount)
            {
                if (enemyState == EnemyState.Death)
                {
                    this.kill();
                    pointReward(points);
                    return;
                }

                frame = 0;
                enemyState = EnemyState.Move;
            }
            currentAnimation.SetFrame(frame);
            frame++;
        }
        counter++;
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
        enemyState = EnemyState.TakeDamage;
        if (health <= 0)
        {
            enemyState = EnemyState.Death;
        }
        return health;
    }

    public virtual void hitPlayer()
    {
        if (enemyState == EnemyState.Move && enemyAttackTimer <= 0)
        {
            PlayerHealthHandler.takeDamage(enemyDamage);
            enemyAttackTimer = timeBetweenAttacks;
            enemyState = EnemyState.Attack;
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
        Points rewardText = new Points(x, y, 200, 50, points);
        this.game.AddChild(rewardText);
        //Console.WriteLine("Point text spawned");
    }
}

