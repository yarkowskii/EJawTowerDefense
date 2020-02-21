using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlotScript : MonoBehaviour
{

    public Transform towerHolder;

    [Header("Extra references")]
    public Sprite sellBtnSprite;


    [Header("UI elements")]
    public Canvas mainCanvas;
    public Transform towerItemHolder;
    public GameObject towerItemPrefab;
    public GameObject panel;
    public GameObject buildBtn;
    public GameObject sellBtn;
    


    [HideInInspector] public TowerSO mTowerSO;
    [HideInInspector] public GameObject mTowerGO;
    [HideInInspector] public BulletPool bulletsPool;
    [HideInInspector] public GameValuesManager gameValuesManager; 
    [HideInInspector] public SlotManager slotManager;
    [HideInInspector] public UIManager uiManager;

    [Header("Flags")]
    public bool isAvailable;
    public bool isOpen;
    public bool isBuilding;

    private List<GameObject> panelItems = new List<GameObject> { };

    private void Update()
    {
        UpdateOpenItems();
    }

    #region UI part
    public void ShowTowerBuildPanel()
    {

        if (!isOpen)
        {
            OpenPanel();
            isBuilding = true;


            int towerCount = gameValuesManager.towersSOHolder.Objects.Count;
            int startAngle = -90;
          

            for (int i = 0; i < towerCount; i++)
            {
                GameObject item = Instantiate(towerItemPrefab, towerItemHolder);
                item.SetActive(false);

                TowerItem towerItem = item.GetComponent<TowerItem>();
                TowerSO towerSO = gameValuesManager.towersSOHolder.Objects[i];

                towerItem.towerImage.sprite = towerSO.towerSprite;
                towerItem.buildPriceText.text = towerSO.buildPrice.ToString();

                if (PlayerStats.Coins < towerSO.buildPrice)
                    towerItem.buildButton.interactable = false;

                int foo = i;

                towerItem.buildButton.onClick.AddListener(() => BuildTower(foo));
                panelItems.Add(item);

                item.SetActive(true);
            }

            PlaceItems(panelItems, startAngle);
        }
    }

    public void ShowTowerSellPanel()
    {

        if (!isOpen)
        {
            OpenPanel();

            int startAngle = 90;

            

            GameObject item = Instantiate(towerItemPrefab, towerItemHolder);
            TowerItem towerItem = item.GetComponent<TowerItem>();
            towerItem.buildPriceText.text = ((int)(mTowerSO.buildPrice * gameValuesManager.sellCoefficient)).ToString();
            towerItem.towerImage.sprite = sellBtnSprite;
            towerItem.buildButton.onClick.AddListener(() => SellTower());
            panelItems.Add(item);
            

            PlaceItems(panelItems, startAngle);
        }
    }

    private void PlaceItems(List<GameObject> items, float startAngle)
    {
        float radius = towerItemHolder.GetComponent<RectTransform>().rect.width / 2f;
        int angleInterval = 360 / items.Count;

        for (int i = 0; i < items.Count; i++)
        {
            float angle = startAngle + i * angleInterval;

            float x = radius * Mathf.Cos(Mathf.Deg2Rad * angle);
            float y = radius * Mathf.Sin(Mathf.Deg2Rad * angle);

            items[i].GetComponent<RectTransform>().localPosition = new Vector2(x, y);
        }
    }

    public void OpenPanel()
    {
        slotManager.CloseOpenSlot();
        isOpen = true;
        panel.SetActive(true);
        mainCanvas.sortingOrder = 5;
        slotManager.closeOpenSlotsButton.SetActive(true);
    }

    public void ClosePanel()
    {
        isOpen = false;
        isBuilding = false;
        mainCanvas.sortingOrder = 0;

        foreach (var item in panelItems)
            Destroy(item);
        
        panelItems.Clear();
        panel.SetActive(false);
    }

    private void UpdateOpenItems()
    {
        if (isOpen)
        {
            if (isBuilding)
            {
                for (int i = 0; i < panelItems.Count; i++)
                {
                    TowerSO towerSO = gameValuesManager.towersSOHolder.Objects[i];
                    if (PlayerStats.Coins < towerSO.buildPrice)
                        panelItems[i].GetComponent<TowerItem>().buildButton.interactable = false;
                    else
                        panelItems[i].GetComponent<TowerItem>().buildButton.interactable = true;

                }
            }

        }
    }
    #endregion


    private void BuildTower(int index)
    {
        ClosePanel();
        buildBtn.SetActive(false);
        sellBtn.SetActive(true);

        GameObject tower = Instantiate(gameValuesManager.towersSOHolder.Objects[index].towerPrefab, towerHolder);
        
        mTowerGO = tower;
        mTowerSO = gameValuesManager.towersSOHolder.Objects[index];

        PlayerStats.Coins -= mTowerSO.buildPrice;
        uiManager.UpdateUI();

        TowerScript towerScript = tower.GetComponent<TowerScript>();
        towerScript.mTowerSO = gameValuesManager.towersSOHolder.Objects[index];
        towerScript.bulletsPool = bulletsPool;
        towerScript.InitializePool();
    }

    private void SellTower()
    {
        ClosePanel();
        buildBtn.SetActive(true);
        sellBtn.SetActive(false);

        Debug.Log("Sell tower");

        PlayerStats.Coins += (int)(mTowerSO.buildPrice * gameValuesManager.sellCoefficient);
        uiManager.UpdateUI();
        Destroy(mTowerGO);
    }
}
