using UnityEngine;

public class Upgrade
{
    private int level;
    private float cost; // doesn't necessarily need to be a float but just to be flexible
    private string name;

    public float Cost
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

    public bool LevelUp(float playerMoney)
    {
        if(playerMoney < cost)
        {
            return false;
        }

        level++;
        cost *= 2;
        
        return true;
    }
}
