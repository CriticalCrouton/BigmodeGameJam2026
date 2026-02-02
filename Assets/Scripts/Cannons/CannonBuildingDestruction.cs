using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonBuildingDestruction : MonoBehaviour
{

    [SerializeField]
    GameObject explosion; //The explosion created by destruction

    [SerializeField]
    int moneyValue; //The money that the building is worth

    [SerializeField]
    float velocityGain; //The velocity gained from destroying the building



    //When something collides with the building...
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If it's the pirate ship, slow down time for the cannon shot.
        if (collision.gameObject.layer == LayerMask.NameToLayer("PirateShip"))
        {
            Time.timeScale = 0.25f;
        }
        //If it's a cannonball, spawn an explosion, give money and speed up ship.
        if (collision.gameObject.layer == LayerMask.NameToLayer("Cannonball"))
        {
            Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
            PirateShipTest.Instance.Money += moneyValue;
            PirateShipTest.Instance.Velocity += velocityGain;

        }
    }

    //When the pirate ship leaves the building's collider, time goes back to normal.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PirateShip"))
        {
            Time.timeScale = 1f;
        }
    }
}
