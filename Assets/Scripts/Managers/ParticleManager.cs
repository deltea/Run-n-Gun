using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public ParticleSystem bulletMiss;

    #region Singleton
    
    static public ParticleManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

}
