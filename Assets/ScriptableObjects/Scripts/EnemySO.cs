using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy", fileName = "New Enemy")]
public class EnemySO : ScriptableObject
{
    public GameObject enemyPrefab;
    
    [Header("Attributes")]
    public float speed;
    public int startHealth;
    public int damage;

    [Header("Extra Attributes")]
    public int minCoinReward;
    public int maxCoinReward;
}
