using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]  
    public class LevelLoader : MonoBehaviour
    {
        private Button button;
        public string LevelName;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(onClick);
            
        }

        private void onClick()
        {
            LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(LevelName);
             //FindObjectOfType<SoundManager>().Play("ButtonClick");
            switch (levelStatus)
        {

            case LevelStatus.Locked:
                Debug.Log("Can't play this level untill you unlock this level");
                
                break;

            case LevelStatus.Unlocked:
                SceneManager.LoadScene(LevelName);  
                break;

            case LevelStatus.Completed:
                SceneManager.LoadScene(LevelName);
                break;

        }
            //SceneManager.LoadScene(LevelName);  
        }
}
