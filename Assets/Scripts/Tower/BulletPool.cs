using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int size;
    private Queue<GameObject> bullets ;

    public void InitializePool()
    {
        bullets = new Queue<GameObject> { };

        for (int i = 0; i < size; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.SetActive(false);
            bullets.Enqueue(bullet);
        }
    }


    public GameObject GetBullet()
    {
        GameObject bullet = bullets.Dequeue();

        bullet.SetActive(true);
        bullets.Enqueue(bullet);

        return bullet;

    }
}

