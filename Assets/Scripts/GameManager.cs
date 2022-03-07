using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform[] camPos;
    public Transform viewCam, OpponentOnLevelDone;
    public GameObject confetti;
    public bool LevelComplete, LevelFail;
    public int numberOfKilled { get; set; }
    public int numberOfEnemies { get; set; }
    public event Action onGameStart, onGameComplete, onGameFail;
    public bool EnemyReached = false;
    public bool gameStarted = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LevelComplete = false;
        LevelFail = false;
        numberOfEnemies = LevelManager.instance.enemiesCount[LevelManager.instance.currentLevel - 1];
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameStarted)
            StartCoroutine(StartGame());
    }

    public IEnumerator StartGame()
    {
        ChangeView();
        UIManager.instance.HideStartPanel();
        yield return new WaitForSeconds(1f);

        onGameStart?.Invoke();
        gameStarted = true;

    }

    public void ChangeView()
    {
        viewCam.DOMove(camPos[1].position, 0.5f);
        viewCam.DORotate(camPos[1].eulerAngles, 0.5f);
    }

    public void ChangeViewBack()
    {
        viewCam.DOMove(camPos[0].position, 0.5f);
        viewCam.DORotate(camPos[0].eulerAngles, 0.5f);
    }
    public void GameComplete()
    {
        ChangeViewBack();
        OpponentOnLevelDone.gameObject.SetActive(true);
        OpponentOnLevelDone.GetComponent<Animator>().Play("Crying");
        AudioManager.instance.Play("ConfettiPop");
        confetti.SetActive(true);
        onGameComplete?.Invoke();
    }

    public void GameFailed()
    {
        ChangeViewBack();
        OpponentOnLevelDone.gameObject.SetActive(true);
        OpponentOnLevelDone.GetComponent<Animator>().Play("Dancing");
        onGameFail?.Invoke();
    }
}
