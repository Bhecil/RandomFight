using System.Collections;
using UnityEngine;

public enum SkillType
{
    Attack,
    Heal
}

[CreateAssetMenu(menuName = "New Skill")]
public class CharacterSkill : ScriptableObject
{
    [Header("Skill stats")]
    //Skill type
    [SerializeField] SkillType _skillType;
    //Skill effect value
    [SerializeField] private int _effectValue;
    //Skill cooldown on use
    [SerializeField] private int _cooldown;

    public int RemainingCooldown { get; set; } = 0;

    [Header("Skill animations")]
    //the skill animation
    [SerializeField] private AnimationClip SkillAnim;
    // the skill return animation
    [SerializeField] private AnimationClip ReturnAnim;

    public void ResetCooldown()
    {
        //set skill reamining cooldown to its cooldown value +1
        RemainingCooldown = _cooldown + 1;
    }

    public void LoadAnim(Animation animation )
    {
        SkillAnim.legacy = true;
        animation.AddClip(SkillAnim, SkillAnim.name);
        ReturnAnim.legacy = true;
        animation.AddClip(ReturnAnim, ReturnAnim.name);
    }

    public IEnumerator UseSkill(Character user, Character target)
    {
        //reset remaining cooldown
        ResetCooldown();

        //play skill animation
        user.Anim.Play(SkillAnim.name);
        yield return new WaitForSeconds(SkillAnim.length);

        //apply skill effect to target
        switch (_skillType)
        {
            case SkillType.Attack:
                target.ModifyHealth(-_effectValue);
                break;
            case SkillType.Heal:
                user.ModifyHealth(_effectValue);
                break;
            default:
                break;
        }

        //play return animation
        user.Anim.Play(ReturnAnim.name);
        yield return new WaitForSeconds(ReturnAnim.length);

        //set both user and traget to idle
        target.GoIdle();
        user.GoIdle();
    }
}
