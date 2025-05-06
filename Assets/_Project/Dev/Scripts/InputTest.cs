using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    [SerializeField] InputActionReference inputReference;
    [SerializeField] GameObject ball;

    public void SpawnObj()
    {
        Instantiate(ball, transform.position, Quaternion.identity);
    }
}
