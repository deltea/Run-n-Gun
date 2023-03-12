using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{

    Door doorTouching;

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Door"))
        {
            doorTouching = trigger.GetComponent<Door>();
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
            doorTouching.GoThrough();
        }
    }

}
