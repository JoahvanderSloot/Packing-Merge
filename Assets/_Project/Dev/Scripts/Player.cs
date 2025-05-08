using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    InputActionReference tapAction;

    [SerializeField]
    InputActionReference positionAction;

    [SerializeField]
    float startYpos;

    GameManager gameManager;
    GameObject currentBall;
    GameObject currentLine;
    bool isDragging = false;
    bool canDrag = true;
    bool isSpawning = false;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        StartCoroutine(SpawnNewBall());
    }

    private void OnEnable()
    {
        tapAction.action.performed += OnTap;
        tapAction.action.canceled += OnRelease;
        tapAction.action.Enable();
        positionAction.action.Enable();
    }

    private void OnDisable()
    {
        tapAction.action.performed -= OnTap;
        tapAction.action.canceled -= OnRelease;
        tapAction.action.Disable();
        positionAction.action.Disable();
    }

    private void OnTap(InputAction.CallbackContext _context)
    {
        if (!canDrag || currentBall == null || gameManager.GameOverMenu.activeInHierarchy)
            return;

        Vector2 _screenPos = positionAction.action.ReadValue<Vector2>();
        Vector3 _worldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(_screenPos.x, _screenPos.y, 10f)
        );

        currentBall.transform.position = new Vector3(_worldPos.x, startYpos, 0f);
        isDragging = true;
    }

    private void OnRelease(InputAction.CallbackContext _context)
    {
        if (currentBall == null || isSpawning)
            return;

        isDragging = false;
        canDrag = false;
        currentBall.GetComponent<Rigidbody2D>().gravityScale = 1f;
        currentBall.GetComponent<PolygonCollider2D>().isTrigger = false;
        currentBall = null;

        StartCoroutine(SpawnNewBall());
    }

    private IEnumerator SpawnNewBall()
    {
        isSpawning = true;
        yield return new WaitForSeconds(0.2f);

        GameObject _nextDropPrefab = gameManager.GetNextDrop();
        currentBall = Instantiate(
            _nextDropPrefab,
            new Vector3(0f, startYpos, 0f),
            Quaternion.identity
        );
        currentBall.GetComponent<PolygonCollider2D>().isTrigger = true;
        currentLine = currentBall.transform.GetChild(0).gameObject;
        currentLine.SetActive(false);

        canDrag = true;
        isSpawning = false;

        gameManager.UpdateNextDrop();
    }

    private void Update()
    {
        if (isDragging && currentBall != null)
        {
            Vector2 _screenPos = positionAction.action.ReadValue<Vector2>();
            Vector3 _worldPos = Camera.main.ScreenToWorldPoint(
                new Vector3(_screenPos.x, _screenPos.y, 10f)
            );

            currentBall.transform.position = new Vector3(_worldPos.x, startYpos, 0f);

            if (currentLine != null)
            {
                currentLine.SetActive(true);
            }
        }
        else
        {
            if (currentLine != null)
            {
                currentLine.SetActive(false);
            }
        }
    }
}
