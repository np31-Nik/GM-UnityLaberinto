using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public void Pause(){
        Debug.Log("Paused");
        Time.timeScale = 0;
    }
    public void UnPause(){
        Time.timeScale = 1;
    }
}
