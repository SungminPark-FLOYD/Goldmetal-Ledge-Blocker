    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    public int blockCount;
    public float blockSize;
    public int nowBlock;

    Block[] blocks;

    void Start()
    {
        

        blocks = GetComponentsInChildren<Block>();
        
    }

    
    public void Align()
    {
        blockCount = blocks.Length;

        if(blockCount == 0)
        {
            Debug.Log("not founded blocks!");
            return;
        }

        blockSize = blocks[0].GetComponentInChildren<BoxCollider>().transform.localScale.z;

        for (int index=0; index < blockCount; index++)
        {
            blocks[index].transform.Translate(0, 0, index * blockSize * -1);
            blocks[index].Init();
        }
    }
    IEnumerator Move()
    {
        float nextZ = transform.position.z + 2;

        while(transform.position.z < nextZ)
        {
            transform.Translate(0, 0, Time.deltaTime * 15f);
            yield return null;
        }

        transform.position = Vector3.forward * nextZ;
       
        
    }
    
    public void Select(int selectType)
    {
        bool result = blocks[nowBlock].Check(selectType);
        if(result) //정답
        {
            GameManager.Success();
            StartCoroutine(Move());

            nowBlock = (nowBlock + 1) % blockCount;
        }
        else  //오답
        {
            GameManager.Fail();
        }
        
    }
}
