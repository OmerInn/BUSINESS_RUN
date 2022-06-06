using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [Tooltip("level degerini tutar")]
    public int level;
    [Tooltip("levelin ground tutan liste")]
    public List<GameObject> LevelGround;
    public TextMeshProUGUI LevelUp;
    public int levelSize;
    public static LevelManager levelManager;
    private void Awake()
    { 
        levelManager = this;
    }
    // Start is called before the first frame update
    void Start() 
    {       
        #region level islemleri
        level = PlayerPrefs.GetInt("level");
        LevelUp.text = level + levelSize.ToString();
        levelSize = PlayerPrefs.GetInt("levelSize",1);
        if (level >= 3 ) { level = 0;} 
        LevelGround[level].SetActive(true);
        level++;
        #endregion
        LevelUp.text = "Level  " + levelSize.ToString();
    }
}
