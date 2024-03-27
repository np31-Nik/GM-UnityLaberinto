using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void Start(){

    }
    public void Level1(){
        SceneManager.LoadScene("level_1");
    }
    public void Level2(){
        SceneManager.LoadScene("level_2");

    }
    public void Level3(){
        SceneManager.LoadScene("level_3");

    }
    public void Level4(){
        SceneManager.LoadScene("level_4");
    }
    public void Level5(){
        SceneManager.LoadScene("level_5");
    }
    public void MainMenu(){
        SceneManager.LoadScene("menu_main");
    }
    
}
