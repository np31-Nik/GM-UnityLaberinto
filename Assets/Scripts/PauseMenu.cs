using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public void Pause(){
        Debug.Log("Paused");
        if(Time.timeScale == 0){
            Time.timeScale = 1;
        }else{
            Time.timeScale = 0;
        }
    }
    public void UnPause(){
        Time.timeScale = 1;
    }
}
