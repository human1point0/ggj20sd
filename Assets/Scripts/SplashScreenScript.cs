using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreenScript : MonoBehaviour
{
    public Button[] Buttons;
    // Start is called before the first frame update
    void Start()
    {
        Buttons[0].Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonCallback()
    {
        //load level here
        SceneManager.LoadScene(1);
    }

    public void ExitButtonCallback()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
