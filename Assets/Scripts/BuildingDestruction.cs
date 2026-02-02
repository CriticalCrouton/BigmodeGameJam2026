using UnityEngine;

public class BuildingDestruction : MonoBehaviour
{
    [SerializeField]
    GameObject explosion; //The prefab explosion spawned by destruction

    [SerializeField]
    int moneyValue; //How much money the building is worth

    [SerializeField]
    float velocityLoss; //How much crashing through the building will slow you down.


    //The money, slowdown, and slow-motion effect happen when you ENTER the building
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PirateShip"))
        {
            PirateShipTest.Instance.Money += moneyValue;
            PirateShipTest.Instance.Velocity -= velocityLoss;
            Time.timeScale = 0.1f;
        }
    }

    //Time returns to normal and the building "explodes" once you LEV
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PirateShip"))
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Time.timeScale = 1;
        }
    }
}
