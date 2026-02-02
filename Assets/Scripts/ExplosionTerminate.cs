using UnityEngine;

public class ExplosionTerminate : MonoBehaviour
{
    private float time;

    [SerializeField]
    float existenceTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > existenceTime)
        {
            Destroy(gameObject);
        }
    }
}
