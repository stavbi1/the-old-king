using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public SliderBar expBar;
    public SliderBar healthBar;
    public Level levelUI;
    public GameObject statGameObject;
    public AudioClip levelUpSound;
    public Player player;

    private StatPanel statPanel;
    private const float EXP_UPGRADE_DELTA = 2f;

    // stats
    private float currentExp = 0f;
    private float maxExp = 100f;
    private int level = 1;
    private float damage = 10f;
    private float jumpForce = 6f;

    public void Start()
    {
        expBar.Init(maxExp, currentExp);
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetJumpForce()
    {
        return jumpForce;
    }

    public void AddExp(float addedExp)
    {
        currentExp += addedExp;

        if (currentExp >= maxExp)
        {
            LevelUp();
        }
        else
        {
            expBar.SetValue(currentExp);
        }
    }

    public void SetCurrentHP(float HP)
    {
        healthBar.SetValue(HP);
    }

    public void Init(float maxHP, float speed)
    {
        healthBar.Init(maxHP, maxHP);

        statPanel = statGameObject.GetComponent<StatPanel>();
        statPanel.Init(new Stats(maxHP, damage, speed, jumpForce));
    }

    public float UpgradeStat(StatIndex statIndex)
    {
        float newValue = statPanel.UpgradeStat(statIndex);

        switch (statIndex)
        {
            case StatIndex.HP:
                healthBar.Init(newValue, newValue);
                break;
            case StatIndex.ATTACK:
                damage = newValue;
                break;
            case StatIndex.JUMP:
                jumpForce = newValue;
                break;
            default:
                break;
        }

        return newValue;
    }

    private void LevelUp()
    {
        currentExp = currentExp - maxExp;
        maxExp *= EXP_UPGRADE_DELTA;
        expBar.Init(maxExp, currentExp);

        level++;
        levelUI.SetLevel(level);

        player.FulfillHP();
        healthBar.Fulfill();
        statPanel.AddLevelPoint();
        AudioSource.PlayClipAtPoint(levelUpSound, transform.position);
    }

}
