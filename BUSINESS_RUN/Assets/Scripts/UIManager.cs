using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager UIinstance;
    public GameObject StartPanel;
    public GameObject FailPanel;
    public GameObject WinPanel;
    public GameObject GamePanel;
    public Timer timerScript;
    public Image TicketSkate;
    public Image TicketBike;
    public Image TicketMotorBike;

    private void Awake()
    {
        GameManager.isGameEnded = false;
        GameManager.isGameFailed = false;
        GameManager.isGameStarted = false;
        GameManager.isGameWined = false;

        UIinstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //-10 olmucaktı niye böyle oldu
    }
    public void StartButton()
    {
        GameManager.isGameStarted = true;
        GamePanel.SetActive(true);
        StartPanel.SetActive(false);
        WinPanel.SetActive(false);
        PathCreation.Examples.PathFollower.pathFollower.speed = PlayerController.playerController.Speed;
        PlayerController.playerController.anim.SetBool("Walk",true);
    }

    public void FailButton()
    {
        timerScript.GameTime(); 
        GameManager.isGameFailed = true;
        SceneManager.LoadScene("BusinessScene");
       

    }
    public void NextLevelButton()
    {
        LevelManager.levelManager.levelSize++;
        PlayerPrefs.SetInt("levelSize",LevelManager.levelManager.levelSize);

        PlayerPrefs.SetInt("level",LevelManager.levelManager.level);
        SceneManager.LoadScene(0);
    }
}
