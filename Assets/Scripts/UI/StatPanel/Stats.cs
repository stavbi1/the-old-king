using System.Collections.Generic;

public enum StatIndex
{
    HP,
    ATTACK,
    SPEED,
    JUMP
}


public class Stats
{
    public static Dictionary<StatIndex, float> statToUpgradeValue = new Dictionary<StatIndex, float>()
        {
            { StatIndex.HP, 10f },
            { StatIndex.ATTACK, 1f },
            { StatIndex.JUMP, 0.15f },
            { StatIndex.SPEED, 0.003f }
        };

    public const int STAT_LEN = 4;
    
    public float[] statValues;

    public Stats(float hp, float attack, float speed, float jump)
    {
        statValues = new float[STAT_LEN] { hp, attack, speed, jump };

        
    }
}
