using Unity.Mathematics;
using UnityEngine;

public class SmoothFollowCamera : MonoBehaviour
{
    [Header("Target Following")]
    public Transform target;
    public float smoothingRatio = 0.125f;
    public bool lockX = false;
    public bool lockY = false;
    public Vector2 offset = Vector2.zero;

    [Header("Zoom")]
    public float minOrthographicSize = 10f;
    public float maxOrthographicSize = 100f;
    public float framePadding = 2f;
    public float zoomSmoothTime = 0.1f;


    private float initialZ;
    private float fixedBottomY; // The fixed bottom edge of the camera view
    private Vector3 velocity = Vector3.zero;
    private float zoomVelocity = 0f;
    private Camera cam;
    private Renderer targetRenderer;
    private Collider2D targetCollider2D;

    void Start()
    {
        initialZ = transform.position.z;
        cam = GetComponent<Camera>();

        // Calculate and store the initial bottom edge of the camera
        fixedBottomY = transform.position.y - cam.orthographicSize;

        if (target != null)
        {
            targetRenderer = target.GetComponent<Renderer>();
            targetCollider2D = target.GetComponent<Collider2D>();
        }
    }

    void LateUpdate()
    {
        if (target == null || cam == null) return;

        Vector3 desiredPosition = new Vector3(
            lockX ? transform.position.x : target.position.x + offset.x,
            transform.position.y, // Will be calculated below
            initialZ
        );


        Bounds targetBounds = GetTargetBounds();

        float requiredSize = CalculateRequiredCameraSize(targetBounds);

        // Smooth zoom
        float targetSize = Mathf.Clamp(requiredSize, minOrthographicSize, maxOrthographicSize);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetSize, ref zoomVelocity, zoomSmoothTime);

        // Update Y position to maintain fixed bottom edge
        if (lockY)
        {
            desiredPosition.y = transform.position.y;
        }
        else
        {
            desiredPosition.y = fixedBottomY + cam.orthographicSize + offset.y;
        }



        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            smoothingRatio,
            float.PositiveInfinity,
            Time.smoothDeltaTime
        );
    }

    private Bounds GetTargetBounds()
    {
        Bounds bounds;
        if (targetRenderer != null)
        {
            bounds = targetRenderer.bounds;
        }
        else if (targetCollider2D != null)
        {
            bounds = targetCollider2D.bounds;
        }
        else
        {
            bounds = new Bounds(target.position, Vector3.one);
        }

        return bounds;
    }

    private float CalculateRequiredCameraSize(Bounds bounds)
    {
        float targetTopEdge = bounds.max.y + framePadding;

        float requiredHeight = targetTopEdge - fixedBottomY;

        float requiredSize = requiredHeight / 2f;

        // float targetWidth = bounds.size.x;
        // float aspectRatio = cam.aspect;
        // float sizeForWidth = (targetWidth / aspectRatio) / 2f + framePadding / aspectRatio;

        return requiredSize;

        // return Mathf.Max(requiredSize, sizeForWidth);
    }

}