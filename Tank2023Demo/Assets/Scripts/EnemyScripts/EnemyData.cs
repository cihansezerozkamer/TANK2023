using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour , IDamagable
{
    

                     public Enem_SO Enemy_TYPE;

    [SerializeField] private int MoveSpeed;
    [SerializeField] private int Health;
    [SerializeField] private float Fire_Rate;
    [SerializeField] private Material TankColor;
    [SerializeField] private Vector3 heightDifference;
    [SerializeField] private Vector3 Scale;
    [SerializeField] private Vector3 SpawnPoint;
    [SerializeField] private Vector3 muzzle;


    public int Health1 { get => Health; set => Health = value; }
    public float Fire_Rate1 { get => Fire_Rate;  }
    public Material TankColor1 { get => TankColor;  }
    public Vector3 Y1 { get => heightDifference; }
    public Vector3 Scale1 { get => Scale;  }
    public Vector3 SpawnPoint1 { get => SpawnPoint; }
    public int MoveSpeed1 { get => MoveSpeed;  }
    public Vector3 Muzzle { get => muzzle; }

    private void Start()
    {
        
        InitEnemyData();
        ChangeMaterialRecursively(transform);
    }
    void ChangeMaterialRecursively(Transform trans)
    {
        Renderer renderer = trans.GetComponent<Renderer>();

        if (renderer != null && TankColor1 != null)
        {
            renderer.material = TankColor1;
        }
        foreach (Transform child in trans)
        {
            ChangeMaterialRecursively(child);
        }
    }
    void InitEnemyData()
    {
        Scale = Enemy_TYPE.Scale;
        Health = Enemy_TYPE.Health;
        Fire_Rate = Enemy_TYPE.Fire_Rate;
        TankColor = Enemy_TYPE.TankColor;
        this.GetComponentInChildren<Transform>().localScale = Scale1;
        Vector3 wheelUP = new(transform.position.x, Enemy_TYPE.Y.y, transform.position.z);
        heightDifference = wheelUP;
        transform.position = wheelUP;
        MoveSpeed =Enemy_TYPE.MoveSpeed;
    }

    public void Damage(int damageAmount)
    {
        Health -= damageAmount;
        if (Health <= 0) Die();

    }

    public void Die()
    {
        ObjectPool.Instance.EnemyPool.Release(gameObject.GetComponent<EnemyController>());
        if(Health >1)GameManager.Instance.OnEnemyKilled(100);
        else GameManager.Instance.OnEnemyKilled(50);
        GameManager.Instance.KillCount++;

        if (LevelManager.Instance.EnemyCounter() == GameManager.Instance.KillCount)
        {
            GameManager.Instance.ScoreBoard();
        }
           
        
    }
 



}
