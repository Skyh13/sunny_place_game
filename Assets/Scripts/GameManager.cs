using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public static GameManager Instance = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
