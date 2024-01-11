using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] float speed;
    void Update()
    {
        if(GameManager.Instance.isStarted && !PlayerController.isGameOver)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.left);
        }
    }
}
