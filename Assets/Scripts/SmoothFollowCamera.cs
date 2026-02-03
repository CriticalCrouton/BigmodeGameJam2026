using Unity.Mathematics;
using UnityEngine;

public class SmoothFollowCamera : MonoBehaviour
{
    public Transform target;
    public float smoothingRatio = 0.125f;
    public bool lockX = false;
    public bool lockY = false;
    public Vector2 offset = Vector2.zero;
    private float initialZ;
    private Vector3 velocity = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialZ = transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position,
                new Vector3(lockX ? transform.position.x : target.position.x + offset.x,
                            lockY ? transform.position.y : target.position.y + offset.y,
                            initialZ),
                ref velocity,
                smoothingRatio,
                float.PositiveInfinity,
                Time.smoothDeltaTime);
        }
    }
}
