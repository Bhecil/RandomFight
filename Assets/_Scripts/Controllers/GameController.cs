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
        FightSetup();
    }

    private void FightSetup()
    {
        //spawn player
        _player = Instantiate(_playerPrefab, _playerSpawnPoint).GetComponent<Character>();

        //spawn enemy
        _enemy = Instantiate(_enemyPrefab, _enemySpawnPoint).GetComponent<Character>();

        //set player skill bar
        _playerSkillBar.SetSkillNames(_player.Skills);

        //reset all skills cooldown of the player and the enemy
        _player.ResetAllCooldowns();
        _enemy.ResetAllCooldowns();

        //start the fight
        PlayerTurn();
    }

    private void PlayerTurn()
    {
        //decrease player skills cooldown
        _player.DecreaseCooldown();
        //show player skill bar
        _playerSkillBar.gameObject.SetActive(true);
        //show player skills cooldowns
        _playerSkillBar.DisplaySkillCooldowns(_player.Cooldowns);
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
            if (_enemy.IsNotOnCooldown(index))
            {
                //if the skill is not on cooldown, use it
                _enemy.UseSkill(index, _player);
                break;
            }
        }
        //wait for skill animations to end
        yield return new WaitForSeconds(2f);

        //player turn
        PlayerTurn();
    }

    public void OnButtonPress(int index)
    {
        if (_player.IsNotOnCooldown(index))
        {
            //if the skill is not on cooldown :
            //hide the skill bar
            _playerSkillBar.gameObject.SetActive(false);

            //use ithe skill
            _player.UseSkill(index, _enemy);

            //enemy turn
            StartCoroutine(EnemyTurn());
        }
        else
        {
            //tell the player the skill is on cooldown
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
