using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtPickup : MonoBehaviour
{
    private bool playerInRange = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (GameManager.instance.broomUnlocked)
            {
                playerInRange = true;
                HudManager.instance.showMessage("Clic droit pour nettoyer");
            }
            else
            {
                playerInRange = true;
                HudManager.instance.showMessage("Vous devez débloquer le balai pour nettoyer");
            }
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
            if (!GameManager.instance.broomUnlocked)
            {
                HudManager.instance.eraseMessage();
                HudManager.instance.showTimedMessage("Vous devez débloquer le balai pour nettoyer");
                return;
            } else if (GameManager.instance.broomUnlocked )
            {
                HudManager.instance.eraseMessage();
                GameManager.instance.dirtPilesCollected++;

                if(transform.parent != null)
                    Destroy(transform.parent.gameObject);
                Destroy(gameObject);

                HudManager.instance.showTimedMessage("Pile de terre nettoyée ! (" + GameManager.instance.dirtPilesCollected + "/12)");
                return;
            }
        }
    }
}
