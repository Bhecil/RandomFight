using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "New Skill")]
public class CharacterSkill : ScriptableObject
{
    //the effect of the skill
    [SerializeField] private int _damage;
    //the cooldown of the skill
    [SerializeField] private int _cooldown;
    public int _remainingCooldown;

    //the skill animation
    [SerializeField] private AnimationClip SkillAnim;
    // the skill return animation
    [SerializeField] private AnimationClip ReturnAnim;

    public bool IsReady()
    {
        return _remainingCooldown <= 0;
    }

    public void WaitTurn()
    {
        _remainingCooldown--;
        if (_remainingCooldown < 0)
        {
            _remainingCooldown = 0;
        }
    }

    public void ResetCoolDown()
    {
        _remainingCooldown = _cooldown;
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
        _remainingCooldown = _cooldown;

        //play skill animation
        user.Anim.Play(SkillAnim.name);
        yield return new WaitForSeconds(SkillAnim.length);

        //apply skill effect to target
        target.TakeDamage(_damage);

        //play return animation
        user.Anim.Play(ReturnAnim.name);
        yield return new WaitForSeconds(ReturnAnim.length);

        //set both user and traget to idle
        target.GoIdle();
        user.GoIdle();
    }
}
