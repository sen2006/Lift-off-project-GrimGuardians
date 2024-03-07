using System;

public static class PlayerHealthHandler
{
    static float playerHealth = 50;
    static float playerMaxHealth = playerHealth;

    public static void checkHealth()
    {
        if (playerHealth <= 0)
        {
            MyGame.GetGame().AddChild(new DeathButton());
            MyGame.GetUI_Handler().setPoints(0);
            playerHealth = playerMaxHealth;
        }
    }

    public static float getHealth()
    {
        return playerHealth;
    }

    public static float getMaxHealth()
    {
        return playerMaxHealth;
    }

    public static void setHealth(float newHealth)
    {
        playerHealth = newHealth;
        
    }

    public static void takeDamage(float damage)
    {
        SoundHandler.damage_taken.play();
        playerHealth -= damage;
        UI_Handler.pointsMultiplier = 1.00f;
        
    }
}
