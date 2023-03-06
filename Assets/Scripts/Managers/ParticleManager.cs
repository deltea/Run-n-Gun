using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public ParticleSystem bulletMiss;
    public ParticleSystem enemySplat;

    #region Singleton
    
    static public ParticleManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    public void PlayParticle(ParticleSystem particles, Vector2 position, Quaternion rotation) {
        if (particles)
        {
            Destroy(Instantiate(particles.gameObject, position, rotation), particles.main.startLifetime.constant);
        }
    }

}
