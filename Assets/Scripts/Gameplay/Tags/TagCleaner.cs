using UnityEngine;
using System.Collections;

public class TagCleaner : MonoBehaviour
{
    private bool playerInRange = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            HudManager.instance.showMessage("Clic droit pour nettoyer");
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
            GameManager.instance.graffCollected++;
            HudManager.instance.showTimedMessage("Tag effacé (" + GameManager.instance.graffCollected + "/4)");
            
            // Destroy the pickup (and its parent if needed).
            Destroy(gameObject);
            
            // Use the persistent HudManager to remove the message after 2 seconds.
        }
    }
}