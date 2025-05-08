using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameSettings Settings;

    [SerializeField] GameObject nextDropDisplay;
    [SerializeField] GameObject[] dropPrefabs;
    [SerializeField] Player player;
    public GameObject GameOverMenu;

    [SerializeField] TextMeshProUGUI scoreText;

    GameObject nextDrop;

    private void Start()
    {
        AudioManager.Instance.Play("GameMusic");
        Settings.Score = 0;
        GameOverMenu.SetActive(false);
        ChooseRandomDrop();
    }

    private void Update()
    {
        scoreText.text = "Score:\n" + Settings.Score.ToString();
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
        AudioManager.Instance.Play("Click");
        if (Settings.Score > Settings.HighScore)
        {
            Settings.HighScore = Settings.Score;
        }
        Settings.Score = 0;
        SceneManager.LoadScene("Start");
    }

    public void Restart()
    {
        AudioManager.Instance.Play("Click");
        SceneManager.LoadScene("Main");
    }

    public void GameOver()
    {
        AudioManager.Instance.Play("Lose");
        GameOverMenu.SetActive(true);

        if (Settings.Score > Settings.HighScore)
        {
            Settings.HighScore = Settings.Score;
        }
        Settings.Score = 0;
    }
}
