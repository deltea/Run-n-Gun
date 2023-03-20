using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Buffs
{
    Dart,
}

public enum BuffClass
{
    Helper
}

public class BuffObject : MonoBehaviour
{

    [SerializeField] private float hoverSpeed = 1;
    [SerializeField] private float hoverMagnitude = 1;
    public Buffs buff;
    public BuffClass buffClass;
    public string objectName;
    [TextArea] public string description;

    void Update() {
        transform.position = Vector2.up * Mathf.Sin(Time.time * hoverSpeed) * hoverMagnitude;
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Player"))
        {
            Destroy(gameObject);
            print(objectName);
            BuffManager.Instance.ActivateBuff(buff);
        }
    }

}
