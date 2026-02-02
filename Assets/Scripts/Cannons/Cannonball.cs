using UnityEngine;

public class Cannonball : MonoBehaviour
{
    private float velocity; //How fast the cannonball is moving
    private CircleCollider2D hitbox; //The collider for the cannonball.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitbox = GetComponent<CircleCollider2D>();
        velocity = 75;
    }

    //Moves the cannonball after it is fired (will need to be changed for multidirectional cannonballs.
    void Update()
    {
        Vector3 newPos = gameObject.transform.position;
        newPos.y += velocity * Time.deltaTime;
        gameObject.transform.position = newPos;
    }
}
