using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{

    private float lastFrameXPos; //This allows us to tell when the boat has "stopped"

    [SerializeField]
    PirateShipTest playerObject; //The player

    [SerializeField]
    List<BuildingDestruction> destructibles; //A list of all buildings on the map

    [SerializeField]
    TextMeshProUGUI launchUI; //Instructive text for launching

    [SerializeField]
    TextMeshProUGUI restartUI; //Instructive text for restarting

    [SerializeField]
    TextMeshProUGUI moneyUI; //Text that displays your current money.

    [SerializeField]
    Button restartButton; //The button for restarting.
    // Update is called once per frame
    void Update()
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
                break;
        }

        if (playerObject.Launched == true && playerObject.transform.position.x == lastFrameXPos) //This should only happen once the boat reaches a standstill)
        {
            restartUI.enabled = true;
            restartButton.gameObject.SetActive(true);
        }
        lastFrameXPos = playerObject.transform.position.x;
    }

    //When the button is clicked, the game will restart.
    public void Restart()
    {
        playerObject.gameObject.transform.position = new Vector3(-2.54f, -3.05f, 0);
        playerObject.Launched = false;
        restartUI.enabled = false;
        restartButton.enabled = false;
        launchUI.enabled = true;

        foreach (BuildingDestruction destructible in destructibles)
        {
            destructible.Explosion.enabled = false;
        }
    }
}
