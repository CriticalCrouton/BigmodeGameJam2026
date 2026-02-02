using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CannonFire : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI cannonballUI; //The UI text displaying cannonball count

    [SerializeField]
    GameObject reloadAnim; //The UI animation that indicates when cannonballs are reloading

    [SerializeField]
    GameObject cannonballPrefab; //The prefab for cannonball projectiles.

    [SerializeField]
    GameObject cannonballFireAnimation; //The animation for the firing of cannonballs.

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
        //Cannonballs are fired upwards with [ENTER] (return). This may need adjustment for multi-directional cannonballs.
        if (Input.GetKeyDown(KeyCode.Return) && cannonballs > 0 && threeSecondTimer == 0)
        {
            cannonballs--;
            GameObject fire = Instantiate(cannonballFireAnimation, gameObject.transform.position, cannonballFireAnimation.gameObject.transform.rotation);
            fire.transform.SetParent(PirateShipTest.Instance.gameObject.transform);
            GameObject cannonball = Instantiate(cannonballPrefab, gameObject.transform.position, gameObject.transform.rotation);
            cannonball.transform.SetParent(PirateShipTest.Instance.gameObject.transform);
            trackOfTime = true;
        }

        //More code can be added here to shoot cannonballs left or right (timer will go into effect either way.

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
