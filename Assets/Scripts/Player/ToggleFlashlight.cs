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
        // Ensure the flashlight is off when the game starts.
        SetFlashlight(false);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isOn = !isOn;
            SetFlashlight(isOn);
        }
    }
    
    private void SetFlashlight(bool active)
    {
        if (flashLight != null)
        {
            flashLight.enabled = active;
        }
    }
}