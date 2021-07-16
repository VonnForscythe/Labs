using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitGame();
            }

       
    }

    public void QuitGame()
    {
#if UNITY_EDITOR

        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
