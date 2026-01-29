using UnityEngine;

public class SpinningAround : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(66 * Time.deltaTime, 32 * Time.deltaTime, 55 * Time.deltaTime);
    }
}
