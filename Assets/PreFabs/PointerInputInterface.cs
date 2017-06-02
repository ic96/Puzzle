using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class PointerInputInterface : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public delegate void PointerEvent(PointerEventData eventData);
    public PointerEvent OnPointerDownEvent;
    public PointerEvent OnPointerUpEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnPointerDownEvent != null)
            this.OnPointerDownEvent(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnPointerUpEvent != null)
            this.OnPointerUpEvent(eventData);
    }
}
