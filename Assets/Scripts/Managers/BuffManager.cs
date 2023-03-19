using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{

    public BuffObject[] buffObjects;
    public GameObject crystalPrefab;

    #region Singleton
    
    static public BuffManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    public void RandomBuff(out Buffs buff) {
        buff = (Buffs)Random.Range(0, System.Enum.GetValues(typeof(Buffs)).Length);
    }

    public void ActivateBuff(Buffs buff) {
        switch (buff)
        {
            case Buffs.Dart: { break; }
        }
    }

    public void SpawnCrystals(float spacing) {
        Instantiate(crystalPrefab, Vector3.up * 3 + Vector3.left * spacing, Quaternion.identity);
        Instantiate(crystalPrefab, Vector3.up * 3 + Vector3.right * spacing, Quaternion.identity);
    }

}
