using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectTypes
{
    public String Name;
    public float Size;
    public Sprite Sprite;
    public int Score;
}

public class ObjectManager : MonoBehaviour
{
    [SerializeField]
    public int ObjectTypeIndex = 0;

    [SerializeField]
    private List<ObjectTypes> objectTypes = new List<ObjectTypes>();

    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider2D;

    [SerializeField]
    GameObject PlayerObject;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        transform.localScale = new Vector3(
            objectTypes[ObjectTypeIndex].Size,
            objectTypes[ObjectTypeIndex].Size,
            1f
        );

        spriteRenderer.sprite = objectTypes[ObjectTypeIndex].Sprite;
        circleCollider2D.radius = 0.09f;
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
        GameObject newObject = Instantiate(PlayerObject, middlePoint, Quaternion.identity);
        newObject.GetComponent<ObjectManager>().ObjectTypeIndex = ObjectTypeIndex + 1;
        newObject.GetComponent<CircleCollider2D>().enabled = true;
        newObject.GetComponent<ObjectManager>().enabled = true;
    }
}
