using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPivot : MonoBehaviour
{

    public Transform player;
    public float smoothing = 0.2f;

    void Update() {
        transform.position = Vector2.Lerp(transform.position, player.position, smoothing);
    }

}
