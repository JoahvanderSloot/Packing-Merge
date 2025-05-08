using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameSettings GameSettings;

    [SerializeField] GameObject nextDropDisplay;
    [SerializeField] GameObject[] dropPrefabs;
    [SerializeField] Player player;
    [SerializeField] GameObject GameOverMenu;

    [SerializeField] TextMeshProUGUI scoreText;

    GameObject nextDrop;

    private void Start()
    {
        ChooseRandomDrop();
    }

    private void Update()
    {
        scoreText.text = "Score:\n" + GameSettings.Score.ToString();
    }

    private void ChooseRandomDrop()
    {
        int _randomIndex = Random.Range(0, dropPrefabs.Length);
        nextDrop = dropPrefabs[_randomIndex];

        if (nextDropDisplay != null)
        {
            nextDropDisplay.GetComponent<Image>().sprite = nextDrop.GetComponent<SpriteRenderer>().sprite;
            nextDropDisplay.SetActive(true);
        }
    }

    public GameObject GetNextDrop()
    {
        return nextDrop;
    }

    public void UpdateNextDrop()
    {
        ChooseRandomDrop();
    }

    public void HomeButton()
    {
        if(GameSettings.Score > GameSettings.HighScore)
        {
            GameSettings.HighScore = GameSettings.Score;
        }
        GameSettings.Score = 0;
        SceneManager.LoadScene("Start");
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
    }
}
