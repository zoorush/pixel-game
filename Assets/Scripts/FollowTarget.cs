using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private Vector3 _offset;
    void Start()
    {
        _offset = transform.position - GameData.Target.position;
    }

    private void LateUpdate()
    {
        if (!GameData.Target) return;
        Vector3 nextPosition = GameData.Target.position + _offset;
        nextPosition.x = transform.position.x;
        transform.position = nextPosition;
    }
}
