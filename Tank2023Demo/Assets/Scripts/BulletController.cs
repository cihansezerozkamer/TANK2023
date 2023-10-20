using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField]
    private int bulletSpeed=10;
    private Rigidbody _RB;
    void FixedUpdate()
    {
        _RB = GetComponent<Rigidbody>();
        _RB.velocity = bulletSpeed * Time.fixedDeltaTime * transform.forward;
        _RB.angularVelocity = Vector3.zero;
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<EnemyController>() != null && !this.gameObject.CompareTag("EnemyBullet")) 
        {
            other.gameObject.GetComponent<EnemyData>().Damage(1);
        }
        
        if (other.gameObject.GetComponent<WallController>() != null )
        {
            other.gameObject.GetComponent<WallController>().DeActivate();
            
        }
        if (other.gameObject.GetComponent<PlayerController>() != null && this.gameObject.CompareTag("EnemyBullet"))
        {
            PlayerData.Instance.Damage();
        }
        if(this.gameObject.activeSelf) ObjectPool.Instance.BulletPool.Release(this);
    }


}
