using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //gameObject for the panel
    public GameObject pauseMenu;
    public bool isPaused;
    
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //checks if user hit space. will pause if not yet, or resume if paused
        if(Input.GetKeyDown(KeyCode.Space)){
            if(isPaused){
                ResumeGame();
            } else{
                PauseGame();
            }
        }
        
    }

    //pauses the game
    public void PauseGame(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    //resumes it
    public void ResumeGame(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
