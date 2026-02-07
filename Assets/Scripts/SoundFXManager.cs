using UnityEngine;

public class SoundFXManager : MonoBehaviour
{

    [SerializeField]
    AudioClip[] crashSounds, upgradeSounds;
    [SerializeField]
    AudioClip cannonSound, chainSound, wallCrash, failedPurchaseSound;
    //Other sound effects will go here

    [SerializeField]
    AudioSource source;

    public static SoundFXManager Instance { get; private set; }
    public AudioClip[] CrashSounds { get { return crashSounds; } }
    public AudioClip[] UpgradeSounds { get { return upgradeSounds; } }
    public AudioClip CannonSound { get { return cannonSound; } }
    public AudioClip ChainSound { get { return chainSound; } }
    public AudioClip WallCrash { get { return wallCrash; } }
    public AudioClip FailedPurchaseSound { get { return failedPurchaseSound; } }
    public AudioSource Source { get { return source; } }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
