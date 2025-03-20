using UnityEngine;
using UnityEngine.UI;

public class Params : MonoBehaviour
{
    public Slider pixelationSlider;
    public Slider luminositySlider;
    
    // Global variable to store the pixelation value.
    public static float pixelation = 0.1f;
    public static float luminosity = 50f;

    void Start()
    {
        if(pixelationSlider != null)
            pixelation = pixelationSlider.value;
        if(luminositySlider != null)
            luminosity = luminositySlider.value;
    }

    void Update()
    {
        if(pixelationSlider != null)
            pixelation = pixelationSlider.value;
        if(luminositySlider != null)
            luminosity = luminositySlider.value;
    }
}