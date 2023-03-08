using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Buffs
{
    SlowerEnemies,
    SlowerEnemyBullets,
    FasterPlayer,
    HigherJump
}

public enum Nerfs
{
    FasterEnemies,
    FasterEnemyBullets,
    SlowerPlayer,
    LowerJump
}

public class VariableManager : MonoBehaviour
{

    // Changable variables
    public float enemySpeed = 10;
    public float enemyBulletSpeed = 8;
    public float playerRunSpeed = 10;
    public float playerJumpHeight = 800;

    private Dictionary<Buffs, string> buffDescriptions = new Dictionary<Buffs, string>();
    private Dictionary<Nerfs, string> nerfDescriptions = new Dictionary<Nerfs, string>();

    static public VariableManager Instance = null;

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
        buffDescriptions.Add(Buffs.SlowerEnemies, "Enemies are slower");
        buffDescriptions.Add(Buffs.SlowerEnemyBullets, "Enemy bullets are slower");
        buffDescriptions.Add(Buffs.FasterPlayer, "Player is faster");
        buffDescriptions.Add(Buffs.HigherJump, "Player jumps higher");

        nerfDescriptions.Add(Nerfs.FasterEnemies, "Enemies are faster");
        nerfDescriptions.Add(Nerfs.FasterEnemyBullets, "Enemy bullets are faster");
        nerfDescriptions.Add(Nerfs.SlowerPlayer, "Player is slower");
        nerfDescriptions.Add(Nerfs.LowerJump, "Player jumps lower");
    }

    public void RandomBuff(out Buffs buff, out string buffDescription) {
        buff = (Buffs)Random.Range(0, System.Enum.GetValues(typeof(Buffs)).Length);
        buffDescription = buffDescriptions[buff].ToString();
    }

    public void RandomNerf(out Nerfs nerf, out string nerfDescription) {
        nerf = (Nerfs)Random.Range(0, System.Enum.GetValues(typeof(Nerfs)).Length);
        nerfDescription = nerfDescriptions[nerf].ToString();
    }

    public void ActivateBuff(Buffs buff) {
        switch (buff)
        {
            case Buffs.SlowerEnemies: { enemySpeed -= 1; break; }
            case Buffs.SlowerEnemyBullets: { enemyBulletSpeed -= 0.5f; break; }
            case Buffs.FasterPlayer: { playerRunSpeed += 2; break; }
            case Buffs.HigherJump: { playerJumpHeight += 100; break; }
        }
    }

    public void ActivateNerf(Nerfs nerf) {
        switch (nerf)
        {
            case Nerfs.FasterEnemies: { enemySpeed += 1; break; }
            case Nerfs.FasterEnemyBullets: { enemyBulletSpeed += 0.5f; break; }
            case Nerfs.SlowerPlayer: { playerRunSpeed -= 2; break; }
            case Nerfs.LowerJump: { playerJumpHeight -= 100; break; }
        }
    }

}
