using UnityEngine;

[CreateAssetMenu(menuName = "New Skill")]
public class CharacterSkill : ScriptableObject
{
    [field:SerializeField] public AnimationClip SkillAnimation { get; private set; }
}
