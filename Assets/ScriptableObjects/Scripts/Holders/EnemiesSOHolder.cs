using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemies Holder", menuName = "Enemies Holder")]
public class EnemiesSOHolder : ScriptableObject, IHolder<EnemySO>
{
    public List<EnemySO> enemies;

    public List<EnemySO> Objects
    {
        get
        {
            return enemies;
        }
    }
}
