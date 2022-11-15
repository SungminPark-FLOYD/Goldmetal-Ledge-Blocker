using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Rigidbody[] characters;
    public int type;

    Ledge ledge;
    void Start()
    {
        ledge = GetComponentInParent<Ledge>();
        
    }

    
    void LateUpdate()
    {
        if(transform.position.z == 4)
        {
            transform.Translate(0, 0, ledge.blockCount * ledge.blockSize* -1);
            Init();
        }
    }

    public void Init()
    {
        type = Random.Range(0, 3);

        for (int index=0; index < characters.Length; index++)
        {
            characters[index].gameObject.SetActive(type == index);
        }
        StartCoroutine(InitPhysics());
    }
    IEnumerator InitPhysics()
    {
        characters[type].isKinematic = true;
        yield return new WaitForFixedUpdate();

        characters[type].velocity = Vector3.zero;
        characters[type].angularVelocity = Vector3.zero;
        yield return new WaitForFixedUpdate();

        characters[type].transform.localPosition = Vector3.zero;
        characters[type].transform.localRotation = Quaternion.identity;
    }

    public bool Check(int selectType)
    {
        bool result = (type == selectType);

        if(result) 
            StartCoroutine(Hit());

        return result; //필수로 반환 해줘야함 
    }

    IEnumerator Hit()
    {
        characters[type].isKinematic = false;
        yield return new WaitForFixedUpdate();

        int ran = Random.Range(0, 2);
        Vector3 forceVec = Vector3.zero;
        Vector3 torqueVec = Vector3.zero;
        

        switch (ran)
        {
            case 0:
                forceVec = (Vector3.right + Vector3.up) * 3.5f;
                torqueVec = (Vector3.forward + Vector3.down) * 2.5f;
                characters[type].AddForce(forceVec, ForceMode.Impulse);
                characters[type].AddTorque(torqueVec, ForceMode.Impulse);
                break;
            case 1:
                forceVec = (Vector3.left + Vector3.up) * 3.5f;
                torqueVec = (Vector3.back + Vector3.up) * 2.5f;
                characters[type].AddForce(forceVec, ForceMode.Impulse);
                characters[type].AddTorque(torqueVec, ForceMode.Impulse);
                break;
        }
    }
}
