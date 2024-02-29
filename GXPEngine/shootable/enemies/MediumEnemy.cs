using GXPEngine;
using GXPEngine.Managers;

public class MediumEnemy : Shootable
{
    public MediumEnemy(int startX, int startY, float speed, int health = 1, int enemyDamage = 1, int enemyAttackSpeed = 1,  int points = 100, bool showHealthBar = true, int animationCols = 1, int animationRows = 1, int frames = -1) : base("assets/sprites/enemies/medium_enemy.png", startX, startY, speed, health, enemyDamage, enemyAttackSpeed, points, showHealthBar, animationCols, animationRows, frames)
    {
    }
    void Update()
    {
        base.Update();
        if (showHealthBar) { renderHealthBar(55, 20); }
    }

    public override void kill()
    {
        SoundHandler.medium_death.play();
        UI_Handler.pointsMultiplier += 0.01f;
        base.kill();
        
    }
}