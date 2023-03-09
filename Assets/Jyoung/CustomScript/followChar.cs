using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followChar : MonoBehaviour
{
    public Transform character;
    // Update is called once per frame
    void Update()
    {
        if(gameObject.active)
            transform.position = character.position;

    }
}
