using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    void Start()
    {
        // Get slider
        Slider slider = GetComponent<Slider>();

        if (slider == null)
        {
            Debug.LogWarning("MusicSlider.cs: No slider found on " + gameObject.name);
            return;
        }

        // Set inital slider position depending on starting volume
        slider.value = SoundManager.Instance.GetMusicVolume();

        // Add listener to slider
        slider.onValueChanged.AddListener(ChangeMusicVolume);
    }

    // Calls SoundManager to set volume
    private void ChangeMusicVolume(float volume)
    {
        SoundManager.Instance.SetMusicVolume(volume);
    }
}
