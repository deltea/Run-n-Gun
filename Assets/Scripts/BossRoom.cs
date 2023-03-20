using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{

    [SerializeField] private GameObject door;

    #region Singleton
    
    static public BossRoom Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        door.SetActive(false);
    }

    public void ShowDoor() {
        door.SetActive(true);
    }

}
