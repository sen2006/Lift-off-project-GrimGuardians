using GXPEngine;
using GXPEngine.Managers;

public class MediumEnemy : Shootable
{
    public MediumEnemy(int startX, int startY, float speed, int health = 1, int enemyDamage = 1, float enemyAttackSpeed = 1,  int points = 100, bool showHealthBar = true) : base(startX, startY, speed, health, enemyDamage, enemyAttackSpeed, points, showHealthBar)
    {
        moveAnimationSprite = new AnimationSprite("assets/sprites/enemies/medium_enemy.png", 4, 1);
        AddChild(moveAnimationSprite);
        moveAnimationSprite.visible = false;

        attackAnimationSprite = new AnimationSprite("assets/sprites/enemies/medium enemy attack.png", 4, 1);
        AddChild(attackAnimationSprite);
        attackAnimationSprite.visible = false;

        takeDamageAnimationSprite = new AnimationSprite("assets/sprites/enemies/medium enemy damage.png", 4, 1);
        AddChild(takeDamageAnimationSprite);
        takeDamageAnimationSprite.visible = false;

        deathAnimationSprite = new AnimationSprite("assets/sprites/enemies/medium enemy death.png", 4, 1);
        AddChild(deathAnimationSprite);
        deathAnimationSprite.visible = false;
    }
    public override void Update()
    {
        base.Update();
        if (showHealthBar) { renderHealthBar(55, 20); }
    }

    public override void kill()
    {
        SoundHandler.medium_death.play();
        base.kill();
        
    }
}