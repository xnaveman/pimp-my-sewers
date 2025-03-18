using UnityEngine;
using UnityEngine.EventSystems;

public class LogoBounce : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Rotation Settings")]
    [Tooltip("Maximum rotation angle (in degrees) to the left/right.")]
    public float rotationAmplitude = 10f;
    [Tooltip("Speed of the rotation animation.")]
    public float rotationSpeed = 2f;

    [Header("Scale/Bounce Settings")]
    [Tooltip("Amount of scale change. 0 means no bounce.")]
    public float scaleAmplitude = 0.1f;
    [Tooltip("Speed of the scaling animation.")]
    public float scaleSpeed = 2f;

    [Header("Animation Delay")]
    [Tooltip("Delay (in seconds) before starting the animation.")]
    public float startDelay = 0f;
    
    [Header("Hover Settings")]
    [Tooltip("Additional scale applied when the pointer is hovering.")]
    public float hoverAdditionalScale = 0.2f;
    [Tooltip("Speed of the smooth transition when hover state changes.")]
    public float hoverTransitionSpeed = 5f;

    private Vector3 baseScale;
    private Quaternion baseRotation;
    private bool isHovered = false;

    void Start()
    {
        // Cache the original scale and rotation
        baseScale = transform.localScale;
        baseRotation = transform.rotation;
    }

    void Update()
    {
        // If within delay time, maintain original transform.
        if(Time.time < startDelay)
        {
            transform.localScale = baseScale;
            transform.rotation = baseRotation;
            return;
        }
        
        // Calculate time offset after delay
        float t = Time.time - startDelay;
        float angle = Mathf.Sin(t * rotationSpeed) * rotationAmplitude;
        transform.rotation = baseRotation * Quaternion.Euler(0, 0, angle);

        // Compute the target scale factor from the bounce animation.
        float baseScaleFactor = 1 + Mathf.Sin(t * scaleSpeed) * scaleAmplitude;
        // If hovered, add additional scale.
        float targetScaleFactor = baseScaleFactor + (isHovered ? hoverAdditionalScale : 0);
        
        // Smoothly transition to the target scale using Lerp.
        Vector3 targetScale = baseScale * targetScaleFactor;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * hoverTransitionSpeed);
    }

    // Called when the pointer enters the UI element.
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }
    
    // Called when the pointer exits the UI element.
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }
}