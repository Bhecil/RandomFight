using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private CharacterSkill[] _skills = new CharacterSkill[3];

    private GameController _controller;
    private Animator _animator;
    private Animation _animation;

    private void Start()
    {
        _controller = GameController.Instance;
        _animator = GetComponent<Animator>();
        _animation = GetComponent<Animation>();
        _skills[0].SkillAnimation.legacy = true;
        _animation.AddClip(_skills[0].SkillAnimation, _skills[0].SkillAnimation.name);
    }

    public void AttackTarget(Character target)
    {
        _animation.Play(_skills[0].SkillAnimation.name);
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
