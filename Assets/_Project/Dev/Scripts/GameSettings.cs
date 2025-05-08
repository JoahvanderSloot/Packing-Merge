using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings.asset", menuName = "Game Settings", order = 0)]
public class GameSettings : ScriptableObject
{
    public int HighScore;
    public int Score;
    public bool Audio;
    public bool Music;
}
