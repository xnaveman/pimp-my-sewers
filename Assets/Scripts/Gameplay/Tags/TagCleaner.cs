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

    void Start()
    {
        mainCamera = Camera.main;
        if (progressBar != null)
            progressBar.fillAmount = 0;
    }

    void Update()
    {
        if(mainCamera == null || cleaned)
            return;
        
        // Create a ray from the center of the screen.
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        bool objectInSight = false;
        
        // Check if the ray hits within the detection distance.
        if(Physics.Raycast(ray, out hit, detectionDistance))
        {
            if(hit.collider != null && hit.collider.gameObject == gameObject)
            {
                objectInSight = true;
            }
        }
        
        // If the tag is being looked at...
        if (objectInSight)
        {
            // ...and the player holds down the right click,
            // increment the hold timer and update the progress bar.
            if(Input.GetMouseButton(1))
            {
                HudManager.instance.showMessage("Nettoyage ...");
                holdTimer += Time.deltaTime;
                if(progressBar != null)
                    progressBar.fillAmount = Mathf.Clamp01(holdTimer / requiredHoldTime);
                
                // When the required hold time has been reached, clean the tag.
                if(holdTimer >= requiredHoldTime)
                {
                    CleanTag();
                }
            }
            else
            {
                // If the button is released early, reset the timer and progress bar.
                ResetHold();
                HudManager.instance.showMessage("Maintenir clic droit pour nettoyer");
            }
        }
        else
        {
            // If not looking at the tag, reset the timer and progress bar.
            HudManager.instance.eraseMessage();
            ResetHold();
        }
    }
    
    void ResetHold()
    {
        holdTimer = 0;
        if(progressBar != null)
            progressBar.fillAmount = 0;
    }
    
    void CleanTag()
    {
        if(cleaned)
            return;
            
        cleaned = true;
        // Increase the global graffCollected counter.
        GameManager.instance.graffCollected++;
        // Show a timed HUD message indicating the cleaning is complete.
        HudManager.instance.showTimedMessage("Graffiti nettoyé (" + GameManager.instance.graffCollected + "/5)");
        // Optionally, you can also use HudManager.RemoveMessageAfterDelay(2f);
        
        // Destroy this tag GameObject.
        Destroy(gameObject);
    }
}