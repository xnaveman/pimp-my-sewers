using UnityEngine;

public class FlashlightPickup : MonoBehaviour
{
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
            
            // Add the item to the HUD bar.
            // (Ensure that your HudManager script has an "addItem" function and that
            // the Item enum has the proper item for the flashlight pickup.)
            HudManager.instance.addItem(Item.FlashLight);
            
            // Deactivate the pickup placeholder (lightTorch) so that it disappears.
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