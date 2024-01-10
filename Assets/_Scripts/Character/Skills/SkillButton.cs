using TMPro;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _cooldownText;

    public void SetName(string name)
    {
        _nameText.text = name;
    }

    public void SetCoolDown(int cooldown)
    {
        _cooldownText.text = cooldown.ToString();
        if (cooldown > 0)
        {
            _cooldownText.enabled = true;
        }
        else
        {
            _cooldownText.enabled = false;
        }
    }
}
