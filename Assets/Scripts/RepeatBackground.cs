using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPosition;
    private float offset;

    void Start()
    {
        startPosition = transform.position;
        offset = GetComponent<Renderer>().bounds.size.x / 2;
    }

    void LateUpdate()
    {
        if (transform.position.x < startPosition.x - offset)
        {
            transform.position = startPosition;
        }
    }
}