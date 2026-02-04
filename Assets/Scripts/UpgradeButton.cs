using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeButton : MonoBehaviour
{
    //Cost text
    [SerializeField]
    TextMeshProUGUI costText;

    GameManagement manager;

    Sprite[] lvBar;

    [SerializeField]
    Image levelBarImg;

    Upgrade currentUpgrade;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = GameManagement.Instance;

        foreach (Upgrade u in manager.upgradeList)
        {
            if (u.Name == this.gameObject.name)
            {
                currentUpgrade = u;
            }
        }
        if (currentUpgrade == null)
        {
            Debug.Log("Error: upgrade not found");
        }
        lvBar = manager.LevelBar;
    }
    private void Update()
    {
        costText.text = "Cost: $" + currentUpgrade.Cost;

        levelBarImg.sprite = lvBar[currentUpgrade.Level];
    }

    public void ButtonPress()
    {
        Debug.Log(currentUpgrade.Name);
        if (currentUpgrade.LevelUp())
        {
            // Code to change the level graphic for upgrade icon
            
        }
        else
        {
            //Code to show not enough money ui
            
        }
    }

}
