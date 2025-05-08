using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> itemPrefs = new List<GameObject>();

    private int ObjectTypeIndex;

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
        ObjectManager other = collision.gameObject.GetComponent<ObjectManager>();
        if (other == null || other.ObjectTypeIndex != ObjectTypeIndex)
            return;
        // ensure only one of the two runs the merge
        if (this.GetInstanceID() > collision.gameObject.GetInstanceID())
            return;

        Vector2 middlePoint = Vector2.Lerp(transform.position, collision.transform.position, 0.5f);

        // destroy both originals
        Destroy(gameObject);
        Destroy(collision.gameObject);

        // spawn one merged object
        Instantiate(itemPrefs[ObjectTypeIndex + 1], middlePoint, Quaternion.identity);
    }
}
