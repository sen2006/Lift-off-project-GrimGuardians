using GXPEngine;
using GXPEngine.Managers;

public class LargeEnemy : Shootable
{
    public LargeEnemy(int startX, int startY, float speed, int health = 1, int enemyDamage = 1, int enemyAttackSpeed = 1, int points = 100, bool showHealthBar = true, int animationCols = 1, int animationRows = 1, int frames = -1) : base("assets/sprites/enemies/large_monster.png", startX, startY, speed, health, enemyDamage, enemyAttackSpeed , points, showHealthBar, animationCols, animationRows, frames)
    {
    }
    void Update()
    {
        base.Update();
        if (showHealthBar) { renderHealthBar(100, 40); }
    }

    void kill()
    {
        SoundHandler.large_death.play();
        base.kill();
    }
}