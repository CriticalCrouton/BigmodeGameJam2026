using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using TMPro;

public class CannonFire : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI cannonballUI; //The UI text displaying cannonball count

    [SerializeField]
    GameObject reloadAnim; //The UI animation that indicates when cannonballs are reloading

    [SerializeField]
    List<GameObject> cannonballPrefabs; //The prefab for cannonball projectiles.

    [SerializeField]
    List<GameObject> cannonballFireAnimations; //The animation for the firing of cannonballs.

    [SerializeField]
    int startingCannonballs; //The number of cannonballs you start a run with

    private float threeSecondTimer; //Storage value for three seconds worth of time (reloading)

    private bool trackOfTime; //Time only matters for reloading, so otherwise it is off

    private int cannonballs; //The number of cannonballs you have.


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cannonballs = startingCannonballs;
        threeSecondTimer = 0;
        trackOfTime = false;
        reloadAnim.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        cannonballUI.text = "    : " + cannonballs;
        //Cannonballs are fired upwards with [UP]. This may need adjustment for multi-directional cannonballs.
        if (Input.GetKeyDown(KeyCode.UpArrow) && cannonballs > 0 && threeSecondTimer == 0)
        {
            cannonballs--;
            GameObject fire = Instantiate(cannonballFireAnimations[0], gameObject.transform.position, cannonballFireAnimations[0].gameObject.transform.rotation);
            fire.transform.SetParent(PirateShipTest.Instance.gameObject.transform);
            GameObject cannonball = Instantiate(cannonballPrefabs[0], gameObject.transform.position, gameObject.transform.rotation);
            cannonball.transform.SetParent(PirateShipTest.Instance.gameObject.transform);
            trackOfTime = true;
        }
        //Cannonballs are fired left with [LEFT]
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && cannonballs > 0 && threeSecondTimer == 0)
        {
            cannonballs--;
            Vector3 pos = new Vector3(gameObject.transform.position.x - 5f, gameObject.transform.position.y - 1f, gameObject.transform.position.z);
            GameObject fire = Instantiate(cannonballFireAnimations[1], pos, cannonballFireAnimations[1].gameObject.transform.rotation);
            fire.transform.SetParent(PirateShipTest.Instance.gameObject.transform);
            GameObject cannonball = Instantiate(cannonballPrefabs[1], pos, gameObject.transform.rotation);
            cannonball.transform.SetParent(PirateShipTest.Instance.gameObject.transform);
            trackOfTime = true;
        }
        //Cannonballs are fired right with [RIGHT]
        else if (Input.GetKeyDown(KeyCode.RightArrow) && cannonballs > 0 && threeSecondTimer == 0)
        {
            cannonballs--;
            Vector3 pos = new Vector3(gameObject.transform.position.x + 3f, gameObject.transform.position.y - 1.33f, gameObject.transform.position.z);
            GameObject fire = Instantiate(cannonballFireAnimations[2], pos, cannonballFireAnimations[2].gameObject.transform.rotation);
            fire.transform.SetParent(PirateShipTest.Instance.gameObject.transform);
            GameObject cannonball = Instantiate(cannonballPrefabs[2], pos, gameObject.transform.rotation);
            cannonball.transform.SetParent(PirateShipTest.Instance.gameObject.transform);
            trackOfTime = true;
        }

        //This enforces a 3-second cooldown between firing cannonballs.
        if (trackOfTime == true)
        {
            reloadAnim.SetActive(true);
            threeSecondTimer += Time.deltaTime;
            if (threeSecondTimer > 3)
            {
                threeSecondTimer = 0;
                trackOfTime = false;
                reloadAnim.SetActive(false);
            }
        }
    }

    //Resets cannonball count (short!)
    public void ResetCannons()
    {
        cannonballs = startingCannonballs;
    }
}
