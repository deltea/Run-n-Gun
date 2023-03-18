using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Buffs
{
    Helper,
}

public class BuffManager : MonoBehaviour
{

    public GameObject crystalPrefab;

    private Dictionary<Buffs, string> buffDescriptions = new Dictionary<Buffs, string>();

    #region Singleton
    
    static public BuffManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        buffDescriptions.Add(Buffs.Helper, "Summon a helper to attack enemies");
    }

    public void RandomBuff(out Buffs buff, out string buffDescription) {
        buff = (Buffs)Random.Range(0, System.Enum.GetValues(typeof(Buffs)).Length);
        buffDescription = buffDescriptions[buff].ToString();
    }

    public void ActivateBuff(Buffs buff) {
        switch (buff)
        {
            case Buffs.Helper: { break; }
        }
    }

    public void SpawnCrystals(float spacing) {
        Instantiate(crystalPrefab, Vector3.up * 3 + Vector3.left * spacing, Quaternion.identity);
        Instantiate(crystalPrefab, Vector3.up * 3 + Vector3.right * spacing, Quaternion.identity);
    }

}
