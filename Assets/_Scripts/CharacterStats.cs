using UnityEngine;

[CreateAssetMenu(menuName = "New Character")]
public class CharacterStats : ScriptableObject
{
    [field: SerializeField] public int HealthMax { get; private set; }
    [field: SerializeField] public int Attack { get; private set; }
    [field: SerializeField] public int Defense { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
}
