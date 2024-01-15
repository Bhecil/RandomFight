using TMPro;
using UnityEngine;

public class SkillBar : MonoBehaviour
{
    [SerializeField] private SkillButton[] _buttons;

    public void SetSkillNames(Skill[] skills)
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].SetName(skills[i].name);
        }
    }

    public void DisplaySkillCooldowns(int[] cooldowns)
    {
        for (int index = 0; index < _buttons.Length; index++)
        {
            _buttons[index].SetCooldown(cooldowns[index]);
        }
    }
}
