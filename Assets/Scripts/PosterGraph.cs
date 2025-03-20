using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterGraph : MonoBehaviour
{
    // Maximum distance from the camera center to detect this object.
    public float detectionDistance = 1f;
    
    private Camera mainCamera;
    private bool messageShown = false;
    
    // Array of materials to cycle through when painting the poster.
    public Material[] posterMaterials;
    // Index to track the current material.
    private int currentMaterialIndex = 0;
    
    // Number of paint bombs required to enable painting.
    public int requiredPaintBombs = 5;
    
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (mainCamera == null)
            return;
        
        // Create a ray from the center of the screen.
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        
        // Cast a ray and check if it hits within the detection distance.
        if (Physics.Raycast(ray, out hit, detectionDistance))
        {
            // Check if the ray hit this object.
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Check if the player has enough paint bombs.
                if (GameManager.instance.sprayCansCollected >= requiredPaintBombs)
                {
                    if (!messageShown)
                    {
                        HudManager.instance.showMessage("Clic droit pour peindre le poster");
                        messageShown = true;
                    }
                    
                    // On right-click, cycle to the next material.
                    if (Input.GetMouseButtonDown(1))
                    {
                        Renderer rend = GetComponent<Renderer>();
                        if (rend != null && posterMaterials != null && posterMaterials.Length > 0)
                        {
                            currentMaterialIndex = (currentMaterialIndex + 1) % posterMaterials.Length;
                            rend.material = posterMaterials[currentMaterialIndex];
                        }
                        HudManager.instance.showTimedMessage("Poster peint !");
                    }
                }
                else // Not enough paint bombs have been collected.
                {
                    if (!messageShown)
                    {
                        HudManager.instance.showMessage("Ramassez toutes les bombes de peinture !");
                        messageShown = true;
                    }
                    
                    if (Input.GetMouseButtonDown(1))
                    {
                        HudManager.instance.showTimedMessage("Ramassez toutes les bombes de peinture !");
                    }
                }
                
                return;
            }
        }
        
        // If the ray doesn't hit this object, erase the message.
        if (messageShown)
        {
            HudManager.instance.eraseMessage();
            messageShown = false;
        }
    }
}