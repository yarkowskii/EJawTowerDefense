using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Values", fileName = "New Game Values")]
public class GameValuesManager : ScriptableObject
{
    [Header("Variables")]
    public int slotsCount;
    public int playerStartHp;
    public float sellCoefficient;

    [Header("Holders")]
    public TowersSOHolder towersSOHolder;
    public EnemiesSOHolder enemiesSOHolder;
    public WavesSOHolder wavesSOHolder;

}
