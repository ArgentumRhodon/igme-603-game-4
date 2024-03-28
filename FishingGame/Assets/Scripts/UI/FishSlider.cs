using UnityEngine;
using UnityEngine.UI;

public enum FillColor
{
    Normal,
    Bad,
    Success
}

public class FishSlider : MonoBehaviour
{
    [SerializeField] private Slider fishSlider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Image handleImage;

    [SerializeField] private Color normalFillColor;
    [SerializeField] private Color badFillColor;
    [SerializeField] private Color successFillColor;

    void Start()
    {
        // Initalize the slider
        fishSlider.interactable = false;
        fishSlider.value = 0;
        MoveSliderImage(fishSlider.value);
        SetFillColor(FillColor.Normal);
    }

    public void UpdateSliderValue(float newValue)
    {
        newValue = Mathf.Clamp01(newValue);

        fishSlider.value = newValue;

        MoveSliderImage(newValue);
    }

    public void SetFillColor(FillColor fillColor)
    {
        switch (fillColor)
        {
            case FillColor.Normal:
                fillImage.color = normalFillColor;
                break;
            case FillColor.Bad:
                fillImage.color = badFillColor;
                break;
            case FillColor.Success:
                fillImage.color = successFillColor;
                break;
        }
    }

    public void ResetSlider()
    {
        // Reset slide value, color, and handle position
        fishSlider.value = 0;
        MoveSliderImage(fishSlider.value);
        SetFillColor(FillColor.Normal);
    }

    private void MoveSliderImage(float val)
    {
        float handlePositionX = val * fishSlider.GetComponent<RectTransform>().rect.width;
        handleImage.rectTransform.anchoredPosition = new Vector2(handlePositionX - 50, handleImage.rectTransform.anchoredPosition.y);
    }
}
