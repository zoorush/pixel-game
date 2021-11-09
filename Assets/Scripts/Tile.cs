using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    void Update()
    {
        if (GameData.Target)
        {
            if (transform.position.y < GameData.Target.position.y - 5)
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}
