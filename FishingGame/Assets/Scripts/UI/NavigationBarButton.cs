using UnityEngine;
using UnityEngine.UI;

public class NavigationBarButton : MonoBehaviour
{
    public Section sectionToShow;
    public GameObject navigationBar;
    public GameObject underline;

    void Start()
    {
        if (navigationBar == null)
        {
            Debug.LogWarning("NavigationBarButton.cs: No navigation bar set!");
            enabled = false;
        }

        // Get controller for nav bar
        NavigationBar controller = navigationBar.GetComponent<NavigationBar>();

        // Set listener for button
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => OnButtonClick(controller));

        // Set underline
        UpdateUnderline(controller);
    }

    // Detrmmines what section to show for this button on click
    private void OnButtonClick(NavigationBar controller)
    {
        switch (sectionToShow)
        {
            case Section.Upgrades:
                controller.ShowUpgradeSection();
                break;
            case Section.Boosts:
                controller.ShowBoostsSection();
                break;
            case Section.Currency:
                controller.ShowCurrencySection();
                break;
            default:
                controller.ShowUpgradeSection();
                break;
        }

        // Update the appearance of the underline based on whether this button corresponds to the active section
        NavigationBarButton[] allButtons = navigationBar.GetComponentsInChildren<NavigationBarButton>();
        foreach (var button in allButtons)
        {
            button.UpdateUnderline(controller);
        }
    }

    // Updates underline for button
    private void UpdateUnderline(NavigationBar controller)
    {
        // Determine if button should be active
        bool isActive = controller.IsSectionActive(sectionToShow);

        // Set the underline accordingly
        underline.SetActive(isActive);
    }
}
