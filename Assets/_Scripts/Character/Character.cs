using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Character : MonoBehaviour
{
    private GameController _controller;

    [Header("Character Stats")]
    //health
    [SerializeField] private float _maxHealth;
    [SerializeField] private HealthBar _healthBar;
    //skills
    [field:SerializeField] public CharacterSkill[] Skills { get; private set; } = new CharacterSkill[3];

    [Header("Character Animations")]
    [SerializeField] private AnimationClip _hitAnim;
    [SerializeField] private AnimationClip _idleAnim;

    private float _currentHealth;
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

    public void DecreaseCooldown()
    {
        //decrease character skills cooldown
        foreach (CharacterSkill skill in Skills)
        {
            skill.RemainingCooldown--;
            if (skill.RemainingCooldown < 0)
            {
                skill.RemainingCooldown = 0;
            }
        }
    }

    public void ModifyHealth(float modifier)
    {
        //play hit react animation
        Anim.Play(_hitAnim.name);

        //change current health value
        _currentHealth = Mathf.Clamp(_currentHealth + modifier, 0, _maxHealth);

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
