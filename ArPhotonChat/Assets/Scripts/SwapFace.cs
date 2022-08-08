using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SwapFace : MonoBehaviour
{
    private ARFaceManager faceManager;

    public List<Material> faceMaterial = new List<Material>();

    private int faceMaterialIndex = 0;

    void Start()
    {
        faceManager = GetComponent<ARFaceManager>();
    }

    public void SwitchFace()
    {
        foreach (ARFace face in faceManager.trackables)
        {
            face.GetComponent<Renderer>().material = faceMaterial[faceMaterialIndex];
        }

        faceMaterialIndex++;

        if (faceMaterialIndex == faceMaterial.Count)
        {
            faceMaterialIndex = 0;
        }
    }
}
