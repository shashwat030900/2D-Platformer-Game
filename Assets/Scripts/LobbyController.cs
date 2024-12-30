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
        FindObjectOfType<SoundManager>().Play("ButtonClick");
    }

    private void PlayGame()
    {
       // SceneManager.LoadScene(1);
       LevelSelection.SetActive(true);  
       
    }

    public void Quit(){
        //WEBGL.Quit();
    }
}
