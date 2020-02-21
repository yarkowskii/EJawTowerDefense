using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{

    public List<Transform> slotSpawnPoints;

    [Header("UI references")]
    public SlotScript slotPrefab;
    public Transform slotsHolder;

    [Header("Extra references")]
    public GameObject closeOpenSlotsButton;

    [Header("Managers")]
    public GameValuesManager gameValuesManager;
    public UIManager uiManager;
    
    private List<SlotScript> mSlots;
    private void Start()
    {
        SpawnSlots();
    }

    private void SpawnSlots()
    {
        int slotsCount = Mathf.Min(Slotpoints.points.Count, gameValuesManager.slotsCount);

        List<int> chooseIndexes = new List<int> { };
        mSlots = new List<SlotScript> { };

        for (int i = 0; i < slotsCount; i++)
        {
            int randIndex = Random.Range(0, slotSpawnPoints.Count);

            while(chooseIndexes.Contains(randIndex))
                randIndex = Random.Range(0, slotSpawnPoints.Count);
            chooseIndexes.Add(randIndex);

            SlotScript slot = Instantiate(slotPrefab, slotSpawnPoints[randIndex].position, Quaternion.identity, slotsHolder);

            slot.gameValuesManager = gameValuesManager;
            slot.uiManager = uiManager;
            slot.slotManager = this;
            mSlots.Add(slot);
        }
    }

    public void CloseOpenSlot()
    {
        foreach (var slot in mSlots)
        {
            if (slot.isOpen)
                slot.ClosePanel();
        }

        closeOpenSlotsButton.SetActive(false);
    }
}
