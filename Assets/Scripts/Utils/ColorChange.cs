using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(MeshRenderer))]
public class ColorChange : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public float duration = .2f;

    public  Color startColor = Color.white;
    private Color finalColor;

    private void OnValidate()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        
    }
    private void Start()
    {
        finalColor = meshRenderer.materials[0].GetColor("_Color");
        LerpColor();
    }

    private void LerpColor()
    {
        meshRenderer.materials[0].SetColor("_Color", startColor);
        meshRenderer.materials[0].DOColor(finalColor, duration).SetDelay(1f);
    }
}
