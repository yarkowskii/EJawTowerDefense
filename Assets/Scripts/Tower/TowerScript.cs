using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{

    [HideInInspector] public BulletPool bulletsPool;

    [HideInInspector] public TowerSO mTowerSO;

    [Header("Managers")]
    public UIManager uiManager;

    

    public void InitializePool()
    {
        bulletsPool.size = mTowerSO.poolSize;
        bulletsPool.bulletPrefab = mTowerSO.bulletPrefab;
        bulletsPool.InitializePool();
    }

    


}
