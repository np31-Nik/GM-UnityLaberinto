using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMoving : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        Vector2 offsetVector = new Vector2(offset, 0);
        rend.material.mainTextureOffset = offsetVector;
    }
}
