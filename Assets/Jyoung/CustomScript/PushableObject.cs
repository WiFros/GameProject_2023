using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : MonoBehaviour
{
    public Rigidbody rb;
    public float pushForce;
    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    public void Push(Vector3 direction)
    {
        rb.AddForce(direction * pushForce, ForceMode.Impulse);
    }
}
