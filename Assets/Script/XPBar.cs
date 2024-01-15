using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{

    public Slider xpBar;
    public Text levelText;

    public void SetMaxXP(int xp)
    {
        xpBar.maxValue = xp;
    }

    public void SetXP(int xp)
    {
        xpBar.value = xp;
    }

    public void levelDisplay(int level)
    {
        levelText.text = $"LVL {level}";
    }
}
