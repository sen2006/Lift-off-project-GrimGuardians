using GXPEngine;
using GXPEngine.Managers;

public class MediumEnemy : Shootable
{
    public MediumEnemy(string texture, int startX, int startY, float speed, ControllerHandler controllerHandler, int health = 1, int enemyDamage = 1, int enemyAttackSpeed = 1,  int points = 100, bool showHealthBar = true, int animationCols = 1, int animationRows = 1, int frames = -1) : base(texture, startX, startY, speed, controllerHandler, health, enemyDamage, enemyAttackSpeed, points, showHealthBar, animationCols, animationRows, frames)
    {
    }

    public void Update()
    {
        base.Update();
    }

    public void playAnimation()
    {
        base.playAnimation();
    }

    public void renderHealthBar()
    {
        base.renderHealthBar();
    }

    public float hit(float damage)
    {
        return base.hit(damage);
    }

    public void setOvertimeDamage(int damagePerSec, int forSec)
    {
        base.setOvertimeDamage(damagePerSec, forSec);
    }

    public void damageOverTime()
    {
        base.damageOverTime();
    }

    public void kill()
    {
        base.kill();
    }

    public void pointReward(int points)
    {
        base.pointReward(points);
    }
}