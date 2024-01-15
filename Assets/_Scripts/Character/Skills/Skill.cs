using System.Collections;
using UnityEngine;

public enum SkillType
{
    Attack,
    Heal
}

[CreateAssetMenu(menuName = "Skill")]
public class Skill : ScriptableObject
{
    [Header("Skill stats")]
    //Skill type
    public SkillType Type;
    //Skill effect value
    public int EffectValue;
    //Skill cooldown on use
    public int Cooldown;

    public int RemainingCooldown { get; set; } = 0;

    [Header("Skill animations")]
    //the skill animation
    [SerializeField] private AnimationClip SkillAnim;
    // the skill return animation
    [SerializeField] private AnimationClip ReturnAnim;

    public void LoadAnim(Animation animation )
    {
        SkillAnim.legacy = true;
        animation.AddClip(SkillAnim, SkillAnim.name);
        ReturnAnim.legacy = true;
        animation.AddClip(ReturnAnim, ReturnAnim.name);
    }

    public IEnumerator UseSkill(Character user, Character target)
    {
        //play skill animation
        user.Anim.Play(SkillAnim.name);
        yield return new WaitForSeconds(SkillAnim.length);

        //apply skill effect to target
        switch (Type)
        {
            case SkillType.Attack:
                target.ModifyHealth(-EffectValue);
                break;
            case SkillType.Heal:
                user.ModifyHealth(EffectValue);
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
