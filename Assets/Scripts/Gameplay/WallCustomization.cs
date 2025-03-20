using UnityEngine;

public class WallCustomization : MonoBehaviour
{
    private bool playerInRange = false;
    
    [Header("Customization Settings")]
    [Tooltip("The GameObject whose materials will be changed. Drag the target here.")]
    public GameObject targetObject;
    [Tooltip("List of materials to cycle through.")]
    public Material[] customMaterials;
    
    // Current index to track which material to apply.
    private int currentMaterialIndex = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            HudManager.instance.showMessage("Clic droit pour changer les murs");
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
            CycleMaterials();
        }
    }
    
    private void CycleMaterials()
    {
        // Ensure target and materials are set.
        if(targetObject == null || customMaterials == null || customMaterials.Length == 0)
            return;
        
        // Cycle to the next material.
        currentMaterialIndex = (currentMaterialIndex + 1) % customMaterials.Length;
        
        // Get the Renderer component of the target.
        Renderer rend = targetObject.GetComponent<Renderer>();
        if(rend != null)
        {
            Material[] mats = rend.materials;
            
            // If the target has at least 3 material slots, update slots 0, 1 and 2.
            if(mats.Length >= 3)
            {
                mats[0] = customMaterials[currentMaterialIndex];
                mats[2] = customMaterials[currentMaterialIndex];
            }
            else // Otherwise, update all available materials.
            {
                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i] = customMaterials[currentMaterialIndex];
                }
            }
            rend.materials = mats;
        }
    }
}