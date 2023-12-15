using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    //prefabs
    [Header("Prefabs")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _enemySpawnPoint;

    //interface
    [Header("User Interface")]
    [SerializeField] private GameObject _playerSkills;

    private Character _player;
    private Character _enemy;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartFight();
    }

    private void StartFight()
    {
        //spawn player
        _player = Instantiate(_playerPrefab, _playerSpawnPoint).GetComponent<Character>();

        //spawn enemy
        _enemy = Instantiate(_enemyPrefab, _enemySpawnPoint).GetComponent<Character>();
    }

    public void Attack()
    {
        _playerSkills.gameObject.SetActive(false);
        _player.AttackTarget(_enemy);
        //enemyturn
    }

    public void Victory()
    {
        Debug.Log("Victory!");
    }

    public void Defeat()
    {
        Debug.Log("Defeat!");
    }

    private void PlayerTurn()
    {

    }
}
