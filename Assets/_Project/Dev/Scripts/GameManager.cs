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
    int nextDropIndex = -1;

    private void Start()
    {
        if (AudioManager.Instance.IsPlaying("Lose"))
        {
            AudioManager.Instance.Stop("Lose");
        }
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
        if (nextDropIndex == -1)
        {
            nextDropIndex = 0;
        }
        else
        {
            int _randomIndex = Random.Range(1, dropPrefabs.Length);
            if (_randomIndex == nextDropIndex)
            {
                _randomIndex = 0;
            }
            nextDropIndex = _randomIndex;
        }

        nextDrop = dropPrefabs[nextDropIndex];

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

        if (AudioManager.Instance.IsPlaying("GameMusic"))
        {
            AudioManager.Instance.Stop("GameMusic");
        }
        if (AudioManager.Instance.IsPlaying("Lose"))
        {
            AudioManager.Instance.Stop("Lose");
        }

        SceneManager.LoadScene("Start");
    }

    public void Restart()
    {
        AudioManager.Instance.Play("Click");
        SceneManager.LoadScene("Main");
    }

    public void GameOver()
    {
        if (AudioManager.Instance.IsPlaying("GameMusic"))
        {
            AudioManager.Instance.Stop("GameMusic");
        }
        GameOverMenu.SetActive(true);

        if (Settings.Score > Settings.HighScore)
        {
            Settings.HighScore = Settings.Score;
        }
    }
}
