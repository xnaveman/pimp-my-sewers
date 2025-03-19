using UnityEngine;

public class GroundCustomization : MonoBehaviour
{
    private bool playerInRange = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            HudManager.instance.showMessage("Clic droit pour changer le sol (" + GameManager.instance.groundDecorationCollected + "/3 débloqués)");
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
        }
    }
}