using UnityEngine;
using System.Collections.Generic;
enum MusicState
{
    Prerun,
    Run,
    Shop
}
public class GameMusic : MonoBehaviour
{
    private MusicState state;
    private AudioSource source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    AudioClip prerunMusic, runMusic, shopMusic;
    void Start()
    {
        state = MusicState.Prerun;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagement.Instance.GameState == GameState.Prerun && state != MusicState.Prerun)
        {
            state = MusicState.Prerun;
            source.pitch = 1;
            source.clip = prerunMusic;
            source.Play();
        }
        if (GameManagement.Instance.GameState == GameState.Run && state != MusicState.Run)
        {
            state = MusicState.Run;
            source.clip = runMusic;
            source.Play();
        }
        if (GameManagement.Instance.GameState == GameState.Shop && state != MusicState.Shop)
        {
            state = MusicState.Shop;
            source.pitch = 1;
            source.clip = shopMusic;
            source.Play();
        }
       
        if (PirateShipTest.Instance.Velocity < 20 && source.clip == runMusic)
        {
            float value = PirateShipTest.Instance.Velocity / 20;
            source.pitch = value;
        }
        
    }
}
