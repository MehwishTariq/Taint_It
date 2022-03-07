using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Sprite[] CountDown;
    public Image countDownImage;
    public TextMeshPro score;
    public GameObject completePanel, failPanel, StartPanel, ScorePanel;
    public int scoreNo = 0;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.onGameComplete += ShowCompletePanel;
        GameManager.instance.onGameFail += ShowFailPanel;
    }

    private void OnEnable()
    {
        GameManager.instance.onGameComplete -= ShowCompletePanel;
        GameManager.instance.onGameFail -= ShowFailPanel;
    }

    public void ShowCompletePanel()
    {
        failPanel.SetActive(false);
        ScorePanel.SetActive(false);
        StartPanel.SetActive(false);
        completePanel.SetActive(true);
    }

    public void ShowFailPanel()
    {
        ScorePanel.SetActive(false);
        completePanel.SetActive(false);
        StartPanel.SetActive(false);
        failPanel.SetActive(true);
    }

    public void HideStartPanel()
    {
        completePanel.SetActive(false);
        failPanel.SetActive(false);
        StartPanel.SetActive(false);
        countDownImage.gameObject.SetActive(true);
        StartCoroutine(CountDownStart());
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score", 0) + scoreNo);
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void SetScore()
    {
        score.GetComponent<RectTransform>().DOPunchScale(new Vector3(1.2f,1.2f,1.2f), 0.1f);
        score.text = scoreNo.ToString();
    }
    IEnumerator CountDownStart()
    {
        countDownImage.sprite = CountDown[0];
        yield return new WaitForSeconds(0.3f);
        countDownImage.sprite = CountDown[1];
        yield return new WaitForSeconds(0.3f);
        countDownImage.sprite = CountDown[2];
        yield return new WaitForSeconds(0.3f);
        countDownImage.gameObject.SetActive(false);
        ScorePanel.SetActive(true);
    }
}
