using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollTexture : MonoBehaviour
{
    Material _material;

    public Vector2 scrollSpeed = new Vector2(0.5f, 0.5f);

    void Start()
    {
        _material = GetComponent<Image>().material;
    }

    void Update()
    {
        _material.mainTextureOffset = scrollSpeed * Time.time;
    }
}
