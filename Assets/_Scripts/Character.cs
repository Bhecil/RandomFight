using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float _health;
    [field:SerializeField] public CharacterSkill[] Skills { get; private set; } = new CharacterSkill[3];

    [SerializeField] private AnimationClip _idleAnimation;
    public Animation Anim { get; private set; }

    private GameController _controller;

    private void Start()
    {
        _controller = GameController.Instance;
        Anim = GetComponent<Animation>();
        //load all skill animations
        foreach (CharacterSkill skill in Skills)
        {
            skill.LoadAnim(Anim);
        }

        //load idle animation and play it
        _idleAnimation.legacy = true;
        Anim.AddClip(_idleAnimation, _idleAnimation.name);
        GoIdle();
    }

    public void UseSkill(int index, Character target)
    {
        StartCoroutine(Skills[index].UseSkill(this, target));
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

    private IEnumerator Wait(float delay)
    {
        yield return new WaitForSeconds(delay);
    } 

    public void GoIdle()
    {
        Anim.Play(_idleAnimation.name);
    }
}
