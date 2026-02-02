using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class UpgradeButton : MonoBehaviour
{
    //Cost text
    [SerializeField]
    TextMeshProUGUI costText;

    [SerializeField]
    GameManagement manager;

    Upgrade currentUpgrade;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (manager.upgradeList[0] == null)
        {
            Debug.Log("where did it go?");
        }
        //foreach(Upgrade u in manager.upgradeList)
        //{
        //    if(u.Name == this.gameObject.name)
        //    {
        //        currentUpgrade = u;
        //    }
        //    if(currentUpgrade == null)
        //    {
        //        Debug.Log("Error: upgrade not found");
        //    }
        //}
    }
    private void Update()
    {
        //costText.text = "Cost: $" + currentUpgrade.Cost;
    }

    public void ButtonPress()
    {
        //if (currentUpgrade.LevelUp(PirateShipTest.Instance.Money))
        //{
        //    // Code to change the level graphic for upgrade icon
        //}
        //else
        //{
        //    //Code to show not enough money ui
        //}
    }

}
