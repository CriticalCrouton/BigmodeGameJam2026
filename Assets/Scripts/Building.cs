using System.Collections;
using UnityEngine;
public enum BuildingType
{
    Background,
    Foreground,
    Wall
}



public class Building : MonoBehaviour
{
    [SerializeField]
    protected GameObject explosion; //The prefab explosion spawned by destruction

    [SerializeField]
    protected int moneyValue; //How much money the building is worth

    [SerializeField]
    float velocityLoss; //How much crashing through the building will slow you down.
    [SerializeField] float friction;

    [SerializeField] protected float explosionBoost;

    [SerializeField] protected BuildingType buildingType;

    private int  foregroundCashBonus;
    private int backgroundCashBonus;
    private float frictionMultiplier;

    private bool currentlyColliding = false;

    //Properties
    public int MoneyValue { get { return moneyValue; } set { moneyValue = value; } }
    public float VelocityLoss { get { return velocityLoss; } set { velocityLoss = value; } }
    public float Friction { get { return friction; } set { friction = value; } }
    public int ForegroundCashBonus { get { return foregroundCashBonus; } set { foregroundCashBonus = value; } }
    public int BackgroundCashBonus { get { return backgroundCashBonus; } set { backgroundCashBonus = value; } }
    public float FrictionMultiplier { get { return frictionMultiplier; } set { frictionMultiplier = value; } }

    private void Start()
    {
        foregroundCashBonus = 0;
        backgroundCashBonus = 0;
        frictionMultiplier = 1;
    }
    //The money, slowdown, and slow-motion effect happen when you ENTER the building
    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentlyColliding = true;

        switch (buildingType)
        {
            case BuildingType.Background:
                BackgroundBehavior(collision);
                break;
            case BuildingType.Foreground:
                ForegroundBehavior(collision);
                break;
            case BuildingType.Wall:
                WallBehavior(collision);
                break;
        }
        //Activates face-changing procedures
        GameManagement.Instance.ReactionController.CoolShit = true;
        int randomSound = Random.Range(0, 5);
        SoundFXManager.Instance.Source.PlayOneShot(SoundFXManager.Instance.CrashSounds[randomSound], 1);
    }

    //Time returns to normal and the building "explodes" once you LEV
    private void OnTriggerExit2D(Collider2D collision)
    {
        currentlyColliding = false;

        switch (buildingType)
        {
            case BuildingType.Background:
                BackgroundBehavior(collision);
                break;
            case BuildingType.Foreground:
                ForegroundBehavior(collision);
                break;
            case BuildingType.Wall:
                WallBehavior(collision);
                break;
        }
        //Activates face-changing procedures
        GameManagement.Instance.ReactionController.CoolShit = false;
        
    }

    private void BackgroundBehavior(Collider2D collision)
    {
        if (currentlyColliding)
        {
            //If it's the pirate ship, slow down time for the cannon shot.
            if (collision.gameObject.layer == LayerMask.NameToLayer("PirateShip"))
            {
                Time.timeScale = 0.5f;
            }
            //If it's a cannonball, spawn an explosion, give money and speed up ship.
            if (collision.gameObject.layer == LayerMask.NameToLayer("Cannonball"))
            {
                Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
                PirateShipTest.Instance.Money += moneyValue + backgroundCashBonus;
                PirateShipTest.Instance.Velocity += explosionBoost;
            }
        }
        /*
        else
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("PirateShip"))
            {
                Time.timeScale = 1f;
            }
        }
        */


    }

    private void ForegroundBehavior(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PirateShip"))
        {
            if (currentlyColliding)
            {
                Time.timeScale = 0.5f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                PirateShipTest.Instance.AddFrictionSource(gameObject, (friction * frictionMultiplier));
                PirateShipTest.Instance.Money += moneyValue + foregroundCashBonus;
            }
            else
            {
                PirateShipTest.Instance.RemoveFrictionSource(gameObject);
                Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                PirateShipTest.Instance.Velocity += explosionBoost;
                Destroy(gameObject);
            }
        }

    }

    private void WallBehavior(Collider2D collision)
    {
        if (PirateShipTest.Instance.Velocity < 150)
        {
            Instantiate(explosion, PirateShipTest.Instance.gameObject.transform.position, Quaternion.identity);
            PirateShipTest.Instance.Velocity = 0;
        }
    }


}
