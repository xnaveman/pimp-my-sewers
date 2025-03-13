using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    //Vitesse en marchant et en courant
    [SerializeField] private float walk, run;

    // Sensitivities for mouse movement in FPS (can be adjusted in the inspector)
    [SerializeField] private float horizontalSensitivity = 1200f;
    [SerializeField] private float verticalSensitivity = 700f;
    private float speed;

    private bool isMoving = false;
    private bool isRunning = false;
    
    private CharacterController cc;

    private float X, Y;
    
    //Pour les bruits de pas
    [SerializeField] private AudioClip[] sfx_steps;
    private int num_step = 0;
    private float step_timer = 0.0f;
    private float max_step_timer = 0.5f;
    private AudioSource audio_steps;

    private void Start()
    {
        speed = walk;
        cc = GetComponent<CharacterController>();
        audio_steps = GetComponent<AudioSource>();
        cc.enabled = true;
        
        // Lock and hide the cursor for a classic FPS experience
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!HudManager.pause)
        {
            // Process mouse look using separate sensitivities
            float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity * Time.deltaTime;
            
            X += mouseX;
            Y -= mouseY; // Invert mouseY for natural feel
            
            // Clamp vertical rotation so you can't over-look up or down.
            const float MIN_Y = -60f; // looking down limit (can't move more)
            const float MAX_Y = 60f;  // looking up limit (can't turn back)
            Y = Mathf.Clamp(Y, MIN_Y, MAX_Y);

            transform.localRotation = Quaternion.Euler(Y, X, 0.0f);
            
            // Movement input
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 forward = transform.forward * vertical;
            Vector3 right = transform.right * horizontal;
            cc.SimpleMove((forward + right) * speed);
            
            // If Left Shift is pressed, run
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = run;
                isRunning = true;
            }
            else
            {
                isRunning = false;
                speed = walk;
            }
            
            // Footsteps audio when moving
            if (horizontal != 0 || vertical != 0)
            {
                if (step_timer <= 0)
                {
                    audio_steps.clip = sfx_steps[num_step];
                    audio_steps.Play();
                    
                    step_timer = max_step_timer;
                    if (isRunning)
                    { 
                        // when running, faster step rate
                        step_timer /= 2;
                    }
                    num_step = (num_step + 1) % sfx_steps.Length;
                }
                else 
                {
                    step_timer -= Time.deltaTime;
                }
            }
            else
            {
                step_timer = 0.1f;
            }
        }
    }
}