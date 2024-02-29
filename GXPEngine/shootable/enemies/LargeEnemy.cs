using GXPEngine;
using GXPEngine.Managers;

public class LargeEnemy : Shootable
{
    public LargeEnemy(int startX, int startY, float speed, int health = 1, int enemyDamage = 1, float enemyAttackSpeed = 1, int points = 100, bool showHealthBar = true) : base(startX, startY, speed, health, enemyDamage, enemyAttackSpeed, points, showHealthBar)
    {
        moveAnimationSprite = new AnimationSprite("assets/sprites/enemies/large_monster.png", 7, 1);
        AddChild(moveAnimationSprite);
        moveAnimationSprite.visible = false;

        attackAnimationSprite = new AnimationSprite("assets/sprites/enemies/large monster attack.png", 7, 1);
        AddChild(attackAnimationSprite);
        attackAnimationSprite.visible = false;

        takeDamageAnimationSprite = new AnimationSprite("assets/sprites/enemies/large monster damage.png", 4, 1);
        AddChild(takeDamageAnimationSprite);
        takeDamageAnimationSprite.visible = false;

        deathAnimationSprite = new AnimationSprite("assets/sprites/enemies/large monster death.png", 3, 1);
        AddChild(deathAnimationSprite);
        deathAnimationSprite.visible = false;
    }
    public override void Update()
    {
        base.Update();
        if (showHealthBar) { renderHealthBar(100, 40); }
    }

    public override void kill()
    {
        SoundHandler.large_death.play();
        UI_Handler.pointsMultiplier += 0.01f;
        base.kill();
    }
}