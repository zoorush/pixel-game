using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent _onCalled;
    public void Call()
    {
        _onCalled?.Invoke();
    }
}
