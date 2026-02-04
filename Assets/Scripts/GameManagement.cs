using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using NUnit.Framework.Constraints;
using System.Linq;

public enum GameState
{
    Prerun,
    Run,
    Shop
}

public class GameManagement : MonoBehaviour
{
    private float lastFrameXPos; //This allows us to tell when the boat has "stopped"

    private GameState state; //Determines if you are in an active run or in the shop.

    [SerializeField]
    CannonFire cannons; //The cannons

    private SpriteRenderer playerVisual; //The sprite renderer for the ship (camera hack)

    [SerializeField]
    List<GameObject> destructibles; //A list of all buildings on the map (presently useless)
    //The game will not break if buildings are not added to this list!!!

    [SerializeField]
    TextMeshProUGUI launchUI; //Instructive text for launching

    [SerializeField]
    TextMeshProUGUI restartUI; //Instructive text for restarting

    [SerializeField]
    Button restartButton; //The button for restarting.

    [SerializeField]
    Button shopButton; //The button for going to the shop.

    [SerializeField]
    Canvas shopCanvas; //Canvas containing shop ui

    //Upgrade variables

    public Upgrade[] upgradeList = new Upgrade[5];

    [SerializeField]
    Sprite lv0;
    [SerializeField]
    Sprite lv1;
    [SerializeField]
    Sprite lv2;
    [SerializeField]
    Sprite lv3;
    [SerializeField]
    Sprite lv4;
    [SerializeField]
    Sprite lv5;

    Sprite[] levelBar = new Sprite[6]; //Level up bar spritesheet

    //Singleton Instance Property
    public static GameManagement Instance { get; private set; }

    //Properties
    public GameState GameState { get { return state; } set { state = value; } }

    public Sprite[] LevelBar
    {
        get { return levelBar; }
    }

    //Singleton Setup
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        //Kind of a sloppy way to make this array but this is C#9.0 and apparently
        //array = [1,2,3,4]; doesn't exist yet????
        upgradeList[0] = new Upgrade("CannonBallUpgrade");
        upgradeList[1] = new Upgrade("CannonUpgrade");
        upgradeList[2] = new Upgrade("OilUpgrade");
        upgradeList[3] = new Upgrade("SailUpgrade");
        upgradeList[4] = new Upgrade("ShipUpgrade");

        levelBar[0] = lv0;
        levelBar[1] = lv1;
        levelBar[2] = lv2;
        levelBar[3] = lv3;
        levelBar[4] = lv4;
        levelBar[5] = lv5;
    }


    private void Start()
    {
        state = GameState.Prerun;
        playerVisual = PirateShipTest.Instance.GetComponent<SpriteRenderer>();
        
    }


    // Update is called once per frame
    void Update()
{
    //Debug.Log("Current Game State: " + state.ToString());
    switch (state)
    {
        case GameState.Prerun:
            HandlePreRun();
            break;

        case GameState.Run:
            HandleRun();
            break;

        case GameState.Shop:
            HandleShop();
            break;
    }
}

void HandlePreRun()
{
    launchUI.enabled = true;
    restartUI.enabled = false;

    restartButton.gameObject.SetActive(false);
    shopButton.gameObject.SetActive(false);
    shopCanvas.gameObject.SetActive(false);

    // Transition to Run when boat launches
    if (PirateShipTest.Instance.Launched)
    {
        state = GameState.Run;
    }
}

void HandleRun()
{
    launchUI.enabled = false;
    restartUI.enabled = false;

    restartButton.gameObject.SetActive(false);
    shopButton.gameObject.SetActive(false);
    shopCanvas.gameObject.SetActive(false);

    // Detect stop
    if (PirateShipTest.Instance.Velocity <= 0.5f)
    {
        PirateShipTest.Instance.Velocity = 0;
        state = GameState.Shop;
    }

    lastFrameXPos = PirateShipTest.Instance.transform.position.x;
}

void HandleShop()
{
    launchUI.enabled = false;
    restartUI.enabled = true;

    restartButton.gameObject.SetActive(true);
    shopButton.gameObject.SetActive(true);
    shopCanvas.gameObject.SetActive(true);
}



    //When the button is clicked, the game will restart.
    public void Restart()
    {
        

        //Resets the pirate ship to it's starting position.
        PirateShipTest.Instance.gameObject.transform.position = new Vector3(-2.54f, -3.05f, 0);
        MapGenerator.Instance.Reset();
        PirateShipTest.Instance.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        playerVisual.enabled = true;
        PirateShipTest.Instance.Launched = false;
        restartUI.enabled = false;
        launchUI.enabled = true;
        restartButton.gameObject.SetActive(false);
        shopCanvas.gameObject.SetActive(false);
        cannons.ResetCannons();

        state = GameState.Prerun;

        /* Unnecessary right now
        foreach (GameObject destructible in destructibles)
        {
            //destructible.Explosion.enabled = false;
        }
        */
    }

    public void GoToShop()
    {
        state = GameState.Shop;

        //This is a bit of a cheat. Moving the player object to where I want the camera to be is more reliable than moving the camera itself.
        PirateShipTest.Instance.gameObject.transform.position = new Vector3(-75f, -3.05f, 0);
        playerVisual.enabled = false;
        launchUI.enabled = false;
        restartUI.enabled = false;
        restartButton.gameObject.SetActive(false);
        shopButton.gameObject.SetActive(false);
        shopCanvas.gameObject.SetActive(true);
    }
}
