using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    GameObject explosion; //The prefab explosion spawned by destruction

    [SerializeField]
    int moneyValue; //How much money the building is worth

    [SerializeField]
    float velocityLoss; //How much crashing through the building will slow you down.
    [SerializeField] float friction;

    [SerializeField] float explosionBoost;


    //The money, slowdown, and slow-motion effect happen when you ENTER the building
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PirateShip"))
        {
            PirateShipTest.Instance.Money += moneyValue;
            // PirateShipTest.Instance.Velocity -= velocityLoss;
            PirateShipTest.Instance.AddFrictionSource(gameObject,friction); //Adds a temporary friction source to the ship

            //Because the system works off of friction now, the boat gets REALLY bogged down during slow motion.
            //Mayhaps we need to use Time.fixedTimeScale? Or make the time scale faster?

            // Time.timeScale = 0.1f;
            Debug.Log("Entered building trigger");
        }
    }

    //Time returns to normal and the building "explodes" once you LEV
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PirateShip"))
        {
            PirateShipTest.Instance.RemoveFrictionSource(gameObject);
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            // Time.timeScale = 1;
            PirateShipTest.Instance.Velocity += explosionBoost;
            Destroy(gameObject);
        }
    }
}
