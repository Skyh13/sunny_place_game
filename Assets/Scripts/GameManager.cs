using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    // Start is called before the first frame update
    void Start()
    {
        //if (Instance == null) {
        //	DontDestroyOnLoad(this);
        Instance = this;
        //}
        //else {
        //	Destroy(this.gameObject);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameOver" && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void PlayButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

    public void OptionsButtonPressed()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }

    public void BackButtonPressed()
    {
        SceneManager.LoadScene(1);
    }
}
