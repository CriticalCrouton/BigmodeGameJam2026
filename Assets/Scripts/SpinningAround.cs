using UnityEngine;

public class SpinningAround : MonoBehaviour
{
    //AWESOME
    void Start()
    {
        
    }

    // COOLEST THING EVER MADE
    void Update()
    {
        gameObject.transform.Rotate(66 * Time.deltaTime, 32 * Time.deltaTime, 55 * Time.deltaTime);
    }
}
