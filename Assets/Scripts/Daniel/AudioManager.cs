using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [Header("----- Audio Source -----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----- Audio Clip -----")]
    public AudioClip walking;
    public AudioClip jump;
    public AudioClip pickupItem;
    public AudioClip dropItem;
    public AudioClip grapple;
    public AudioClip miniGameOneCorrectAnswer;
    public AudioClip buttonPressed;

    [Header("----- Background Music -----")]
    public AudioClip gameBackground;
    public AudioClip mainMenuBackground;
    public AudioClip creditsBackground;
    public AudioClip endBackground;

    private Dictionary<string, AudioClip> sceneBackgroundMusic = new Dictionary<string, AudioClip>();
    private static AudioManager instance;


    public void Awake()
    {
        if (instance == null)
        {
            // If not, set the instance to this
            instance = this;
            // Ensure this instance persists across scenes
            DontDestroyOnLoad(gameObject);


            //musicSource.volume = 0.40f;


            SFXSource.volume = 0.25f;
            sceneBackgroundMusic.Add("HospitalMap 1", gameBackground);
            sceneBackgroundMusic.Add("MainMenu", mainMenuBackground);
            sceneBackgroundMusic.Add("Credits", creditsBackground);
            sceneBackgroundMusic.Add("GameOver", endBackground);
            // Subscribe to the scene loaded event
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene exists in the sceneBackgroundMusic dictionary
        if (sceneBackgroundMusic.ContainsKey(scene.name))
        {

            if (scene.name == "HospitalMap 1")
            {
                musicSource.volume = 0.2f; // Set volume to 10%
            }
            else
            {
                musicSource.volume = 0.4f; // Set volume to default (40%)
            }

            // Set the background music for the loaded scene
            musicSource.clip = sceneBackgroundMusic[scene.name];
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}