using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMovement : MonoBehaviour
{
    void Update()
    {
        transform.Translate(5 * Time.deltaTime * Vector3.left);
    }
}
