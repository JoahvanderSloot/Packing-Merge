using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> itemPrefs = new List<GameObject>();

    [SerializeField] int givesScore;

    private int ObjectTypeIndex;
    public bool CanDie = false;

    private void Start()
    {
        // get index of item
        foreach (GameObject _item in itemPrefs)
        {
            if (
                _item.GetComponent<SpriteRenderer>().sprite
                == gameObject.GetComponent<SpriteRenderer>().sprite
            )
            {
                ObjectTypeIndex = itemPrefs.IndexOf(_item);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            CanDie = true;
        }
        else if (collision.gameObject.CompareTag("Peanut"))
        {
            if(collision.gameObject.GetComponent<Peanut>().CanKill)
            {
                CanDie = true;
            }
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("LineHit"))
        {

            if (collision.gameObject.GetComponent<ObjectManager>() != null)
            {
                if (collision.gameObject.GetComponent<ObjectManager>().CanDie)
                {
                    CanDie = true;
                }
            }
        }

        ObjectManager other = collision.gameObject.GetComponent<ObjectManager>();
        if (other == null || other.ObjectTypeIndex != ObjectTypeIndex)
            return;

        Destroy(gameObject);
        Destroy(collision.gameObject);
        AudioManager.Instance.Play("Merge");

        if (ObjectTypeIndex == itemPrefs.Count - 1)
        {
            GameObject[] _peanutsArray = GameObject.FindGameObjectsWithTag("Peanut");

            foreach (GameObject _peanut in _peanutsArray)
            {
                Destroy(_peanut);
            }
        }
        else
        {
            // ensure only one of the two runs the merge
            if (this.GetInstanceID() > collision.gameObject.GetInstanceID())
                return;

            Vector2 middlePoint = Vector2.Lerp(transform.position, collision.transform.position, 0.5f);

            // spawn one merged object
            GameObject _newBall = Instantiate(itemPrefs[ObjectTypeIndex + 1], middlePoint, Quaternion.identity);
            _newBall.GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
        GameManager _gameManager = FindFirstObjectByType<GameManager>();
        _gameManager.Settings.Score += givesScore;
    }
}
