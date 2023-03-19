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

    public Buffs buff;
    public BuffClass buffClass;
    public string objectName;
    [TextArea] public string description;

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Player"))
        {
            Destroy(gameObject);
            print(objectName);
            BuffManager.Instance.ActivateBuff(buff);
        }
    }

}
