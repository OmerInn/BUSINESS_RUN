using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Slider timerSlider;
    public Text timerText;
    public float gameTime;
    public bool stopTimer;
    public float time;
    int minutes, seconds;
    public bool keepTiming;
    private void Start()
    {
        stopTimer = false;
        keepTiming = true;
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
        time = gameTime;
    }
    private void Update()
    {
        if (GameManager.isGameStarted)
        {
            GameTime();
            GameFail();
        }

        
       /* if (GameManager.isGameEnded)
        {
            GameFinish();
        }*/
      
    }

   /* private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FinishTime"))
        {
            time = 30;
            stopTimer = false;
        }
    }*/
    public void GameTime()
    {
        if (stopTimer == false)
        {
            time = time - Time.deltaTime;
            timerSlider.value = time;
        }
        
         

    }
    public void GameFail()
    {
        if (time <=0)
        {
            stopTimer = true;
            // stopTimer = true;
            PathCreation.Examples.PathFollower.pathFollower.speed = 0;
            PlayerController.playerController.anim.SetBool("Walk", false);
            PlayerController.playerController.anim.SetBool("Defeated", true);
            GameManager.isGameEnded = true;
            GameManager.instance.OnlevelFailed();
        }
    }
    public void GameFinish()
    {
        if (time==0)
        {
            stopTimer = false;
            time = 0;
        }
    }
}
