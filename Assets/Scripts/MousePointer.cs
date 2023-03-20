using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MousePointer : MonoBehaviour
{

    [SerializeField] private float normalSize = 1;
    [SerializeField] private float clickingSize = 0.5f;

    Camera cam;

    void Start() {
        cam = Camera.main;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update() {
        transform.position = Vector2.Lerp(transform.position, cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()), 0.5f);

        if (Mouse.current.leftButton.isPressed)
        {
            transform.localScale = Vector2.one * clickingSize;
        } else {
            transform.localScale = Vector2.one * normalSize;
        }
    }

}
