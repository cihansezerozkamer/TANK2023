using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    [SerializeField]
    private Transform[] _SpawnPoints;
    [SerializeField] private List<LevelData_SO> _EnemyDataOnLevel;
    [SerializeField] private Dictionary<string, Enem_SO> Enemys;

    [SerializeField] private Enem_SO EnemyNormal;
    [SerializeField] private Enem_SO EnemyBig;
    [SerializeField] private Enem_SO EnemyFast;
    [SerializeField] private Enem_SO EnemyGunner;

    [SerializeField] private PlayerPrefs PlayerPrefs;
    [SerializeField] private int _LevelInfo = 0;

    [SerializeField] private List<EnemyController> ActiveEnemys;
    [SerializeField] private GameObject[] LevelPrefabs;
    public PlayerController player;
    public int EnemyCount=0;
    private void Awake()
    {
        
        Enemys = new Dictionary<string, Enem_SO>
        {
            { "EnemyNormal", EnemyNormal },
            { "EnemyBig", EnemyBig },
            { "EnemyFast", EnemyFast },
            { "EnemyGunner", EnemyGunner }
        };
        // BonusLevelData.Add("ExplodeBonus", ExplodeBonus);
        //BonusLevelData.Add("TimeBonus", TimeBonus);
        // BonusLevelData.Add("ProtectBonus", ProtectBonus);
       
        if (Instance != null && Instance == this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        PlayerPrefs.SetInt("ReachedLevel", 0);
        GameManager.Instance.OnStart += OnlevelStart;
    }
    public void OnlevelStart()
    {
        if (PlayerPrefs.GetInt("ReachedLevel") != _LevelInfo) _LevelInfo = PlayerPrefs.GetInt("ReachedLevel");
        StartCoroutine(SpawnEnemy(_LevelInfo));
       
        PlayerPrefs.Save();
        RecursiveResetObjects(LevelPrefabs[_LevelInfo]);
    }

    private System.Collections.IEnumerator SpawnEnemy(int levelinfo)
    {
        LevelPrefabs[_LevelInfo].SetActive(true);
        ReleaseAllEnemys();
        foreach (KeyValuePair<string, int> s in _EnemyDataOnLevel[levelinfo].EnemyLevelData)
        {
            
            if (s.Value != 0)
            {
                
                int EnemCounter = s.Value;
                while (EnemCounter-- > 0)
                {
                    EnemyCount++;
                    int RandomSP = Random.Range(0, _SpawnPoints.Length);
                    EnemyController enemy = ObjectPool.Instance.EnemyPool.Get();
                    ActiveEnemys.Add(enemy);
                    enemy.transform.position = _SpawnPoints[RandomSP].position;
                    enemy.GetComponent<EnemyData>().Enemy_TYPE = Enemys[s.Key];
                    yield return new WaitForSeconds(Random.Range(3f, 5f));

                }
            }
        }
       
        StopCoroutine(SpawnEnemy(levelinfo));
    }

    
    public void LevelIncrease()
    {
        LevelPrefabs[_LevelInfo].SetActive(false);
        _LevelInfo++;
        Debug.Log(_LevelInfo);
        PlayerPrefs.SetInt("ReachedLevel", _LevelInfo);
        
    }
    public void LevelChange()
    {
        LevelPrefabs[_LevelInfo].SetActive(true);
    }
    private void RecursiveResetObjects(GameObject parent)
    {
        parent.GetComponent<WallController>()?.ActivateMe();

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            GameObject child = parent.transform.GetChild(i).gameObject;
            RecursiveResetObjects(child);
        }
    }
    public int EnemyCounter()
    {
        EnemyCount = 0;
        foreach (KeyValuePair<string, int> s in _EnemyDataOnLevel[_LevelInfo].EnemyLevelData)
        {
            EnemyCount += s.Value;
        }
        return EnemyCount;
        
    }
    public void ReleaseAllEnemys()
    {
        foreach(EnemyController enemy in ActiveEnemys)
        {
            if(enemy.gameObject.activeSelf)ObjectPool.Instance.EnemyPool?.Release(enemy);
        }
    }
}
