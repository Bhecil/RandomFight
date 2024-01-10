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

    public void UpdateCooldownText()
    {
        _cooldownText.text = _nameText.text;
    }
}
