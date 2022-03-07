using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] Levels;
    public static LevelManager instance;
    [Header("Count of enemies appearing at a time")]
    public int[] enemiesCount;
    public int currentLevel;


    private void Awake()
    {
        instance = this;
        currentLevel = PlayerPrefs.GetInt("LevelNo", 1);
        Levels[currentLevel - 1].SetActive(true);
    }

    private void Start()
    {
        GameManager.instance.onGameComplete += SetNextLevel;
        
    }
    public void SetNextLevel()
    {
        if (currentLevel == 5)
            currentLevel = 1;
        else
            currentLevel++;
        PlayerPrefs.SetInt("LevelNo", currentLevel);
        GameManager.instance.onGameComplete -= SetNextLevel;
    }

}
