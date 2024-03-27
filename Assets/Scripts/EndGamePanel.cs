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
            SceneManager.LoadScene(scene+1);
        }
    }





}
