using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class EndGamePanel : MonoBehaviour
{

    public TextMeshProUGUI score;
    private int scene;
    public GameObject loadingScreen;
    public TextMeshProUGUI loadText;
    
    public void Start(){
        scene = SceneManager.GetActiveScene().buildIndex;
    }
    public void LevelSelect(){
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02F ;
        SceneManager.LoadScene("level_select");
    }
    public void Restart(){
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02F ;
        SceneManager.LoadScene(scene);
    }
    public void NextLevel(){
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02F ;
        if(scene == 6){
            SceneManager.LoadScene("level_select");
        }else{
            StartCoroutine(LoadLevel(scene+1));
        }
    }

    private IEnumerator LoadLevel(int level){
        AsyncOperation operation = SceneManager.LoadSceneAsync(level);
        loadingScreen.SetActive(true);

        while(!operation.isDone){
            float progress = Mathf.Round(operation.progress * 100);
            loadText.text = progress.ToString() + " %";
            yield return null;
        }
    }





}
