using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void Start(){

    }
    public void Level1(){
        //SceneManager.LoadScene("level1");
        SceneManager.LoadScene("test_gems");
    }
    public void Level2(){
        //SceneManager.LoadScene("level2");
        SceneManager.LoadScene("test_ground");
    }
    public void Level3(){
        SceneManager.LoadScene("level3");
    }
    public void Level4(){
        SceneManager.LoadScene("level4");
    }
    public void Level5(){
        SceneManager.LoadScene("level5");
    }
    public void MainMenu(){
        SceneManager.LoadScene("menu_main");
    }
    
}
