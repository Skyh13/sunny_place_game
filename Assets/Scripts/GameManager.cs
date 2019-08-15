using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public AudioClip titleMusic;
    AudioSource backgroundMusic = null;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) {
        	DontDestroyOnLoad(this);
            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else {
        	Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameOver" && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }

        if (SceneManager.GetActiveScene().name == "Title") {
            if (backgroundMusic == null) {
                backgroundMusic = SoundManager.Instance.PlayMusic(titleMusic);
            }
        }
    }

    public void PlayButtonPressed()
    {
        SceneManager.LoadScene(2);
    }

    public void OptionsButtonPressed()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }

    public void BackButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(3);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Title") {
            if (backgroundMusic == null) {
                backgroundMusic = SoundManager.Instance.PlayMusic(titleMusic);
            }
            GameObject.Find("Play Button").GetComponent<Button>().onClick.AddListener(PlayButtonPressed);
            GameObject.Find("Options Button").GetComponent<Button>().onClick.AddListener(OptionsButtonPressed);
            GameObject.Find("Quit Button").GetComponent<Button>().onClick.AddListener(QuitButtonPressed);
        }

        if (scene.name == "Options") {
            GameObject.Find("Back Button").GetComponent<Button>().onClick.AddListener(BackButtonPressed);
        }
    }
}
