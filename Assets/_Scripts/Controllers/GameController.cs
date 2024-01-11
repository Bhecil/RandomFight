using System.Collections;
using TMPro;
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
    [SerializeField] private SkillBar _playerSkillBar;

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

        //set player skill bar
        _playerSkillBar.SetSkillNames(_player.Skills);

        //reset all skills cooldown
        foreach (CharacterSkill skill in _player.Skills)
        {
            skill.ResetCooldown();
        }
        PlayerTurn();
    }

    private void PlayerTurn()
    {
        //decrease player skills cooldown
        _player.DecreaseCooldown();
        //show player skill bar
        _playerSkillBar.gameObject.SetActive(true);
        //show player skills cooldowns
        _playerSkillBar.DisplaySkillCooldowns(_player.Skills);
    }

    private IEnumerator EnemyTurn()
    {
        //wait for for play to end their turn
        yield return new WaitForSeconds(2f);

        //decrease enemy skills cooldown
        _enemy.DecreaseCooldown();

        //choose skill and use it
        for (int index = _enemy.Skills.Length - 1; index >= 0; index--)
        {
            if (_enemy.Skills[index].RemainingCooldown <= 0)
            {
                _enemy.UseSkill(index, _player);
                break;
            }
            else
            {
                Debug.Log(_enemy.Skills[index].name);
            }
        }
        //wait for skill animations to end
        yield return new WaitForSeconds(2f);

        //player turn
        PlayerTurn();
    }

    public void OnSkillUse(int index)
    {
        var skill = _player.Skills[index];
        if (skill.RemainingCooldown <= 0)
        {
            _playerSkillBar.gameObject.SetActive(false);
            _player.UseSkill(index, _enemy);

            StartCoroutine(EnemyTurn());
        }
        else
        {
            Debug.Log("Skill is on Cooldown!");
        }
    }

    public void Victory()
    {
        Debug.Log("Victory!");
        QuitGame();
    }

    public void Defeat()
    {
        Debug.Log("Defeat!");
        QuitGame();
    }

    private void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
