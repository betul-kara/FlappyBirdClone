using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] float destinationPoint;
    [SerializeField] float returnPoint;
    [SerializeField] float height;
    void Update()
    {
        if (transform.position.x <= destinationPoint)
        {
            transform.position = new Vector3(returnPoint, height, 0);
        }
    }
}