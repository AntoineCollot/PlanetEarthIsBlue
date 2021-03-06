﻿using UnityEngine;
using System.Collections;

public class PostProcessDrawColor : MonoBehaviour
{
    [Range(0,1)]
    public float intensity;
    public Material material;

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (intensity == 0)
        {
            Graphics.Blit(source, destination);
            return;
        }

        material.SetFloat("_Cutoff", intensity);
        Graphics.Blit(source, destination, material);
    }
}