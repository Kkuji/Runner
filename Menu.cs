using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public GameObject EndRun;
    public GameObject Background;
    public GameObject MusicController;
    public Button MusicControllerButton;
    public Sprite MusicOff;
    public Sprite MusicOn;
    public Button EffectsControllerButton;
    public Sprite EffectsOff;
    public Sprite EffectsOn;
    public GameObject EffectsController;
    public Transform Character;
    public static bool pause = true;
    public static AudioSource backgroundMelody;

    private bool music = true;
    private bool newLevel = false;
    private bool effects = true;

    public void Play()
    {
        if (PlayerController.died)
        {
            PlayerController.OneTimeDeath = true;
            PlayerController.died = false;
            PlayerController.animator.enabled = true;
            backgroundMelody = GetComponent<AudioSource>();
            backgroundMelody.Play();
        }
        else
        {
            backgroundMelody = GetComponent<AudioSource>();
            backgroundMelody.Play();
            PlayerController.animator.enabled = true;
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Paused()
    {
        if (pause)
        {
            backgroundMelody.Pause();
            PauseUnPause(true);
            newLevel = false;
            PlayerController.EnviromentMoves = false;
        }
        else
        {
            backgroundMelody.Play();
            PauseUnPause(false);
            if (PlayerController.helloEnded)
                PlayerController.EnviromentMoves = true;
        }
    }
    public void EndRunning()
    {
        SceneManager.LoadScene("scennnnn");
        newLevel = true;
        WorldController.speed = 4f;
        PlayerController.helloEnded = false;
        WorldController.speedOfAnim = 1.7f;
        WorldController.speedOfRun = 1.4f;
        PlayerController.coins = 0;
        pause = true;
    }

    public void MusicPause()
    {
        if (music)
        {
            MusicControllerButton.image.sprite = MusicOff;
            backgroundMelody.volume = 0;
            music = !music;
        }
        else
        {
            MusicControllerButton.image.sprite = MusicOn;
            backgroundMelody.volume = 0.2f;
            music = !music;
        }
    }

    public void EffectsPause()
    {
        if (effects)
        {
            EffectsControllerButton.image.sprite = EffectsOff;
            PlayerController.collisionSound.volume = 0;
            effects = !effects;
        }
        else
        {
            EffectsControllerButton.image.sprite = EffectsOn;
            PlayerController.collisionSound.volume = 0.2f;
            effects = !effects;
        }
    }
    public void PauseUnPause(bool c)
    {
        Background.SetActive(c);
        PlayerController.animator.enabled = !c;
        PlayerController.play = !c;
        pause = !c;
        EndRun.SetActive(c);
        if (!newLevel)
        {
            MusicController.SetActive(c);
            EffectsController.SetActive(c);
        }
    }
}
