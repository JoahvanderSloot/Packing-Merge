using UnityEngine;

public class Killbox : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        ObjectManager _obj = other.gameObject.GetComponent<ObjectManager>();
        if (_obj != null)
        {
            if (_obj.CanDie)
            {

            }
        }
    }
}
