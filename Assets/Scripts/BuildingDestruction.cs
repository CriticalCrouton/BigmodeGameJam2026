using UnityEngine;

public class BuildingDestruction : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer explosion;

    [SerializeField]
    int moneyValue; //How much money the building is worth

    [SerializeField]
    float velocityLoss; //How much crashing through the building will slow you down.

    [SerializeField]
    PirateShipTest ship;

    public SpriteRenderer Explosion { get { return explosion; } set { explosion = value; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        explosion.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //The money, slowdown, and slow-motion effect happen when you ENTER the building
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PirateShip"))
        {
            ship.Money += moneyValue;
            ship.Velocity -= velocityLoss;
            Time.timeScale = 0.1f;
        }
    }

    //Time returns to normal and the building "explodes" once you LEV
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PirateShip"))
        {
            explosion.enabled = true;
            Time.timeScale = 1;
        }
    }
}
