using UnityEngine;
using UnityEngine.UI;

public class ToggleUIElement : MonoBehaviour
{
    [SerializeField] GameObject element;

    private void Start()
    {
        // Assuming you have a button component attached
        Button button = GetComponent<Button>();
        if (button != null)
        {
            // Add a listener to the button click event
            button.onClick.AddListener(ToggleActiveState);
        }
        else
        {
            Debug.LogWarning("ToggleElement.cs: No button component found!");
        }
    }

    private void ToggleActiveState()
    {
        element.SetActive(!element.activeSelf);
    }
}
