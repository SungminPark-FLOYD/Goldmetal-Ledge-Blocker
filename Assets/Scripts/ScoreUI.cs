using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    Text mytext;

    void Start()
    {
        mytext = GetComponent<Text>();
    }


    void LateUpdate()
    {
        mytext.text = string.Format("{0:F0}",GameManager.score);
    }
}
