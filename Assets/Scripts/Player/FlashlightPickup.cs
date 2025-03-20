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
            HudManager.instance.showMessage("Clic droit pour ramasser");
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
        if(playerInRange && !flashlightUnlocked && Input.GetMouseButtonDown(1))
        {
            flashlightUnlocked = true;
            AudioManager am = AudioManager.instance;
			am.PlaySFX(am.sfx_list.sfx_take);
            
            // Automatically activate the flashlight.
            ToggleFlashlight tf = GetComponentInParent<ToggleFlashlight>();
            if(tf != null)
            {
                tf.ForceToggleOn();
            }
            
            HudManager.instance.eraseMessage();
            
            // Add the item to the HUD bar.
            HudManager.instance.addItem(Item.FlashLight);
            
            // Deactivate the pickup placeholder so that it disappears.
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