using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonPress;
    public GameObject aboutPanel;
    public GameObject storePanel;
    
    public void PlayGame(){
        audioSource.PlayOneShot(buttonPress,1f);
        SceneManager.LoadScene("level_select");
    }
    public void QuitGame(){
        audioSource.PlayOneShot(buttonPress,1f);
        Application.Quit();
    }
    public void Store(){
        audioSource.PlayOneShot(buttonPress,1f);
        storePanel.SetActive(!aboutPanel.activeSelf);
    }
    public void Mute(){
        audioSource.PlayOneShot(buttonPress,1f);
        audioSource.mute = !audioSource.mute;
    }
    public void About(){
        audioSource.PlayOneShot(buttonPress,1f);
        aboutPanel.SetActive(!aboutPanel.activeSelf);

    }
}
