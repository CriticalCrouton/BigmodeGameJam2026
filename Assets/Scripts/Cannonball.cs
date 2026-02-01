using UnityEngine;

public class Cannonball : MonoBehaviour
{
    private float velocity;

    private CircleCollider2D hitbox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitbox = GetComponent<CircleCollider2D>();
        velocity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = gameObject.transform.position;
        newPos.y += velocity * Time.deltaTime;
        gameObject.transform.position = newPos;
    }

    public void Launch()
    {
        velocity = 100;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("CannonDestructibleBuilding"))
        {
            CannonBuildingDestruction destructionScript = collision.gameObject.GetComponent<CannonBuildingDestruction>();
            Instantiate(destructionScript.Explosion, destructionScript.gameObject.transform.position, destructionScript.gameObject.transform.rotation);
            PirateShipTest.Instance.Money += destructionScript.MoneyValue;
            PirateShipTest.Instance.Velocity += destructionScript.VelocityGain;
        }
    }
}
