using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour,IamDamagable
{
    public int Health;
    public bool isPlayerCanMove=false;

    public static PlayerData Instance { get; private set; }
    public Transform PlayerSpawn;

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
    }
        public void Damage()
    {
        Health -= 1;
        if (this.Health <= 0)
        {
            StartCoroutine(Die());
        }
      
        GameManager.Instance.OnHealthChange();
    }
    private void Start()
    {
        GameManager.Instance.OnStart += ResetPosition;

    }

    public void SetHealth(int health)
    {
       this.Health = health;
        
    }
    private void ResetPosition()
    {
        transform.position = PlayerSpawn.position;
        transform.rotation = PlayerSpawn.rotation;
    }
  

    public System.Collections.IEnumerator Die()
    {
        
        this.gameObject.GetComponent<ParticleSystem>().Play();
        isPlayerCanMove = false;
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
        transform.rotation = PlayerSpawn.rotation;
        this.gameObject.transform.position = PlayerSpawn.position;
        GameManager.Instance.GameOver();
        this.gameObject.GetComponent<ParticleSystem>().Stop();
        StopCoroutine(Die());
    }

}
