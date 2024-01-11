using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isStarted;
    private void Start()
    {
        Instance = this;
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isStarted = true;
        }
    }
}
