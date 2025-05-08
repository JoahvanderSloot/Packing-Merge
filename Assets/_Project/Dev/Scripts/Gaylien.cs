using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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

    [SerializeField]
    private int pointIndex;

    [SerializeField]
    private Transform DropPoint;

    [SerializeField]
    private GameObject peanutPrefab;

    [SerializeField]
    private float peanutDropSpeed = 1;

    [SerializeField]
    private List<Sprite> peanutSprites = new List<Sprite>();

    [SerializeField]
    private bool isreset = false;

    Coroutine peanutDropCoroutine;

    private GameManager gameManager;
    private int currentScore = 0;

    private int scoreGoal = 0;

    [SerializeField]
    private int scoreSpawn = 0;

    bool saidHello = false;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        scoreGoal = scoreSpawn;
        pointIndex = points.Length - 1;
        transform.position = points[pointIndex].position;
        desiredDirection = (points[pointIndex].position - transform.position).normalized;
        direction = desiredDirection;
        peanutDropCoroutine = StartCoroutine(DropPeanut());
    }

    void Update()
    {
        currentScore = gameManager.Settings.Score;
        if (currentScore >= scoreGoal)
        {

            scoreGoal += scoreSpawn;
            pointIndex = 0;
        }

        if (pointIndex >= points.Length - 1)
        {
            if (isreset == false)
            {
                StopCoroutine(peanutDropCoroutine);
                peanutDropCoroutine = null;
                isreset = true;
                transform.position = points[0].position;
            }

            return;
        }
        else
        {
            isreset = false;
            if (peanutDropCoroutine == null)
            {
                peanutDropCoroutine = StartCoroutine(DropPeanut());
                AudioManager.Instance.Play("Gaylien");
            }
        }
        float dist = Vector2.Distance(transform.position, points[pointIndex].position);
        if (dist < 0.1f)
        {
            pointIndex = pointIndex + 1;
        }
        desiredDirection = (points[pointIndex].position - transform.position).normalized;
        direction = Vector2.Lerp(direction, desiredDirection, turnSpeed * Time.deltaTime);
        transform.position += (Vector3)(direction * movespeed * Time.deltaTime);

        if(pointIndex == 3 && !saidHello)
        {
            AudioManager.Instance.Play("Hello");
            saidHello = true;
        }
        else if(pointIndex != 1 && saidHello)
        {
            saidHello = false;
        }
    }

    IEnumerator DropPeanut()
    {
        while (true)
        {
            yield return new WaitForSeconds(peanutDropSpeed);
            AudioManager.Instance.Play("Peanuts");
            GameObject peanut = Instantiate(peanutPrefab, DropPoint.position, Quaternion.identity);
            peanut.GetComponent<SpriteRenderer>().sprite = peanutSprites[
                Random.Range(0, peanutSprites.Count)
            ];
        }
    }
}