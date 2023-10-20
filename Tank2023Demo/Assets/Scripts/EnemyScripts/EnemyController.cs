using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemyMoveable
{
    public Rigidbody rb { get; set; }
    private EnemyData enemyData;
    private bool isTurning = false;
    private int previousRandomAngle = -1;
    private Coroutine turnCoroutine;
    public Transform muzzle;
    private float _fireTimer;
    [SerializeField] private float _AttackSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyData = GetComponent<EnemyData>();
        StartCoroutine(TurnCoroutine());
    }

    private void FixedUpdate()
    {
        MoveEnemy();
        _fireTimer += Time.fixedDeltaTime;

        _AttackSpeed = Random.Range(enemyData.Fire_Rate1, enemyData.Fire_Rate1 * 2);
        if (_fireTimer > _AttackSpeed && !isTurning)
        {
            EnemyAttack();
        }
        if (GameManager.Instance.MainMenu.activeSelf && this.gameObject.activeSelf)
        {
            ObjectPool.Instance.EnemyPool.Release(this);
        }
    }

    public void MoveEnemy()
    {
        if (!isTurning)
        {
            Vector3 movement = transform.forward * enemyData.MoveSpeed1 * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }
   

    private System.Collections.IEnumerator TurnCoroutine()
    {
        while (true)
        {
           
            yield return new WaitForSeconds(Random.Range(0.5f,2f)); // Wait for a random time between 1 to 3 seconds
            if (!isTurning)
            {
                isTurning = true;

                // Disable physics temporarily
                rb.isKinematic = true;
                int randomAngle;
                do
                {
                    randomAngle = Random.Range(0, 4) * 90;
                } while (randomAngle == previousRandomAngle);

                previousRandomAngle = randomAngle;
                 // Randomly choose 0, 90, 180, or 270 degrees
                Quaternion targetRotation = Quaternion.Euler(0, randomAngle, 0);
                float elapsedTime = 0f;
                float turnDuration = 0.7f;
                Quaternion startRotation = transform.rotation;

                while (elapsedTime < turnDuration)
                {
                    float t = elapsedTime / turnDuration;
                    transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                transform.rotation = targetRotation; // Ensure final rotation is exactly 90 degrees

                // Re-enable physics
                rb.isKinematic = false;

                isTurning = false;
            }
            
        }
    }


    private void EnemyAttack()
    {
       BulletController bullet = ObjectPool.Instance.BulletPool.Get();
       bullet.tag = "EnemyBullet";
       bullet.transform.localPosition = muzzle.transform.position;
       bullet.transform.localRotation = transform.rotation;
 
        _fireTimer = 0f;
    }
  


}
