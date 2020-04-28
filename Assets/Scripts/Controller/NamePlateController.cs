using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class NamePlateController : MonoBehaviour
{
    public MeshRenderer NameTextMesh;
    public TextMesh NameText;

    public void SetName(string name)
    {
        NameText.text = name;
    }
    public void SetColor(Color color)
    {
        NameText.color = color;
    }
    public void SetBgColor(Material mat)
    {
        NameTextMesh.material = mat;
    }
}
