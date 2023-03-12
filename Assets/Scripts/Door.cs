using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Door connectedDoor;

    Transform player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void GoThrough() {
        connectedDoor.TeleportToDoor();
    }

    public void TeleportToDoor() {
        player.position = transform.position;
    }

}
