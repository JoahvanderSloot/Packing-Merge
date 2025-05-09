using System.Collections;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    bool soundPlayed = false;
    GameObject item;
    Coroutine killPlayer;

    private void OnTriggerStay2D(Collider2D collision)
    {
        ObjectManager _obj = collision.gameObject.GetComponent<ObjectManager>();
        if (_obj != null)
        {
            if (_obj.CanDie)
            {
                if (!soundPlayed)
                {
                    if(killPlayer == null)
                    {
                        killPlayer = StartCoroutine(TriggerGameOver());
                        item = collision.gameObject;
                    }
                }
            }
        }
        else if (collision.gameObject.CompareTag("Peanut"))
        {
            collision.gameObject.GetComponent<Peanut>().CanKill = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == item)
        {
            soundPlayed = false;
            item = null;
            StopCoroutine(killPlayer);
            killPlayer = null;
        }

        if (collision.gameObject.CompareTag("Peanut"))
        {
            collision.gameObject.GetComponent<Peanut>().CanKill = true;
        }
    }

    private IEnumerator TriggerGameOver()
    {
        yield return new WaitForSeconds(0.5f);
        soundPlayed = true;
        AudioManager.Instance.Play("Lose");
        gameManager.GameOver();
    }
}
