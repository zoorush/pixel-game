using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventPlayer : MonoBehaviour
{
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private Player _player;
    public void DeactivateCollider()
    {
        //_collider2D.enabled = false;
    }
    public void ActivateCollider()
    {
        //_collider2D.enabled = true;
    }

    public void PerformAction()
    {
        _player.PerformAction(true);
    }
    public void StopAction()
    {
        _player.PerformAction(false);
    }
}
