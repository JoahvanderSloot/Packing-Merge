using UnityEngine;

public class Killbox : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    bool soundPlayed = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        ObjectManager _obj = collision.gameObject.GetComponent<ObjectManager>();
        if (_obj != null)
        {
            if (_obj.CanDie)
            {
                if (!soundPlayed)
                {
                    AudioManager.Instance.Play("Lose");
                    soundPlayed = true;
                }
                gameManager.GameOver();
            }
        }
    }
}
