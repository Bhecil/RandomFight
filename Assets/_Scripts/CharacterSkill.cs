using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "New Skill")]
public class CharacterSkill : ScriptableObject
{
    [SerializeField] private int _damage;

    [field:SerializeField] public AnimationClip SkillAnim { get; private set; }
    [field:SerializeField] public AnimationClip ReturnAnim { get; private set; }

    public void LoadAnim(Animation animation )
    {
        SkillAnim.legacy = true;
        animation.AddClip(SkillAnim, SkillAnim.name);
        ReturnAnim.legacy = true;
        animation.AddClip(ReturnAnim, ReturnAnim.name);
    }

    public void PlaySkillAnim(Animation animation)
    {
        animation.Play(SkillAnim.name);
    }

    public IEnumerator UseSkill(Character user, Character target)
    {
        user.Anim.Play(SkillAnim.name);
        yield return new WaitForSeconds(SkillAnim.length);
        target.TakeDamage(_damage);
        user.Anim.Play(ReturnAnim.name);
        yield return new WaitForSeconds(ReturnAnim.length);
        user.GoIdle();
    }
}
