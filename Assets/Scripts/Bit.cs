using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bit : MonoBehaviour
{

    public float startForce = 5;
    public float smoothing = 0.05f;
    public Vector2 range;

    private bool collected = false;

    Rigidbody2D bitBody;
    Collider2D bitCollider;
    SpriteGraphics graphics;
    Transform cam;

    void Start() {
        bitBody = GetComponent<Rigidbody2D>();
        bitCollider = GetComponent<Collider2D>();
        graphics = GetComponent<SpriteGraphics>();
        cam = Camera.main.transform;

        bitBody.AddForce(transform.up * startForce, ForceMode2D.Impulse);
    }

    void Update() {
        if (collected)
        {
            Vector3 bitTextPos = BitManager.Instance.bitText.transform.position + cam.position;
            transform.position = Vector2.Lerp(transform.position, bitTextPos, smoothing);
            if (
                transform.position.x > bitTextPos.x - range.x && 
                transform.position.x < bitTextPos.x + range.x &&
                transform.position.y > bitTextPos.y - range.y &&
                transform.position.y < bitTextPos.y + range.y
            )
            {
                BitManager.Instance.GainBits(1);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void Collect() {
        graphics.Flash();
        graphics.Bump();
        bitBody.velocity = Vector2.zero;
        bitBody.isKinematic = true;
        bitCollider.enabled = false;
        collected = true;
    }

}
