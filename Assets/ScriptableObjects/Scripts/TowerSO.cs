using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower", fileName = "New Tower")]
public class TowerSO : ScriptableObject
{
    [Header("Attributes")]
    public int buildPrice;
    public int range;
    public float shootInterval;
    public int damage;

    [Header("Extra Attributes")]
    public int poolSize;
    public float bulletSpeed;

    [Header("In-game variable")]
    public Sprite towerSprite;

    public GameObject towerPrefab;
    public GameObject bulletPrefab;


}
