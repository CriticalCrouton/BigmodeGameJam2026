using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CannonFire : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI cannonballUI;

    [SerializeField]
    GameObject cannonballFireAnimation;

    [SerializeField]
    int startingCannonballs;

    private int cannonballs;

    public int Cannonballs { get { return cannonballs; } set { cannonballs = value; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cannonballs = startingCannonballs;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
