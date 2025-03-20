using UnityEngine;

public class AmbientLightController : MonoBehaviour
{
    float lighting = Params.luminosity;

    void Start()
    {
        // Use Params.luminosity as a multiplier.
        // For instance, if 15 is "normal" brightness, dividing by 15 yields 1 at default.
        RenderSettings.ambientLight = new Color(lighting / 255f, lighting / 255f, lighting / 255f);
    }
}