using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CannonFire : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI cannonballUI;

    [SerializeField]
    GameObject reloadAnim;

    [SerializeField]
    GameObject cannonballPrefab;

    [SerializeField]
    GameObject cannonballFireAnimation;

    [SerializeField]
    int startingCannonballs;

    private float threeSecondTimer;

    private bool trackOfTime;

    private int cannonballs;

    public int Cannonballs { get { return cannonballs; } set { cannonballs = value; } }

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
        if (Input.GetKeyDown(KeyCode.Return) && cannonballs > 0 && threeSecondTimer == 0)
        {
            cannonballs--;
            GameObject fire = Instantiate(cannonballFireAnimation, gameObject.transform.position, cannonballFireAnimation.gameObject.transform.rotation);
            fire.transform.SetParent(PirateShipTest.Instance.gameObject.transform);
            GameObject cannonball = Instantiate(cannonballPrefab, gameObject.transform.position, gameObject.transform.rotation);
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

    public void ResetCannons()
    {
        cannonballs = startingCannonballs;
    }
}
