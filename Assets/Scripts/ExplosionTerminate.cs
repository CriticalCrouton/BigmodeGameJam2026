using UnityEngine;

public class ExplosionTerminate : MonoBehaviour
{
    private float time; //Elapsed Time

    [SerializeField]
    float existenceTime; //The amount of time that the explosion should exist for.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 0;
    }

    //Destroys the explosion after it has existed for the required amount of time
    //Existence is pain to an explosion Jerry!
    void Update()
    {
        time += Time.deltaTime;
        if (time > existenceTime)
        {
            Destroy(gameObject);
        }
    }
}
