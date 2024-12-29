using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOverController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Level finished by the player");
            if(LevelManager.Instance != null){
                LevelManager.Instance.MarkCurrentLevelComplete();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else{
                Debug.LogError("LevelManager instance is null. Ensure LevelManager is present in the scene.");
            }
            //SceneManager.LoadScene(2);
           
           
        }
    }
}
