using UnityEngine;

public class XRLightToggle : MonoBehaviour
{
    public Light pointLight;  // assign manually in Inspector
    private bool isOn = false;

    void Start()
    {
        if (pointLight != null)
            pointLight.intensity = 0f;
    }

    // This MUST be public, void, and parameterless to show in UnityEvents
    public void ToggleLight()
    {
        isOn = !isOn;
        if (pointLight != null)
            pointLight.intensity = isOn ? 50f : 0f;
    }
}
