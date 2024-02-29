using GXPEngine;

public class BossEnemy : Shootable
{
    public BossEnemy(int startX, int startY, float speed, int health = 1, int enemyDamage = 1, float enemyAttackSpeed = 1, int points = 100, bool showHealthBar = true) : base(startX, startY, speed, health, enemyDamage, enemyAttackSpeed, points, showHealthBar)
    {
        moveAnimationSprite = new AnimationSprite("assets/sprites/enemies/boss_enemy.png", 4, 1);
        AddChild(moveAnimationSprite);
        moveAnimationSprite.visible = false;

        attackAnimationSprite = new AnimationSprite("assets/sprites/enemies/boss attack sprite.png", 4, 1);
        AddChild(attackAnimationSprite);
        attackAnimationSprite.visible = false;

        takeDamageAnimationSprite = new AnimationSprite("assets/sprites/enemies/boss damage sprite.png", 4, 1);
        AddChild(takeDamageAnimationSprite);
        takeDamageAnimationSprite.visible = false;

        deathAnimationSprite = new AnimationSprite("assets/sprites/enemies/boss death sprite.png", 4, 1);
        AddChild(deathAnimationSprite);
        deathAnimationSprite.visible = false;
    }
    public override void Update()
    {
        base.Update();
        if (showHealthBar) { renderHealthBar(235, 20); }
    }

    public override void kill()
    {
        SoundHandler.boss_death.play();
        UI_Handler.pointsMultiplier += 0.01f;
        base.kill();
    }
}