using UnityEngine;

public class ToggleFlashlight : MonoBehaviour
{
    private Light flashLight;
    private bool isOn = false;
    
    private void Awake()
    {
        flashLight = GetComponent<Light>();
        if (flashLight == null)
        {
            Debug.LogWarning("No Light component found on " + gameObject.name);
        }
    }
    
    private void Start()
    {
        // Ensure the flashlight starts off.
        SetFlashlight(false);
    }
    
    private void Update()
    {
        // Only allow toggling if the flashlight has been unlocked.
        if (Input.GetKeyDown(KeyCode.T) && FlashlightPickup.flashlightUnlocked)
        {
            isOn = !isOn;
            SetFlashlight(isOn);
        }
    }
    
    public void SetFlashlight(bool active)
    {
        if (flashLight != null)
        {
            flashLight.enabled = active;
        }
    }
    
    // Call this method to force-enable the flashlight (when picked up).
    public void ForceToggleOn()
    {
        isOn = true;
        SetFlashlight(true);
    }
}