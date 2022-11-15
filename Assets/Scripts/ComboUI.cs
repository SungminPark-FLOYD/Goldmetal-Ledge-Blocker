using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ComboUI : MonoBehaviour
{
    Text myText;

    void Start()
    {
        myText = GetComponent<Text>();
    }

    
    void LateUpdate()
    {
        myText.text = GameManager.combo + " Combo!";
    }
}
