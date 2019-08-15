using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSoundSettings : MonoBehaviour
{
    public static SaveSoundSettings Instance = null;
    public float musicVolume;
    public float sfxVolume;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
