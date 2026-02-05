using UnityEngine;

public class AnimatedUISign : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator animator;
    private bool switchUp;
    private bool switchDown;

    void Start()
    {
        switchUp = false;
        switchDown = false;
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagement.Instance.GameState == GameState.Shop && switchUp == false)
        {
            animator.SetBool("ShopOpen", true);
            switchUp = true;
            switchDown = false;
        }
        if (GameManagement.Instance.GameState == GameState.Prerun && switchDown == false)
        {
            animator.SetBool("ShopOpen", false);
            switchDown = true;
            switchUp = false;
        }
    }
}
