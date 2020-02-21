using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShootingScript : MonoBehaviour
{


    private float lastShootTime;
    
    private bool canShooting;

    private TowerScript towerScript;

    private List<GameObject> currentEnemyTargets = new List<GameObject> { };
    private CircleCollider2D circleCollider;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        towerScript = GetComponent<TowerScript>();

    }


    private void Start()
    {
        SetupRange();
    }

    private void SetupRange()
    {
        circleCollider.radius = towerScript.mTowerSO.range / 10f;
    }

    private void FixedUpdate()
    {
        if (canShooting)
            Shooting();

    }

    private void Shooting()
    {

        if (Time.time > lastShootTime)
        {
            lastShootTime = Time.time + towerScript.mTowerSO.shootInterval;
            MakeShoot();

        }
    }

    private void MakeShoot()
    {
        GameObject bullet = towerScript.bulletsPool.GetBullet();
        bullet.transform.position = transform.position;
        bullet.SetActive(true);

        GameObject nearestTarget = GetNearestEnemy();
        BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        bulletScript.damage = towerScript.mTowerSO.damage;
        bulletScript.speed = towerScript.mTowerSO.bulletSpeed;
        bulletScript.Seek(nearestTarget.transform);

    }

    private GameObject GetNearestEnemy()
    {
        GameObject nearestTarget = currentEnemyTargets[0];

        foreach (var target in currentEnemyTargets)
        {
            if (Vector2.Distance(transform.position, target.transform.position) < Vector2.Distance(transform.position, nearestTarget.transform.position))
                nearestTarget = target;
        }

        return nearestTarget;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When enemy enter range it becomes target

        if (collision.CompareTag("Enemy") && !currentEnemyTargets.Contains(collision.gameObject))
        {

            currentEnemyTargets.Add(collision.gameObject);
            canShooting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // When enemy exit range it stop to be target

        if (collision.CompareTag("Enemy") && currentEnemyTargets.Contains(collision.gameObject))
        {
            currentEnemyTargets.Remove(collision.gameObject);

            if (currentEnemyTargets.Count == 0)
                canShooting = false;
        }

    }

    void OnDrawGizmosSelected()
    {
        // Visualization of range when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, towerScript.mTowerSO.range / 10f);
    }
}
