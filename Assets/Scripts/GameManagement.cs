using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public enum GameState
{
    Run,
    Shop
}

public class GameManagement : MonoBehaviour
{
    private float lastFrameXPos; //This allows us to tell when the boat has "stopped"

    private GameState state;

    [SerializeField]
    PirateShipTest playerObject; //The player

    private SpriteRenderer playerVisual;

    [SerializeField]
    List<BuildingDestruction> destructibles; //A list of all buildings on the map

    [SerializeField]
    TextMeshProUGUI launchUI; //Instructive text for launching

    [SerializeField]
    TextMeshProUGUI restartUI; //Instructive text for restarting

    [SerializeField]
    Button restartButton; //The button for restarting.

    [SerializeField]
    Button shopButton; //The button for going to the shop.

    [SerializeField]
    Button backFromShopButton; //The button for going back into the game from the shop.

    public static GameManagement Instance { get; private set; }

    public GameState GameState { get { return state; } set { state = value; } }

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
    }


    private void Start()
    {
        playerVisual = playerObject.GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        if (state == GameState.Run)
        {
            //UI differences depending on boat launch status.
            switch (playerObject.Launched)
            {
                case true:
                    launchUI.enabled = false;
                    break;
                case false:
                    launchUI.enabled = true;
                    restartUI.enabled = false;
                    restartButton.gameObject.SetActive(false);
                    shopButton.gameObject.SetActive(false);
                    backFromShopButton.gameObject.SetActive(false);
                    break;
            }

            if (playerObject.Launched == true && playerObject.transform.position.x == lastFrameXPos) //This should only happen once the boat reaches a standstill)
            {
                restartUI.enabled = true;
                restartButton.gameObject.SetActive(true);
                shopButton.gameObject.SetActive(true);
            }
            lastFrameXPos = playerObject.transform.position.x;
        }
        
    }

    //When the button is clicked, the game will restart.
    public void Restart()
    {
        state = GameState.Run;
        playerObject.gameObject.transform.position = new Vector3(-2.54f, -3.05f, 0);
        playerVisual.enabled = true;
        playerObject.Launched = false;
        restartUI.enabled = false;
        restartButton.enabled = false;
        launchUI.enabled = true;

        foreach (BuildingDestruction destructible in destructibles)
        {
            destructible.Explosion.enabled = false;
        }
    }

    public void GoToShop()
    {
        state = GameState.Shop;

        //This is a bit of a cheat. Moving the player object to where I want the camera to be is more reliable than moving the camera itself.
        playerObject.gameObject.transform.position = new Vector3(-75f, -3.05f, 0);
        playerVisual.enabled = false;
        launchUI.enabled = false;
        restartUI.enabled = false;
        restartButton.gameObject.SetActive(false);
        shopButton.gameObject.SetActive(false);
        backFromShopButton.gameObject.SetActive(true);
        
        //Enabling of all of the shop buttons. (likely a public method of a shop script)
    }
}
