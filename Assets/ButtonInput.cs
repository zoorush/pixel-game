using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonInput : MonoBehaviour,IPointerClickHandler,IPointerDownHandler,IPointerUpHandler
{
    private Player _player;
    [SerializeField] private bool _pressed = false;
    [SerializeField] private BonusType _type;
    public enum BonusType
    {
        Jump,Tackle,Speed
    }

    IEnumerator Start()
    {
        while (!_player)
        {
            _player = FindObjectOfType<Player>();
            yield return null;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_pressed)
        {
            return;
        }
        switch (_type)
        {
            case BonusType.Jump:
                _player.JumpBonus();
                break;
            case BonusType.Tackle:
                _player.TackleBonus();
                break;
        }
    }
    private bool _holdingButton = false;
    private void Update()
    {
        if (_holdingButton)
        {
            switch (_type)
            {
                case BonusType.Speed:
                    _player.ActivateSpeedBonus();
                    break;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_pressed)
        {
            return;
        }
        _holdingButton = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _holdingButton = false;
    }
}
