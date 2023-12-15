using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _damage;

    private GameController _controller;
    private Animator _animator;

    private void Start()
    {
        _controller = GameController.Instance;
        _animator = GetComponent<Animator>();
    }

    public void AttackTarget(Character target)
    {
        _animator.SetTrigger("Attack");
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
