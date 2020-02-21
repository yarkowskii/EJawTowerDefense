using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Waves Holder", menuName = "Waves Holder")]
public class WavesSOHolder : ScriptableObject, IHolder<WaveSO>
{
    public List<WaveSO> waves;

    public List<WaveSO> Objects
    {
        get
        {
            return waves;
        }
    }
}
