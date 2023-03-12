using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Buffs
{
    Helper,
}

public class BuffManager : MonoBehaviour
{

    // Changable variables

    private Dictionary<Buffs, string> buffDescriptions = new Dictionary<Buffs, string>();

    static public BuffManager Instance = null;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        // Populate descriptions
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

}
