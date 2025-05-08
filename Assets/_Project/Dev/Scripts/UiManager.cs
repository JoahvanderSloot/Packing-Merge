using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameSettings gameSettings;
    [SerializeField] TextMeshProUGUI highScoreText;

    void Start()
    {
        AudioManager.Instance.StopAllCoroutines();
        highScoreText.text = "High Score:\n" + gameSettings.HighScore.ToString();
    }

    public void StartGame()
    {
        AudioManager.Instance.Play("Click");
        SceneManager.LoadScene("Main");
    }
}
