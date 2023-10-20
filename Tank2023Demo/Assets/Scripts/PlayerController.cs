using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 100.0f;
    [SerializeField] private Transform _MUZZLE;
    [SerializeField] private float _rateOfFire = 0.5f;
    private float _fireTimer = 0f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent rotation due to physics collisions
    }

    private void Update()
    {
        _fireTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && _fireTimer > _rateOfFire)
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        
        if (PlayerData.Instance.isPlayerCanMove)
        {
            float moveDirection = Input.GetAxis("Vertical");
            float rotateDirection = Input.GetAxis("Horizontal");
            
            // Applying position directly to the Rigidbody for movement
            Vector3 movement = transform.forward * moveSpeed * moveDirection * Time.fixedDeltaTime;
            rb.velocity = movement;

            // Applying rotation directly to the Transform
            float turn = rotationSpeed * rotateDirection * Time.fixedDeltaTime;
            transform.Rotate(Vector3.up, turn);
        }
    }

    void Shoot()
    {
        BulletController bullet = ObjectPool.Instance.BulletPool.Get();
        bullet.transform.position = _MUZZLE.position;
        bullet.transform.rotation = _MUZZLE.rotation;
        _fireTimer = 0f;
    }
   
 
}
