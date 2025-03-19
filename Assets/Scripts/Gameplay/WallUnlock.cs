using UnityEngine;
using System.Collections;

public class WallUnlock : MonoBehaviour
{
    private bool playerInRange = false;
    public GameObject targetObject;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            // Optionally, display an empty or informative message.
            HudManager.instance.showMessage("Clic droit pour débloquer les murs");
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
            // Check if all graffiti (4) are collected.
            if(GameManager.instance.graffCollected >= 4)
            {
                HudManager.instance.eraseMessage();
                targetObject.SetActive(true);
                HudManager.instance.showTimedMessage("Murs débloqués !");
                
                // Destroy this unlock object (or its parent if needed).
                if(transform.parent != null)
                    Destroy(transform.parent.gameObject);
                Destroy(gameObject);
                
                // Remove the message after 2 seconds.
                HudManager.instance.RemoveMessageAfterDelay(2f);
            }
            else
            {
                // Not enough graffiti collected.
                HudManager.instance.showTimedMessage("Collectez tous les graff pour activer l'objet !");
                HudManager.instance.RemoveMessageAfterDelay(2f);
            }
        }
    }
}