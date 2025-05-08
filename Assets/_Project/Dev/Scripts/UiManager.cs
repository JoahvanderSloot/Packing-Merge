using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameSettings gameSettings;
    [SerializeField] TextMeshProUGUI highScoreText;

    void Start()
    {
        highScoreText.text = "High Score:\n" + gameSettings.HighScore.ToString();
    }

    public void StartGame()
    {
        //AudioManager.m_Instance.Play("Click");
        SceneManager.LoadScene("Main");
    }
}
