using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonBuildingDestruction : MonoBehaviour
{

    [SerializeField]
    GameObject explosion;

    [SerializeField]
    int moneyValue;

    [SerializeField]
    float velocityGain;

    public GameObject Explosion { get { return explosion; } }
    public int MoneyValue { get { return moneyValue; } }
    public float VelocityGain { get { return velocityGain; } }

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
        if (collision.gameObject.layer == LayerMask.NameToLayer("PirateShip"))
        {
            Time.timeScale = 0.25f;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Cannonball"))
        {
            Instantiate(Explosion, gameObject.transform.position, gameObject.transform.rotation);
            PirateShipTest.Instance.Money += moneyValue;
            PirateShipTest.Instance.Velocity += velocityGain;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PirateShip"))
        {
            Time.timeScale = 1f;
        }
    }
}
