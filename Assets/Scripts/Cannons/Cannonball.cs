using System.Collections.Generic;
using UnityEngine;

public enum CannonballType
{
    Up,
    Right,
    Left,
}

public class Cannonball : MonoBehaviour
{
    [SerializeField] private float velocity = 300f; //How fast the cannonball is moving
    private CircleCollider2D hitbox; //The collider for the cannonball.

    private float elapsedTime; //Cannonballs destroy themselves after five seconds.

    [SerializeField]
    private CannonballType type; //Determines the direction of the cannonball

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitbox = GetComponent<CircleCollider2D>();
    }

    //Moves the cannonball after it is fired (will need to be changed for multidirectional cannonballs.
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 5)
        {
            Destroy(gameObject);
        }

        // if (type == CannonballType.Up)
        // {
        //     Vector3 newPos = gameObject.transform.position;
        //     newPos.y += velocity * Time.deltaTime;
        //     gameObject.transform.position = newPos;
        // }
        // else if (type == CannonballType.Right)
        // {
        //     Vector3 newPos = gameObject.transform.position;
        //     newPos.x += velocity * Time.deltaTime;
        //     gameObject.transform.position = newPos;
        // }
        // else if (type == CannonballType.Left)
        // {
        //     Vector3 newPos = gameObject.transform.position;
        //     newPos.x -= velocity * Time.deltaTime;
        //     gameObject.transform.position = newPos;
        // }

        //move in direction of transform.right at speed of velocity
        Vector3 newPos = gameObject.transform.position;
        newPos += gameObject.transform.up * velocity * Time.deltaTime;
        gameObject.transform.position = newPos;
    }
}
