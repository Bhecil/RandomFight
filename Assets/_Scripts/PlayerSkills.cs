using TMPro;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _buttons;

    public void SetSkillNames(CharacterSkill[] skills)
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].text = skills[i].name;
        }
    }
}
