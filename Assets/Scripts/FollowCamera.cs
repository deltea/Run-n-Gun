using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowCamera : MonoBehaviour
{

    public bool isFollowing = true;
    public float smoothing = 0.1f;
    public float mouseFollow = 1;
    public Vector2 bounds;

    Camera cam;
    Transform player;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start() {
        cam = Camera.main;
    }

    void FixedUpdate() {
        if (isFollowing)
        {
            float x = Mathf.Lerp(transform.position.x, player.position.x, smoothing);
            float y = Mathf.Lerp(transform.position.y, player.position.y, smoothing);

            float halfHeight = cam.orthographicSize;
            float halfWidth = cam.aspect * halfHeight;

            x = Mathf.Clamp(x, -bounds.x + halfWidth, bounds.x - halfWidth);
            y = Mathf.Clamp(y, -bounds.y + halfHeight, bounds.y - halfHeight);

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3 direction = mousePosition - transform.position;

            transform.position = new Vector3(x, y, -10) + direction.normalized * mouseFollow;
        }
    }

    public void ResetCamera() {
        transform.position = player.position;
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(transform.position, new Vector2(bounds.x * 2, bounds.y * 2));
    }

}
