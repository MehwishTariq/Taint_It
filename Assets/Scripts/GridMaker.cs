using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour
{
    public GameObject maskSquare;
    
    Sprite image;
    int completeImage;
    public int rows, column;

    private void Awake()
    {
        GameManager.instance.LevelComplete = false;
        GameManager.instance.LevelFail = false;
    }

    public void CheckImage()
    {
        completeImage = 0;
        image = GetComponent<SpriteRenderer>().sprite;
        foreach(Transform x in transform)
        {
            if (x.GetComponent<SpriteMask>().enabled)
            {
                completeImage++;
            }
            if(!x.GetComponent<SpriteMask>().enabled && !x.GetComponent<BoxCollider>().enabled)
            {
                x.GetComponent<BoxCollider>().enabled = true;
            }
        }
        if(completeImage == transform.childCount)
        {
            GameManager.instance.LevelComplete = true;
            GameManager.instance.LevelFail = false;
        }
        else if(completeImage == 1 && GameManager.instance.EnemyReached)
        {
            Debug.Log("failee;");
            GameManager.instance.LevelComplete = false;
            GameManager.instance.LevelFail = true;
        }
    }

    private void Update()
    {
        if (!GameManager.instance.LevelComplete && !GameManager.instance.LevelFail)
            CheckImage();
       
    }

    [ContextMenu ("Create Grid")]
    public void CreateGrid()
    {
        image = GetComponent<SpriteRenderer>().sprite;
        for(int i = 0; i < column ; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject  mask = Instantiate(maskSquare,transform);
                mask.transform.localPosition = new Vector3(-1.29f + (0.39f * i), 0.853f - (0.38f * j), 0);
                mask.transform.localRotation = Quaternion.identity;

            }
        }
    }
}
