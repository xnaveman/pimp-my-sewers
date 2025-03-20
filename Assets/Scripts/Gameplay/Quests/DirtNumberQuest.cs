using UnityEngine;

public class DirtNumberQuest : MonoBehaviour
{
    // Maximum distance from the camera center to detect this object.
    public float detectionDistance = 5f;
    
    private Camera mainCamera;
    private bool messageShown = false;
    
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if(mainCamera == null)
            return;
        
        // Create a ray from the center of the screen (viewport (0.5,0.5)).
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        
        // Cast a ray and check if it hits within the detection distance.
        if (Physics.Raycast(ray, out hit, detectionDistance))
        {
            // Check if the object we hit is this object.
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (!messageShown)
                {
                    HudManager.instance.showMessage("Tas de terre néttoyés : " + GameManager.instance.dirtPilesCollected + "/12 (Trouvez le balai !)");
                    messageShown = true;
                } else if (!messageShown && GameManager.instance.broomUnlocked == true)
                {
                    HudManager.instance.showMessage("Tas de terre néttoyés : " + GameManager.instance.dirtPilesCollected + "/12");
                    messageShown = true;
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