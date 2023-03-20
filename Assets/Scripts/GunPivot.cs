using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPivot : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private float smoothing = 0.2f;

    void FixedUpdate() {
        transform.position = Vector2.Lerp(transform.position, player.position, smoothing);
    }

}
