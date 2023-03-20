using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteGraphics : MonoBehaviour
{

    private Color flashColor = new Color(42, 200, 238);
    private float flashSpeed = 0.2f;
    private float scaleSmoothing = 0.05f;
    private float bumpScale = 0.5f;

    SpriteRenderer graphic;
    Color originalColor;
    Vector2 originalScale;

    void Start() {
        graphic = GetComponent<SpriteRenderer>();

        if (graphic != null) originalColor = graphic.color;
        originalScale = transform.localScale;
    }

    void Update() {
        transform.localScale = Vector2.Lerp(transform.localScale, originalScale, scaleSmoothing);
    }

    public void Flash() {
        if (graphic != null)
        {
            StartCoroutine(FlashRoutine());
        }
    }

    public void Bump() {
        transform.localScale = originalScale + Vector2.one * bumpScale;
    }

    private IEnumerator FlashRoutine() {
        graphic.color = flashColor;
        yield return new WaitForSeconds(flashSpeed);
        graphic.color = originalColor;
    }

}
