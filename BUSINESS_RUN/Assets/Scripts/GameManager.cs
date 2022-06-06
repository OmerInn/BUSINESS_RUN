using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    #region Oyun degiskenleri
    public static bool isGameStarted = false;
    public static bool isGameEnded = false;
    public static bool isGameWined = false;
    public static bool isGameFailed = false;
    public  bool isInputEnabled;
    public static GameManager instance;
    #endregion
    public TextMeshProUGUI CoinText;
    public int Coin;
    // Start is called before the first frame update
    private void Awake()
    {
        isGameStarted = false;
        isGameEnded = false;
        isGameWined = false;
        isGameFailed = false;
        instance = this;

    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void CoinAdd()
    {
        Coin += 5;
        CoinText.text = Coin.ToString();   
    }
    public void CoinMinus(int size)
    {
        if (Coin>size)
        {
            Coin -= size;
            CoinText.text = Coin.ToString();
        } // finishe geldiğimde süreyi 0 lıcam       
    }
    #region Oyun ilerleme islemleri
    public void OnGameStart()
    {
        if (isGameStarted == false && isGameEnded == false)
        {
            isGameStarted = true;
            isGameEnded = false;
        }
    }
    public void OnGameEnded()
    {
        if (isGameEnded == false && isGameStarted == true)
        {
            isGameEnded = true;
            isGameStarted = false;

        }
        if (isGameEnded && isGameFailed) OnlevelFailed();
        if (isGameEnded && isGameWined) OnlevelCompleted();
    }
    #endregion
    /// <summary>
    /// Oyun kazanildigi zaman calisir
    /// </summary>
    public void OnlevelCompleted()
    {
        if (isGameEnded == true && isGameWined == true)
        {
            UIManager.UIinstance.WinPanel.SetActive(true);
            UIManager.UIinstance.StartPanel.SetActive(false);
            UIManager.UIinstance.GamePanel.SetActive(false);

        }

    }
    /// <summary>
    /// Oyun kaybedildigi zaman calisir
    /// </summary>
    public void OnlevelFailed()
    {
        PathCreation.Examples.PathFollower.pathFollower.speed = 0;
        PlayerController.playerController.anim.Play("Defeated");
        isGameEnded = true;
        isGameFailed = true;
        isGameStarted = false;
        UIManager.UIinstance.GamePanel.SetActive(false);
        UIManager.UIinstance.FailPanel.SetActive(true);       
    }
}
