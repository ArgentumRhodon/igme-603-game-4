using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    void Start()
    {
        // Get slider
        Slider slider = GetComponent<Slider>();

        if (slider == null)
        {
            Debug.LogWarning("SoundSlider.cs: No slider found on " + gameObject.name);
            return;
        }

        // Set inital slider position depending on starting volume
        slider.value = SoundManager.Instance.GetSFXVolume();

        // Add listener to slider
        slider.onValueChanged.AddListener(ChangeSFXVolume);
    }

    // Calls SoundManager to set volume
    private void ChangeSFXVolume(float volume)
    {
        SoundManager.Instance.SetSFXVolume(volume);
    }
}
