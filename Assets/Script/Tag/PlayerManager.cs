using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private SimpleCharacterMovement player1;
    [SerializeField] private SimpleCharacterMovement player2;
    [SerializeField] private StationCameraFollow cam;
    [SerializeField] private Vector3 positionOffset1;
    [SerializeField] private Vector3 positionOffset2;
    [SerializeField] private Vector3 rotationOffset1;
    [SerializeField] private Vector3 rotationOffset2;
    private bool isTaged = false;

    private void Start()
    {
        cam.SetCameraOffset(positionOffset1, rotationOffset1);
        player1.SetActive(true);
        player2.SetActive(false);
        cam.SetTarget(player1.transform);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SwitchTag();
        }
    }

    public void SwitchTag()
    {
        isTaged = !isTaged;
        cam.SetCameraOffset(isTaged ? positionOffset2 : positionOffset1, isTaged ? rotationOffset2 : rotationOffset1);
        player1.SetActive(!isTaged);
        player2.SetActive(isTaged);
        cam.SetTarget(isTaged ? player2.transform : player1.transform);
    }

    public bool GetTag()
    {
        return isTaged;
    }

}
