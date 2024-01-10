using UnityEngine;

public class Character : MonoBehaviour
{
    private GameController _controller;

    [Header("Stats")]
    //health
    [SerializeField] private float _maxHealth;
    [SerializeField] private HealthBar _healthBar;

    private float _currentHealth;

    //skills
    [field:SerializeField] public CharacterSkill[] Skills { get; private set; } = new CharacterSkill[3];

    [Header("Animations")]
    [SerializeField] private AnimationClip _hitAnim;
    [SerializeField] private AnimationClip _idleAnim;
    public Animation Anim { get; private set; }

    private void Start()
    {
        _controller = GameController.Instance;
        // set Health
        _currentHealth = _maxHealth;
        //HealthText.text = _currentHealth + " / " + _maxHealth;

        Anim = GetComponent<Animation>();
        //load all skill animations
        foreach (CharacterSkill skill in Skills)
        {
            skill.LoadAnim(Anim);
        }

        //load hit animation
        _hitAnim.legacy = true;
        Anim.AddClip(_hitAnim, _hitAnim.name);

        //load idle animation and play it
        _idleAnim.legacy = true;
        Anim.AddClip(_idleAnim, _idleAnim.name);
        GoIdle();
    }

    public void UseSkill(int index, Character target)
    {
        StartCoroutine(Skills[index].UseSkill(this, target));
    }

    public void TakeDamage(float damage)
    {
        //play hit react animation
        Anim.Play(_hitAnim.name);
        //change current health value
        _currentHealth = Mathf.Max(_currentHealth - damage, 0);

        // update Health bar
        _healthBar.UpdateHealthBar(_maxHealth, _currentHealth);

        if (_currentHealth <= 0 )
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

    public void GoIdle()
    {
        Anim.Play(_idleAnim.name);
    }
}
