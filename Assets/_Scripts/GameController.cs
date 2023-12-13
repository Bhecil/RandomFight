using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Character _player;

    private void Start()
    {
        _player.ResetStats();
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
}
