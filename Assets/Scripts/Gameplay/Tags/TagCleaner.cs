using UnityEngine;
using UnityEngine.UI;

public class TagCleaner : MonoBehaviour
{
    // Maximum distance from the camera center where the tag can be targeted.
    public float detectionDistance = 1f;
    // Time (in seconds) the player must hold the right mouse button.
    public float requiredHoldTime = 2f;
    
    // UI progress bar reference (set this in the Inspector).
    [SerializeField] private Image progressBar;

    private Camera mainCamera;
    private float holdTimer = 0f;
    private bool cleaned = false;

    // Define our tag-specific messages.
    private const string cleaningMessage = "Nettoyage ...";
    private const string promptMessage = "Maintenir clic droit pour nettoyer";

    void Start()
    {
        mainCamera = Camera.main;
        if (progressBar != null)
            progressBar.fillAmount = 0;
    }

    void Update()
    {
        if (mainCamera == null || cleaned)
            return;
        
        // Create a ray from the center of the screen.
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        bool objectInSight = false;
        
        // Check if the ray hits within the detection distance.
        if (Physics.Raycast(ray, out hit, detectionDistance))
        {
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                objectInSight = true;
            }
        }
        
        if (objectInSight)
        {
            if (Input.GetMouseButton(1))
            {
                // Only override the HUD message if it is empty or already one of our tag messages.
                if (HudManager.instance.CurrentMessage == "" ||
                    HudManager.instance.CurrentMessage == cleaningMessage ||
                    HudManager.instance.CurrentMessage == promptMessage)
                {
                    HudManager.instance.showMessage(cleaningMessage);
                }
                holdTimer += Time.deltaTime;
                if (progressBar != null)
                    progressBar.fillAmount = Mathf.Clamp01(holdTimer / requiredHoldTime);
                
                if (holdTimer >= requiredHoldTime)
                {
                    CleanTag();
                }
            }
            else
            {
                // If button is released early, set our prompt only if our message is active.
                if (HudManager.instance.CurrentMessage == "" ||
                    HudManager.instance.CurrentMessage == cleaningMessage)
                {
                    HudManager.instance.showMessage(promptMessage);
                }
                ResetHold();
            }
        }
        else
        {
            // Only erase the message if it is one of our tag messages.
            if (HudManager.instance.CurrentMessage == cleaningMessage ||
                HudManager.instance.CurrentMessage == promptMessage)
            {
                HudManager.instance.eraseMessage();
            }
            ResetHold();
        }
    }
    
    void ResetHold()
    {
        holdTimer = 0;
        if (progressBar != null)
            progressBar.fillAmount = 0;
    }
    
    void CleanTag()
    {
        if (cleaned)
            return;
            
        cleaned = true;
        // Increase the global graffCollected counter.
        GameManager.instance.graffCollected++;
        HudManager.instance.showTimedMessage("Graffiti nettoyé (" + GameManager.instance.graffCollected + "/5)");
        
        Destroy(gameObject);
    }
}