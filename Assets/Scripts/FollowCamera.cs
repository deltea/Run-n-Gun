using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public float smoothing = 0.1f;
    public Transform player;
    public Vector2 bounds;

    Camera cam;

    void Start() {
        cam = Camera.main;
    }

    void FixedUpdate() {
        float x = Mathf.Lerp(transform.position.x, player.position.x, smoothing);
        float y = Mathf.Lerp(transform.position.y, player.position.y, smoothing);

        float halfHeight = cam.orthographicSize;
        float halfWidth = cam.aspect * halfHeight;

        x = Mathf.Clamp(x, -bounds.x + halfWidth, bounds.x - halfWidth);
        y = Mathf.Clamp(y, -bounds.y + halfHeight, bounds.y - halfHeight);

        transform.position = new Vector3(x, y, -10);
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(transform.position, new Vector2(bounds.x * 2, bounds.y * 2));
    }

}
