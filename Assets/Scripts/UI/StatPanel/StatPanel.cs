using UnityEngine;
using UnityEngine.UI;

public class StatPanel : MonoBehaviour
{
    public Button statButton;

    private StatObject[] statObjects;
    private Color NORMAL_STAT_COLOR = new Color(1, 1, 1);
    private Color TOGGLE_STAT_COLOR = new Color(0.599f, 0.732f, 1);
    private ColorBlock statButtonColors;
    private int pointsLeft;

    private void ToggleUpgrade()
    {
        if (statButton.colors.normalColor.Equals(NORMAL_STAT_COLOR))
        {
            statButtonColors.normalColor = TOGGLE_STAT_COLOR;
        }
        else
        {
            statButtonColors.normalColor = NORMAL_STAT_COLOR;
        }
        statButton.colors = statButtonColors;

        foreach (StatObject stat in statObjects)
        {
            stat.ToggleButton();
        }
    }

    public void Init(Stats stats)
    {
        pointsLeft = 0;
        statButtonColors = statButton.colors;

        statObjects = new StatObject[Stats.STAT_LEN];
        int statIndex = 0;

        foreach (Transform stat in transform)
        {
            statObjects[statIndex] = new StatObject(
                stat.GetComponentInChildren<Text>(),
                stat.GetComponentInChildren<Button>(),
                stats.statValues[statIndex].ToString()
            );

            statIndex++;
        }
    }

    public void AddLevelPoint()
    {
        if (pointsLeft == 0)
        {
            ToggleUpgrade();
        }

        pointsLeft++;
    }

    public float UpgradeStat(StatIndex statIndex)
    {
        if (pointsLeft == 1)
        {
            ToggleUpgrade();
        }

        pointsLeft--;

        return statObjects[(int)statIndex].UpgradeStat(Stats.statToUpgradeValue[statIndex]);
    }
}
