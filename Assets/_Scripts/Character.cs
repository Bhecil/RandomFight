using UnityEngine;

public class Character : MonoBehaviour
{
    [field:SerializeField] public CharacterStats Stats { get; private set; }

    private int _healthValue;

    public void ResetStats()
    {
        _healthValue = Stats.HealthMax;
    }

    public void Attack(Character target)
    {
        int attackValue = Random.Range(1, 6) + Stats.Attack;

        if (attackValue > target.Stats.Attack)
        {
            target.TakeDamage(Stats.Damage);
        }
    }

    public void TakeDamage(int damageValue)
    {
        _healthValue -= damageValue;
        _healthValue = Mathf.Max(_healthValue, 0);
        if (_healthValue <= 0)
        {
            Die();
        }
    }

    public void Heal(int healValue)
    {
        _healthValue += healValue;
        _healthValue = Mathf.Min(_healthValue, Stats.HealthMax);
    }

    public void Die()
    {
        Debug.Log(name +" is Dead");
        Destroy(gameObject);
    }
}
