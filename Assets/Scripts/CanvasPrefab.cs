using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPrefab : MonoBehaviour
{

    Canvas canvas;

    void Start() {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = GameObject.FindGameObjectWithTag("UI Camera").GetComponent<Camera>();
    }

}
