using UnityEngine;
using System.Collections;

public class BedBuild : MonoBehaviour
{
    private bool playerInRange = false;
    public GameObject targetObject;

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            HudManager.instance.showMessage("Clic droit pour construire un lit");
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
        if(playerInRange && Input.GetMouseButtonDown(1) && GameManager.instance.trashCollected >= 8)
        {
            HudManager.instance.eraseMessage();
            targetObject.SetActive(true);
            HudManager.instance.showTimedMessage("Lit construit !");
            
            // Destroy the pickup (and its parent if needed).
            if(transform.parent != null)
                Destroy(transform.parent.gameObject);
            Destroy(gameObject);
            
            // Use the persistent HudManager to remove the message after 2 seconds.
            HudManager.instance.RemoveMessageAfterDelay(2f);
        } else if (playerInRange && Input.GetMouseButtonDown(1) && GameManager.instance.trashCollected < 8) {
            HudManager.instance.showTimedMessage("Ramassez touts les déchets pour construire un lit !");
            HudManager.instance.RemoveMessageAfterDelay(2f);

        }
    }
}