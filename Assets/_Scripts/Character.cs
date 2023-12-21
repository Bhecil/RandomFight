using System.Collections;
using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    private GameController _controller;

    [SerializeField] private float _maxHealth;
    private float _currentHealth;
    public TextMeshProUGUI HealthText { get; set; }

    [field:SerializeField] public CharacterSkill[] Skills { get; private set; } = new CharacterSkill[3];

    public Animation Anim { get; private set; }
    [SerializeField] private AnimationClip _hitAnim;
    [SerializeField] private AnimationClip _idleAnim;

    private void Start()
    {
        _controller = GameController.Instance;
        // set Health
        _currentHealth = _maxHealth;
        HealthText.text = _currentHealth + " / " + _maxHealth;

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
        Anim.Play(_hitAnim.name);
        _currentHealth = Mathf.Max(_currentHealth - damage, 0);

        // update Health bar
        HealthText.text = _currentHealth + " / " + _maxHealth;

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
