using TMPro;
using UnityEngine;

public class SkillBar : MonoBehaviour
{
    [SerializeField] private SkillButton[] _buttons;

    public void SetSkillNames(CharacterSkill[] skills)
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].SetName(skills[i].name);
        }
    }

    public void DisplaySkillCooldowns(CharacterSkill[] skills)
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].SetCoolDown(skills[i].RemainingCooldown);
        }
    }
}
