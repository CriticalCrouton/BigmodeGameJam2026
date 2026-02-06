using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PirateShipTest : MonoBehaviour
{
    [SerializeField]
    float startingVelocity; //How fast the boat will move when it's launched

    private int money; //How much money the player has

    private float velocityX; //How fast the boat is moving

    private float sinBounce; //Variable controlling the phase of the boat's sinwave y-bounce.

    private bool launched; //Whether or not you have launched the boat.

    private Dictionary<GameObject, float> frictionSources = new(); //Sources of friction affecting the ship

    // [SerializeField]
    // TextMeshProUGUI moneyUI; //The UI that tells you how much money you have

    //Properties
    public float Velocity { get { return velocityX; } set { velocityX = value; } }
    public int Money { get { return money; } set { money = value; } }
    public bool Launched { get { return launched; } set { launched = value; } }
    public float StartingVelocity { get { return startingVelocity; } set { startingVelocity = value; } }

    public float shipFriction
    {
        get
        {
            float totalFriction = 1f;
            foreach (float friction in frictionSources.Values)
            {
                totalFriction *= friction;
            }
            return totalFriction;
        }
    }

    private Rigidbody2D rb;

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

        rb = GetComponent<Rigidbody2D>();
        frictionSources.Add(gameObject, 0.999f); //Base friction
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagement.Instance.GameState != GameState.Run)
        {
            velocityX = 0;
        }

        //Boat launch (with space)
        if (Input.GetKeyDown(KeyCode.Space) && launched == false)
        {
            velocityX = startingVelocity;
            launched = true;
            GameManagement.Instance.GameState = GameState.Run;
        }

        //Happens every frame
        Vector3 newPos = transform.position;
        if (launched == false)
        {
            newPos.y += Mathf.Sin(sinBounce) * Time.deltaTime;
            transform.position = newPos;
        }
        //Once the boat is launched, it will gradually slow down until it is not moving any more.
        if (launched == true)
        {
            // newPos.x += velocity * Time.deltaTime;
            if (velocityX > 0)
            {
                velocityX *= shipFriction;
                Debug.Log("Velocity: " + velocityX + " Friction: " + shipFriction);
            }
            if (velocityX < 0.5)
            {
                velocityX = 0;
            }
        }

        

        //set rotation based on velocity
        float angle = Mathf.Clamp(velocityX / 10f, 0, 15);
        //lerp rotation for smoothness
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), 0.1f);

        sinBounce += 5 * Time.deltaTime; //Multiplying value by Time makes the sin bounce faster.
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(velocityX, rb.linearVelocity.y);
    }

    public void AddFrictionSource(GameObject source, float friction)
    {
        frictionSources.Add(source, friction);
    }

    public void RemoveFrictionSource(GameObject source)
    {
        if (frictionSources.ContainsKey(source))
        {
            frictionSources.Remove(source);
        }
    }
}
