using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class CaptureManager : MonoBehaviour
{
    [Header("TargetList")]
    public List<GameObject> captureTargetList;

    [Header("PhotoList")]
    public List<GameObject> photoList;

    [Header("TransList")]
    public List<GameObject> transCallbackList;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

        }
    }
}
