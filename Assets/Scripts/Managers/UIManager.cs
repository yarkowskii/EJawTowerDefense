using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameValuesManager gameValuesManager;

    [Header("UI references")]
    public TextMeshProUGUI playerHpText;
    public TextMeshProUGUI coinsText;

    public Image waveFillImage;
    public Image hpFillImage;

    private void Start()
    {
        UpdateUI();
    }

    public  void UpdateUI()
    {
        playerHpText.text = $"Health: {PlayerStats.Hp}";
        coinsText.text = $"Coins: {PlayerStats.Coins}";

        float progg = Mathf.InverseLerp(gameValuesManager.playerStartHp, 0, PlayerStats.Hp);
        hpFillImage.fillAmount = Mathf.Lerp(1, 0, progg);
    }
}
