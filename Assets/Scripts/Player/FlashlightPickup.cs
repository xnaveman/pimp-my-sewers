using UnityEngine;

public class FlashlightPickup : MonoBehaviour
{
    // Global variable that determines if the flashlight has been picked up.
    public static bool flashlightUnlocked = false;
    private bool playerInRange = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            HudManager.instance.showMessage("F pour prendre");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
            HudManager.instance.eraseMessage();
        }
    }
    
    private void Update()
    {
        if(playerInRange && !flashlightUnlocked && Input.GetKeyDown(KeyCode.F))
        {
            flashlightUnlocked = true;
            // Automatically activate the flashlight.
            ToggleFlashlight tf = GetComponentInParent<ToggleFlashlight>();
            if(tf != null)
            {
                tf.ForceToggleOn();
            }
            HudManager.instance.eraseMessage();
            // Deactivate the parent placeholder (lightTorch) so that it disappears from the table.
            if(transform.parent != null)
            {
                transform.parent.gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}