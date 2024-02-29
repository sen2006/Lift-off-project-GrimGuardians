using System;

public static class PlayerHealthHandler
{
    static float playerHealth = 100;
    static float playerMaxHealth = playerHealth;

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
        playerHealth -= damage;
        Console.WriteLine("" + playerHealth);
    }
}
