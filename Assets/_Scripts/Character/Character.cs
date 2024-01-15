using UnityEngine;

public class Character : MonoBehaviour
{
    private GameController _controller;

    [Header("Character Stats")]
    //health
    [SerializeField] private float _maxHealth;
    [SerializeField] private HealthBar _healthBar;
    //skills
    [field: SerializeField] public Skill[] Skills { get; private set; }
    public int[] Cooldowns { get; private set; } = new int[3];

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
        foreach (Skill skill in Skills)
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
        //reset cooldown for this skill
        Cooldowns[index] = Skills[index].Cooldown + 1;
        //use the skill
        StartCoroutine(Skills[index].UseSkill(this, target));
    }

    public bool IsNotOnCooldown(int index)
    {
        return Cooldowns[index] <= 0;
    }

    public void ResetAllCooldowns()
    {
        for (int index = 0; index < Cooldowns.Length; index++)
        {
            Cooldowns[index] = Skills[index].Cooldown + 1;
        }
    }

    public void DecreaseCooldown()
    {
        //decrease all skills cooldown
        for (int index = 0; index < Cooldowns.Length; index++)
        {
            Cooldowns[index] = Mathf.Max(0, Cooldowns[index] - 1);
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
