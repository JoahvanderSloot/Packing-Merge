using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButtons : MonoBehaviour
{
    [SerializeField] private Button audioButton;
    [SerializeField] private Button musicButton;

    [SerializeField] private List<Sprite> images;

    [SerializeField] bool isAudioButton;

    [SerializeField] GameSettings gameSettings;

    private void Start()
    {
        if (isAudioButton)
        {
            UpdateButtonSprite(audioButton, gameSettings.Audio);
        }
        else
        {
            UpdateButtonSprite(musicButton, gameSettings.Music);
        }
    }

    public void AudioButton()
    {
        if (audioButton != null)
        {
            //AudioManager.m_Instance.Play("Hover");
            gameSettings.Audio = !gameSettings.Audio;
            UpdateButtonSprite(audioButton, gameSettings.Audio);
        }
    }

    public void MusicButton()
    {
        if (musicButton != null)
        {
            gameSettings.Music = !gameSettings.Music;
            UpdateButtonSprite(musicButton, gameSettings.Music);

            if (gameSettings.Music)
            {
                //AudioManager.m_Instance.Play("MenuMusic");
            }
            else
            {
                //AudioManager.m_Instance.StopAllSounds();
            }

            //AudioManager.m_Instance.Play("Click");
        }
    }

    private void UpdateButtonSprite(Button _button, bool _isActive)
    {
        if (_button != null && images != null && images.Count >= 2)
        {
            _button.image.sprite = _isActive ? images[1] : images[0];
        }
    }
}
