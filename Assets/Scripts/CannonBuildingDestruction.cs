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
        if (Input.GetKeyDown(KeyCode.Return) && cannons.Cannonballs > 0)
        {
            cannons.Cannonballs--;
            
            //Add a check to make sure the ship is within the collision zone.

        }
    }
}
