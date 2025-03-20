using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayPickup : MonoBehaviour
{
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
        if(playerInRange && Input.GetMouseButtonDown(1))
        {
            HudManager.instance.eraseMessage();
            GameManager.instance.sprayCansCollected++;
            HudManager.instance.showTimedMessage("Bombe de peinture trouvée ! (" + GameManager.instance.sprayCansCollected + "/5)");
            AudioManager am = AudioManager.instance;
			am.PlaySFX(am.sfx_list.sfx_take);
            
            // Destroy the pickup (and its parent if needed).
            if(transform.parent != null)
                Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}
