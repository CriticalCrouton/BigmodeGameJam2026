using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CannonFire : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI cannonballUI;

    [SerializeField]
    GameObject cannonballPrefab;

    [SerializeField]
    GameObject cannonballFireAnimation;

    [SerializeField]
    int startingCannonballs;

    private int cannonballs;

    public int Cannonballs { get { return cannonballs; } set { cannonballs = value; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cannonballs = startingCannonballs;
    }

    // Update is called once per frame
    void Update()
    {
        cannonballUI.text = "    : " + cannonballs;
        if (Input.GetKeyDown(KeyCode.Return) && cannonballs > 0)
        {
            cannonballs--;
            GameObject fire = Instantiate(cannonballFireAnimation, gameObject.transform.position, cannonballFireAnimation.gameObject.transform.rotation);
            fire.transform.SetParent(PirateShipTest.Instance.gameObject.transform);
            GameObject cannonball = Instantiate(cannonballPrefab, gameObject.transform.position, gameObject.transform.rotation);
            cannonball.transform.SetParent(PirateShipTest.Instance.gameObject.transform);
            Cannonball cannonballScript = cannonball.GetComponent<Cannonball>();
            cannonballScript.Launch();
        }
    }

    public void ResetCannons()
    {
        cannonballs = startingCannonballs;
    }
}
