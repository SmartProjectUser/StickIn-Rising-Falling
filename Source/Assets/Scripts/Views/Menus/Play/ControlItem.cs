using org.stickin.controllers;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {
    private Vector2 _oldDelta;
    
    public void OnPointerDown(PointerEventData eventData) {
        _oldDelta = eventData.delta;
    }

    public void OnPointerUp(PointerEventData eventData) {
        GameplayController.Instance.Player.Move(Vector2.zero);
    }

    public void OnDrag(PointerEventData eventData) {
        GameplayController.Instance.Player.Move(eventData.delta / transform.lossyScale.x / 0.5f);
    }
}
