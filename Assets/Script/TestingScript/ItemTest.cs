using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTest : MonoBehaviour
{
    public float maxSpeed;

    Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(rigid.velocity.y > maxSpeed)
        {
            rigid.velocity = new Vector3(rigid.velocity.x, maxSpeed, rigid.velocity.z);
        }
    }
}
