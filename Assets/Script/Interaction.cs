using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public GameObject currentObject;
    public GameObject player;
    public float distance = 5f;
    public GameObject interactionUI;
    public Text interactionText;
    public LayerMask layerMask;
    public GameObject hitObject;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(player.transform.position, player.transform.forward);
    }
}
