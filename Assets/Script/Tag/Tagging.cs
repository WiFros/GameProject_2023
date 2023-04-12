using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tagging : MonoBehaviour
{
    [SerializeField] private StationCameraFollow cam;
    [SerializeField] private Vector3 positionOffset1;
    [SerializeField] private Vector3 positionOffset2;
    [SerializeField] private Vector3 rotationOffset1;
    [SerializeField] private Vector3 rotationOffset2;
    private bool isTaged;

    private void Start()
    {
        cam.SetCameraOffset(positionOffset1, rotationOffset1);
    }

    public void SwitchTag()
    {
        isTaged = !isTaged;
        cam.SetCameraOffset(isTaged ? positionOffset1 : positionOffset2, isTaged ? rotationOffset1 : rotationOffset2);
    }

    public bool GetTag()
    {
        return isTaged;
    }
}
