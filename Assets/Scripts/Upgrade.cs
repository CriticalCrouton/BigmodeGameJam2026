using UnityEngine;

public class Upgrade
{
    private int level;
    private int cost; // doesn't necessarily need to be a float but just to be flexible
    private string name;


    public int Level
    {
        get
        {
            return level;
        }
    }
    public int Cost
    {
        get
        {
            return cost;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
    }

    public Upgrade(string name)
    {
        level = 0;
        cost = 1000;
        this.name = name;
    }

    public bool LevelUp()
    {
        if (PirateShipTest.Instance.Money < cost)
        {
            Debug.Log("Not enough money!");
            return false;
        }

        PirateShipTest.Instance.Money -= cost;
        if(level <= 5)
        {
            level++;
        }
        cost *= 2;

        Debug.Log("Enough money!");
        return true;
    }
}
