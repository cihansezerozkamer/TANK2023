using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LEVEL", menuName = "ScriptableObjects/Level", order = 2)]
public class LevelData_SO : ScriptableObject
{
    public int EnemyNormal;
    public int EnemyBig;
    public int EnemyFast;
    public int EnemyGunner;
    public int ExplodeBonus;
    public int TimeBonus;
    public int ProtectBonus;
    public Dictionary<string, int> EnemyLevelData;
    public Dictionary<string, int> BonusLevelData;


    private void OnEnable()
    {
        EnemyLevelData = new Dictionary<string, int>
        {
            { "EnemyNormal", EnemyNormal },
            { "EnemyBig", EnemyBig },
            { "EnemyFast", EnemyFast },
            { "EnemyGunner", EnemyGunner }
        };
        BonusLevelData = new Dictionary<string, int>
        {
            { "ExplodeBonus", ExplodeBonus },
            { "TimeBonus", TimeBonus },
            { "ProtectBonus", ProtectBonus }
        };

    }
    
    
}
