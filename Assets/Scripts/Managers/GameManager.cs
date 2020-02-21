using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isGame;

    [Header("UI references")]
    public GameObject losePanel;
    public GameObject winPanel;

    [Header("Managers references")]
    public UIManager iuManager;
    public SlotManager slotManager;

    private void Start()
    {
        isGame = true;
    }

    public void Update()
    {
        if(PlayerStats.Hp <= 0 && isGame)
        {
            PlayerStats.Hp = 0;
            iuManager.UpdateUI();
            LoseLevel();
        }
    }

    public void WinLevel()
    {
        isGame = false;
        winPanel.SetActive(true);
        slotManager.CloseOpenSlot();
        Debug.Log("Win level");
    }

    public void LoseLevel()
    {
        isGame = false;
        slotManager.CloseOpenSlot();
        losePanel.SetActive(true);
        Debug.Log("Lose level");
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
