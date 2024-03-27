using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelect : MonoBehaviour
{

    public GameObject loadingScreen;
    public TextMeshProUGUI loadText;

    public void Level1(){
        StartCoroutine(LoadLevel("level_1"));
    }
    public void Level2(){
        StartCoroutine(LoadLevel("level_2"));

    }
    public void Level3(){
        StartCoroutine(LoadLevel("level_3"));

    }
    public void Level4(){
        StartCoroutine(LoadLevel("level_4"));
    }
    public void Level5(){
        StartCoroutine(LoadLevel("level_5"));
    }
    public void MainMenu(){
        SceneManager.LoadScene("menu_main");
    }

    private IEnumerator LoadLevel(string level){
        AsyncOperation operation = SceneManager.LoadSceneAsync(level);
        loadingScreen.SetActive(true);

        while(!operation.isDone){
            float progress = Mathf.Round(operation.progress * 100);
            loadText.text = progress.ToString() + " %";
            yield return null;
        }
    }
    
}
