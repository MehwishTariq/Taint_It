using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{

    public Transform[] newPos;
    public GameObject EnemyPrefab;
 
    int numberOfEnemiesAtTime;

    private void OnEnable()
    {
        GameManager.instance.onGameComplete -= RemoveAllEnemies;
        GameManager.instance.onGameFail -= RemoveAllEnemies;
    }
    private void Start()
    {
        numberOfEnemiesAtTime = GameManager.instance.numberOfEnemies;
        GameManager.instance.onGameComplete += RemoveAllEnemies;
        GameManager.instance.onGameFail += RemoveAllEnemies;
        CreateInstance();
    }

    public void CreateInstance()
    {
        GameManager.instance.numberOfKilled = 0;
        for (int i = 0; i < numberOfEnemiesAtTime; i++)
        {
            Instantiate(EnemyPrefab, newPos[Random.Range(0, 2)].position, Quaternion.identity, gameObject.transform);
        }
    }

    public void RemoveAllEnemies()
    {
        foreach (Transform x in transform)
            x.gameObject.SetActive(false);
    }
}
