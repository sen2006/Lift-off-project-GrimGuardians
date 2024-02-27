using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class PlayerHealthHandler
{
    static float playerHealth = 0;
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

    public static void changeHealth(float change)
    {
        playerHealth += change;
    }
}
