using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public GameValuesManager gameValuesManager;

    //Static varibles for easier controlling
    public static int Coins = 100;
    public static int Hp;
    public static int Wave = 0;

    private void Awake()
    {
        //Reset values every level
        Hp = gameValuesManager.playerStartHp;
        Coins = 100;
        Wave = 0;
    }


}
