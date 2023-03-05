using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public float smoothing = 0.1f;
    public Transform player;

    void FixedUpdate() {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.position.x, smoothing), 0, -10);
    }

}
