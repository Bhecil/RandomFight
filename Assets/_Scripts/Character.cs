using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private CharacterSkill[] _skills = new CharacterSkill[3];

    [SerializeField] private AnimationClip _idleAnimation;

    private GameController _controller;
    private Animation _animation;

    private void Start()
    {
        _controller = GameController.Instance;
        _animation = GetComponent<Animation>();

        //add idle animation and play it
        _idleAnimation.legacy = true;
        _animation.AddClip(_idleAnimation, _idleAnimation.name);
        _animation.Play(_idleAnimation.name);

        _skills[0].SkillAnimation.legacy = true;
        _animation.AddClip(_skills[0].SkillAnimation, _skills[0].SkillAnimation.name);
    }

    public void AttackTarget(Character target)
    {
        _animation.Play(_skills[0].SkillAnimation.name);
        //delay
        target.TakeDamage(_damage);
    }

    public void TakeDamage(float damage)
    {
        _health = Mathf.Max(_health - damage, 0);
        if (_health <= 0 )
        {
            Die();
        }
    }

    private void Die()
    {
        switch (tag)
        {
            case "Player":
                _controller.Defeat();
                break;
            case "Enemy":
                _controller.Victory();
                break;
        }
        Destroy(gameObject);
    }
}
