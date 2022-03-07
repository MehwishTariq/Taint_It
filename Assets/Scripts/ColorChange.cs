using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public static Color forReticle;
    void Start()
    {
        forReticle = new Color(Random.value, Random.value, Random.value, 1.0f);
        GetComponent<SpriteRenderer>().color = forReticle;
    }

}
