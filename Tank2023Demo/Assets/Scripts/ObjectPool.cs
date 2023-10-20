using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    public IObjectPool<BulletController> BulletPool;
    public BulletController BulletPrefab;

    public IObjectPool<EnemyController> EnemyPool;
    public EnemyController EnemyPrefab;

    [Header("Pool Limit")]
    [SerializeField] private int _POOLLimit;
    private void Awake()
    {
        if (Instance != null && Instance == this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        BulletPool = new ObjectPool<BulletController>(CreateBullet, GetBullet, ReleaseBullet, DestroyBullet);
        EnemyPool = new ObjectPool<EnemyController>(CreateEnemy, GetEnemy, ReleaseEnemy, DestroyEnemy);
    }

    

    // Enemy:----------------------------------------------------------------
    private void DestroyEnemy(EnemyController obj)
    {
        Destroy(obj.gameObject);

    }

    private void ReleaseEnemy(EnemyController obj)
    {
        obj.gameObject.SetActive(false);

    }

    private void GetEnemy(EnemyController obj)
    {
        obj.gameObject.SetActive(true);

    }

    private EnemyController CreateEnemy()
    {
        EnemyController enemy = Instantiate(EnemyPrefab);
        enemy.transform.SetParent(transform);
        return enemy;
    }
    //Bullet:------------------------------------------------------------------
    private void DestroyBullet(BulletController obj)
    {
        Destroy(obj.gameObject);
   }

    private void ReleaseBullet(BulletController obj)
    {
        obj.gameObject.tag = "Untagged";
        obj.gameObject.SetActive(false);
        
    }

    private void GetBullet(BulletController obj)
    {
        obj.gameObject.SetActive(true);
    }

    private BulletController CreateBullet()
    {
        BulletController bullet = Instantiate(BulletPrefab);
        bullet.transform.SetParent(transform);
        return bullet;
    }
    
}
