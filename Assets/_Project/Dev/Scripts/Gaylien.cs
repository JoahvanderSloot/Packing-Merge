using System.Collections;
using UnityEngine;

public class Gaylien : MonoBehaviour
{
    [SerializeField]
    private Transform[] points;

    [SerializeField]
    private float movespeed;

    [SerializeField]
    private float turnSpeed = 5f; // how fast to blend between directions

    private Vector2 direction; // current movement direction
    private Vector2 desiredDirection; // next target direction
    private int pointIndex;

    [SerializeField]
    private Transform DropPoint;

    [SerializeField]
    private GameObject peanutPrefab;

    [SerializeField]
    private float peanutDropSpeed = 1;

    void Start()
    {
        transform.position = points[pointIndex].position;
        desiredDirection = (points[pointIndex].position - transform.position).normalized;
        direction = desiredDirection;
        StartCoroutine(DropPeanut());
    }

    void Update()
    {
        float dist = Vector2.Distance(transform.position, points[pointIndex].position);
        if (dist < 0.1f)
        {
            pointIndex = pointIndex + 1;
        }
        desiredDirection = (points[pointIndex].position - transform.position).normalized;
        direction = Vector2.Lerp(direction, desiredDirection, turnSpeed * Time.deltaTime);
        transform.position += (Vector3)(direction * movespeed * Time.deltaTime);

        if (pointIndex >= points.Length - 1)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DropPeanut()
    {
        while (true)
        {
            yield return new WaitForSeconds(peanutDropSpeed);
            Instantiate(peanutPrefab, DropPoint.position, Quaternion.identity);
        }
    }
}
