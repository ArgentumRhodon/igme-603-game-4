using UnityEngine;
using UnityEngine.UI;

public class FishSlider : MonoBehaviour
{
    private Slider fishSlider;
    [SerializeField] private Image handleImage;

    void Start()
    {
        // Initalize the slider
        fishSlider = gameObject.GetComponent<Slider>();
        fishSlider.interactable = false;
        fishSlider.value = 0;
    }

    public void UpdateSliderValue(float newValue)
    {
        newValue = Mathf.Clamp01(newValue);

        fishSlider.value = newValue;

        MoveSliderImage(newValue);
    }

    public void ResetSlider()
    {
        fishSlider.value = 0;
        MoveSliderImage(fishSlider.value);
    }

    private void MoveSliderImage(float val)
    {
        float handlePositionX = val * fishSlider.GetComponent<RectTransform>().rect.width;
        handleImage.rectTransform.anchoredPosition = new Vector2(handlePositionX - 50, handleImage.rectTransform.anchoredPosition.y);
    }
}
