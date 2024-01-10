using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void Start()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
