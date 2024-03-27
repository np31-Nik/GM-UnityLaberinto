using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausedMenu;
    public GameObject pauseButton;


    public void Pause(){
        Debug.Log("pausing");
        Time.timeScale = 0;
        pausedMenu.SetActive(true);
        pauseButton.SetActive(false);
    }
    public void UnPause(){
        Debug.Log("unpausing");
        Time.timeScale = 1;
        pausedMenu.SetActive(false);
        pauseButton.SetActive(true);
    }
}
