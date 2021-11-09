using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private UnityEvent _onCalledAnimationEvent;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("tutorial",1) == 1)
        {
            _animator.SetTrigger("firstTime");
        }
        else
        {
            _animator.SetTrigger("startDirectly");
        }
    }

    public void StartEvent()
    {
        _onCalledAnimationEvent?.Invoke();
    }
}
