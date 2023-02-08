using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC;

public class PushInteration : MonoBehaviour
{
    public Vector3 dic = new Vector3();
    public Transform transForm;
    public int direction;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        direction = AC.Kick
    }
    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject;
        if (collision.gameObject.name == "Player")
        {
            dic = gameObject.transform.position - player.transform.position;
        }
    }
}
