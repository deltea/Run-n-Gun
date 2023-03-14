using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{

    GameObject doorTouching;

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Door"))
        {
            doorTouching = trigger.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D trigger) {
        if (trigger.CompareTag("Door"))
        {
            doorTouching = null;
        }
    }

    void OnInteract() {
        if (doorTouching != null)
        {
            RoomsManager.Instance.CreateRoom();
        }
    }

}
