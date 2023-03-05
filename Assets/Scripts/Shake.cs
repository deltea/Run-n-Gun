using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShakeMode
{
    Fixed,
    Dynamic
}

public class Shake : MonoBehaviour
{

    public ShakeMode shakeMode = ShakeMode.Fixed;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.7f;
    private float dampingSpeed = 1.0f;
    Vector3 initialPosition;

    void Start() {
        if (shakeMode == ShakeMode.Fixed) initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeMode == ShakeMode.Dynamic) initialPosition = transform.localPosition;
        if (shakeDuration > 0) {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.unscaledDeltaTime * dampingSpeed;
        } else {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void ShakeIt(float duration, float magnitude) {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }

}
