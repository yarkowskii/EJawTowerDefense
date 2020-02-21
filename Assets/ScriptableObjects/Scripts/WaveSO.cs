using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Wave", fileName = "New Wave")]

public class WaveSO : ScriptableObject
{
    public EnemySO enemySO;

    [Header("Attributes")]
    public int count;
    public int duration;
    public float spawnRate;
}
