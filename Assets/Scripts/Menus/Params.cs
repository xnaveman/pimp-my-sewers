using UnityEngine;
using UnityEngine.UI;

public class Params : MonoBehaviour
{
    public Slider pixelationSlider;
    
    // Global variable to store the pixelation value.
    public static float pixelation = 0f;

    void Start()
    {
        if(pixelationSlider != null)
            pixelation = pixelationSlider.value;
    }

    void Update()
    {
        if(pixelationSlider != null)
            pixelation = pixelationSlider.value;
    }
}