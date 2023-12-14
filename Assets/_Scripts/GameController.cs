using UnityEngine;

public enum GameState
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WIN,
    LOOSE
}

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _enemySpawnPoint;

    /*private Character _player;
    private Character _enemy;*/

    private GameState _currentState;

    private void Start()
    {
        _currentState = GameState.START;
        StartRound();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.TryGetComponent(out Character character))
                    {
                        if (character.CompareTag("Enemy"))
                        {
                            character.Die();
                        }
                    }
                }
            }
        }
    }

    private void StartRound()
    {
        //spawn player
        var player = Instantiate(_playerPrefab, _playerSpawnPoint).GetComponent<Character>();
        //reset player
        player.ResetStats();

        //spawn enemy
        var enemy = Instantiate(_enemyPrefab, _enemySpawnPoint).GetComponent<Character>();
        // reset enemy
        enemy.ResetStats();
    }

    private void PlayerRound()
    {

    }
}
