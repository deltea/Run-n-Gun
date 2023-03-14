using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    public float hoverScale = 1.2f;
    public float pressedScale = 0.8f;
    public float scaleSmoothing = 0.2f;

    [HideInInspector] public bool pressing = false;
    [HideInInspector] public bool hovering = false;

    private float targetScale = 1;

    void Start() {
        transform.localScale = Vector2.zero;
    }

    void Update() {
        if (pressing) targetScale = pressedScale;
        else
        {
            if (hovering) targetScale = hoverScale;
            else targetScale = 1;
        }

        transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one * targetScale, scaleSmoothing);
    }

    public void OnPointerDown(PointerEventData data) { pressing = true; }
    public void OnPointerUp(PointerEventData data) { pressing = false; }
    public void OnPointerEnter(PointerEventData data) { hovering = true; }
    public void OnPointerExit(PointerEventData data) { hovering = false; }

}
