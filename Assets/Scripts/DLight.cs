using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLight : MonoBehaviour
{
    public float changSpeed;
    void LateUpdate()
    {
        if(GameManager.isGameOver)
            return;
        transform.Rotate(Vector3.right * Time.deltaTime * changSpeed);
    }
}
