using Unity.VisualScripting;
using UnityEngine;

public class ReactionChanges : MonoBehaviour
{
    private Animator animator;
    private bool coolShit;
    private int randomFace;

    private float time;

    public bool CoolShit
    {
        get { return coolShit; }
        set { coolShit = value; }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        coolShit = false;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        randomFace = Random.Range(1, 4);
        if (coolShit == false)
        {
            animator.SetBool("CoolShit", false);
        }
        if (coolShit == true)
        {
            animator.SetBool("CoolShit", true);
            animator.SetInteger("RandomFace", randomFace);
        }

        /*
        if (coolShit == true)
        {
            time += Time.deltaTime;
            if (time > 3)
            {
                time = 0;
                coolShit = false;
            }
        }
        */
    }
}
