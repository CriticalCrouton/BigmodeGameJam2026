using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PirateShipTest : MonoBehaviour
{
    [SerializeField]
    float StartingVelocity; //How fast the boat will move when it's launched

    private int money; //How much money the player has

    private float velocity; //How fast the boat is moving

    private float sinBounce; //Variable controlling the phase of the boat's sinwave y-bounce.

    private bool launched; //Whether or not you have launched the boat.

    [SerializeField]
    TextMeshProUGUI moneyUI; //The UI that tells you how much money you have

    //Properties
    public float Velocity { get { return velocity; } set { velocity = value; } }
    public int Money { get { return money; } set { money = value; } }
    public bool Launched { get { return launched; } set { launched = value; } }
    public List<float> frictionSources = new List<float> { 0.95f };
    public float shipFriction
    {
        get
        {
            float totalFriction = 1f;
            foreach (float friction in frictionSources)
            {
                totalFriction *= friction;
            }
            return totalFriction;
        }
    }

    //Singleton instance property
    public static PirateShipTest Instance { get; private set; }

    //Singleton setup
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sinBounce = 0; 
        money = 0;
        launched = false;
    }

    // Update is called once per frame
    void Update()
    {
        //reset friction sources
        frictionSources.Clear();
        frictionSources.Add(0.999f); //Base friction

        //Boat launch (with space)
        if (Input.GetKeyDown(KeyCode.Space) && launched == false)
        {
            velocity = StartingVelocity;
            launched = true;
            GameManagement.Instance.GameState = GameState.Run;
        }

        //Happens every frame
        Vector3 newPos = transform.position;
        if (launched == false) newPos.y += Mathf.Sin(sinBounce) * Time.deltaTime; 

        //Once the boat is launched, it will gradually slow down until it is not moving any more.
        if (launched == true)
        {
            newPos.x += velocity * Time.deltaTime;
            if (velocity > 0)
            {
                velocity *= shipFriction; //Temp value; can be changed with impunity.
                Debug.Log("Velocity: " + velocity + " Friction: " + shipFriction);
            }
            if (velocity < 0)
            {
                velocity = 0;
            }
        }
        
        //set rotation based on velocity
        float angle = Mathf.Clamp(velocity /10f, 0, 15);
        //lerp rotation for smoothness
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), 0.1f);

        sinBounce += 5 * Time.deltaTime; //Multiplying value by Time makes the sin bounce faster.
        gameObject.transform.position = newPos;

        moneyUI.text = "$: " + money;
    }

    public void AddFrictionSource(float friction)
    {
        frictionSources.Add(friction);
    }
    
}
