using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public Button buttonPlay;
    public GameObject LevelSelection;
    private void Awake()
    {
        buttonPlay.onClick.AddListener(PlayGame);
        
    }

    private void PlayGame()
    {
       // SceneManager.LoadScene(1);
       LevelSelection.SetActive(true);  
       //FindObjectOfType<SoundManager>().Play("ButtonClick");
       Debug.Log("Button clicked");
       
    }

    
    



    /* [DllImport("__Internal")]
    private static extern void WebGLQuit();

    public void ReloadGame()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        WebGLQuit();
        #else
        Debug.Log("This function only works in WebGL builds.");
        #endif
    } */


}
