using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _onTrigger;
    [SerializeField] private string _tagName;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_tagName))
        {
            _onTrigger?.Invoke();
        }
    }
}
