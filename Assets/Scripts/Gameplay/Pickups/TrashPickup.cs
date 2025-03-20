using UnityEngine;
using System.Collections;

public class TrashPickup : MonoBehaviour
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
            GameManager.instance.trashCollected++;
            HudManager.instance.showTimedMessage("Poubelle ramassée (" + GameManager.instance.trashCollected + "/8)");
            AudioManager am = AudioManager.instance;
			am.PlaySFX(am.sfx_list.sfx_trash);
            
            // Destroy the pickup (and its parent if needed).
            if(transform.parent != null)
                Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}