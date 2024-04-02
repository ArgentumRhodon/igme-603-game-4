using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    private Image buttonImage;

    void Start()
    {
        button = gameObject.GetComponent<Button>();
        buttonImage = gameObject.transform.Find("Image").GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetImageActive(true); // Set image to active
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetImageActive(false); // Set image to inactive
    }

    void SetImageActive(bool isActive)
    {
        buttonImage.gameObject.SetActive(isActive);
    }
}
