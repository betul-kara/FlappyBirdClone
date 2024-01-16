using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRemove : MonoBehaviour
{
    void Update()
    {
        if (transform.position.x <= -17)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
