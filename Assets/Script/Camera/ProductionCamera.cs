using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionCamera : MonoBehaviour
{
    public GameObject sample;

    void Start()
    {

    }

    void Update()
    {
        transform.eulerAngles = new Vector3(Vector2.Angle(new Vector2(transform.position.x, transform.position.y), new Vector2(sample.transform.position.x, sample.transform.position.y)) * (-1), 0, 0);
    }
}
