using UnityEngine;

public class Killbox : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectManager _obj = collision.gameObject.GetComponent<ObjectManager>();
        if (_obj != null)
        {
            if (_obj.CanDie)
            {
                gameManager.GameOver();
            }
        }
    }
}
