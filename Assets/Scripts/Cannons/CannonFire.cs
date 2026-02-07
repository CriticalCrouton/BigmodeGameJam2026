using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;

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
    GameObject parentTarget; //The parent object for cannonballs

    [SerializeField]
    int startingCannonballs; //The number of cannonballs you start a run with

    private float reloadTimer; //Storage value for three seconds worth of time (reloading)

    private bool trackOfTime; //Time only matters for reloading, so otherwise it is off

    private int cannonballs; //The number of cannonballs you have.

    public float reloadTime; //Time it takes to reload a cannonball

    //Properties
    public int StartingCannonballs { get { return startingCannonballs; } set { startingCannonballs = value; } }

    private bool anyArrowPressed()
    {
        return Keyboard.current.upArrowKey.isPressed ||
               Keyboard.current.downArrowKey.isPressed ||
               Keyboard.current.leftArrowKey.isPressed ||
               Keyboard.current.rightArrowKey.isPressed;
    }

    private Vector2 inputDirection
    {
        get
        {
            prevInputDirection = Input.GetKeyDown(KeyCode.UpArrow) ? Vector2.up :
                         Input.GetKeyDown(KeyCode.LeftArrow) ? Vector2.left :
                         Input.GetKeyDown(KeyCode.RightArrow) ? Vector2.right :
                         Input.GetKeyDown(KeyCode.DownArrow) ? Vector2.down :
                         prevInputDirection;
            return prevInputDirection;
        }
    }

    private Vector2 prevInputDirection = Vector2.up;

    [SerializeField]
    private float fireOffset = 2.0f; //Distance from ship center to spawn cannonball


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cannonballs = startingCannonballs;
        reloadTimer = 0;
        trackOfTime = false;
        reloadAnim.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        cannonballUI.text = "    : " + cannonballs;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, inputDirection), 0.4f);


        //check for any arrow key press to fire cannonball in that direction
        if (!trackOfTime && cannonballs > 0 && anyArrowPressed())
        {
            ;

            if (inputDirection != Vector2.zero)
            {
                cannonballs--;
                SoundFXManager.Instance.Source.PlayOneShot(SoundFXManager.Instance.CannonSound, 1);
                Vector3 pos = gameObject.transform.position + (Vector3)inputDirection * fireOffset;
                GameObject fire = Instantiate(cannonballFireAnimation, pos, Quaternion.LookRotation(Vector3.forward, inputDirection));
                fire.transform.SetParent(parentTarget.transform);
                fire.GetComponent<ParticleSystem>().Play();

                GameObject cannonball = Instantiate(cannonballPrefab, pos, Quaternion.LookRotation(Vector3.forward, inputDirection));
                cannonball.transform.SetParent(parentTarget.transform);
                trackOfTime = true;
            }
        }


        //This enforces a cooldown between firing cannonballs.
        if (trackOfTime == true)
        {
            reloadAnim.SetActive(true);
            reloadTimer += Time.deltaTime;
            if (reloadTimer > reloadTime)
            {
                reloadTimer = 0;
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
