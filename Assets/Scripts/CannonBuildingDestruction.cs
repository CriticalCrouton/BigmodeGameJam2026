using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonBuildingDestruction : MonoBehaviour
{
    [SerializeField]
    PirateShipTest ship;

    [SerializeField]
    CannonFire cannons;

    [SerializeField]
    GameObject explosion;

    [SerializeField]
    Collider2D collisionZone;

    [SerializeField]
    int moneyValue;

    [SerializeField]
    float velocityGain;

    public Collider2D CollisionZone { get { return collisionZone; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0.1f;
    }

    //This should work... but it doesn't!!!! It literally doesn't at all I don't get it.
    private void OnTriggerStay2D(Collider2D collision)
    {
        Console.WriteLine("CannonDestructible");
        if (Input.GetKeyDown(KeyCode.Return) && cannons.Cannonballs > 0)
        {
            cannons.Cannonballs--;
            Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
            ship.Velocity += velocityGain; //Destroying a ship with a cannonball makes you speed up.
            ship.Money += moneyValue;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Time.timeScale = 1f;
    }
}
