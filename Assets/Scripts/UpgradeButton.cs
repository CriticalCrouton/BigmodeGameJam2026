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
        Debug.Log("Change the method NOW!");
    }
    public void Sail()
    {
        Debug.Log(currentUpgrade.Name);
        if (currentUpgrade.LevelUp())
        {
            manager.SailUpgrade();
            UpgradeSound();
        }
        else
        {
            NotEnoughMoney();
        }
        
    }
    public void Oil()
    {
        Debug.Log(currentUpgrade.Name);
        if (currentUpgrade.LevelUp())
        {
            manager.OilUpgrade(currentUpgrade.Level);
            UpgradeSound();
        }
        else
        {
            NotEnoughMoney();
        }
    }
    public void CannonBall()
    {
        Debug.Log(currentUpgrade.Name);
        if (currentUpgrade.LevelUp())
        {
            manager.CannonballUpgrade();
            UpgradeSound();
        }
        else
        {
            NotEnoughMoney();
        }
    }
    public void Cannon()
    {
        Debug.Log(currentUpgrade.Name);
        if (currentUpgrade.LevelUp())
        {
            manager.CannonUpgrade();
            UpgradeSound();
        }
        else
        {
            NotEnoughMoney();
        }
    }
    public void Ship()
    {
        Debug.Log(currentUpgrade.Name);
        if (currentUpgrade.LevelUp())
        {
            manager.ShipUpgrade();
            UpgradeSound();
        }
        else
        {
            NotEnoughMoney();
        }
    }

    private void UpgradeSound()
    {
        SoundFXManager.Instance.Source.PlayOneShot(SoundFXManager.Instance.UpgradeSounds[currentUpgrade.Level - 1], 1);
    }

    private void NotEnoughMoney()
    {
        SoundFXManager.Instance.Source.PlayOneShot(SoundFXManager.Instance.FailedPurchaseSound, 1);
    }
    
}
