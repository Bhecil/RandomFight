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
    [SerializeField] private GameObject _playerSkillBar;
    [SerializeField] private TextMeshProUGUI _playerHealthText;
    [SerializeField] private TextMeshProUGUI _enemyHealthText;

    private Character _player;
    private Character _enemy;

    private void Awake()
    {
        Instance = this;
        StartFight();
    }

    private void StartFight()
    {
        //spawn player and give health reference
        _player = Instantiate(_playerPrefab, _playerSpawnPoint).GetComponent<Character>();
        _player.HealthText = _playerHealthText;

        //spawn enemy and give health reference
        _enemy = Instantiate(_enemyPrefab, _enemySpawnPoint).GetComponent<Character>();
        _enemy.HealthText = _enemyHealthText;

        //set player skill bar and reset skill cooldowns
        _playerSkillBar.GetComponent<SkillBar>().SetSkillNames(_player.Skills);
        foreach (CharacterSkill skill in _player.Skills)
        {
            skill.ResetCoolDown();
        }
    }

    public void OnSkillUse(int index)
    {
        if (_player.Skills[index].IsReady())
        {
            _playerSkillBar.SetActive(false);
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

    private void PlayerTurn()
    {
        //show player skill bar
        _playerSkillBar.SetActive(true);
        //decrease all skill timers in the skill bar
        foreach (CharacterSkill skill in _player.Skills)
        {
            skill.WaitTurn();
        }
    }

    private IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(2f);
        //use a skill
        _enemy.UseSkill(0, _player);

        yield return new WaitForSeconds(2f);
        //player turn
        PlayerTurn();
    }

    private void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
