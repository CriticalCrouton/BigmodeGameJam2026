using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PirateShipTest : MonoBehaviour
{
    [SerializeField]
    float StartingVelocity; //How fast it goes when it starts

    private float velocity; //How fast it is actually going

    private float time; //ONLY matters for the sin bounce

    private bool launched; //Whether or not you have launched the boat.

    private float lastFrameXPos; //This allows us to tell when the boat has "stopped"

    private bool stopped = false; //Whether or not the boat is stopped (after being launched)

    [SerializeField]
    TextMeshProUGUI launchUI; //The UI to instruct to launch the boat

    [SerializeField]
    TextMeshProUGUI restartUI; //The UI to instruct to restart the game.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 0;
        launched = false;
        stopped = false;
        launchUI.enabled = true;
        restartUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Boat launch
        if (Input.GetKeyDown(KeyCode.Space) && launched == false)
        {
            velocity = StartingVelocity;
            launched = true;
        }
        //Resetting to the beginning
        if (Input.GetKeyDown(KeyCode.Space) && stopped == true)
        {
            gameObject.transform.position = new Vector3(-2.42f, -1.73f, 0);
            time = 0;
            launched = false;
            stopped = false;
            restartUI.enabled = false;
            launchUI.enabled = true;
        }


        Vector3 newPos = transform.position;
        newPos.y += Mathf.Sin(time) * Time.deltaTime;

        //Once the boat is launched, it will gradually slow down until it is not moving any more.
        if (launched == true)
        {
            launchUI.enabled = false;
            newPos.x += velocity * Time.deltaTime;
            if (velocity > 0)
            {
                velocity -= 2 / 60f;
            }
            if (velocity < 0)
            {
                velocity = 0;
            }
            if (newPos.x == lastFrameXPos) //This should only happen once the boat reaches a standstill)
            {
                stopped = true;
                restartUI.enabled = true;
            }
        }

        time += 5 * Time.deltaTime;
        lastFrameXPos = newPos.x;
        gameObject.transform.position = newPos;
    }
}
