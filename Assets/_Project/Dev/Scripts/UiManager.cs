using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameSettings gameSettings;
    [SerializeField] TextMeshProUGUI highScoreText;

    void Start()
    {
        highScoreText.text = "High Score: " + gameSettings.HighScore.ToString();
    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        //AudioManager.m_Instance.Play("Click");
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        //AudioManager.m_Instance.Play("Click");
        Application.Quit();
    }
}
