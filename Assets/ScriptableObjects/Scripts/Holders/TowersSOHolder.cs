using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Towers Holder", menuName = "Towers Holder")]
public class TowersSOHolder : ScriptableObject, IHolder<TowerSO>
{
    public List<TowerSO> towers;

    public List<TowerSO> Objects
    {
        get
        {
            return towers;
        }
    }
}
