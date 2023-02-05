using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public void SwitchColor(float r, float g, float b)
    {
        var color = new Color(r / 255, g / 255, b / 255);
        MeshRenderer meshRenderer = transform.GetComponent<MeshRenderer>();
        meshRenderer.material.color = color;
    }
}
