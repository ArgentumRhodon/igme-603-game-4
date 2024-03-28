using UnityEngine;
using UnityEngine.UI;

// Enum for shop sections
public enum Section
{
    Upgrades,
    Boosts,
    Currency
}

public class NavigationBar : MonoBehaviour
{
    public GameObject upgradesSection;
    public GameObject boostsSection;
    public GameObject currencySection;

    private GameObject activeSection;

    void Start()
    {
        // Default to upgrades
        activeSection = upgradesSection;
        ShowUpgradeSection();
    }

    public void ShowUpgradeSection()
    {
        activeSection = upgradesSection;
        upgradesSection.SetActive(true);
        boostsSection.SetActive(false);
        currencySection.SetActive(false);
    }

    public void ShowBoostsSection()
    {
        activeSection = boostsSection;
        upgradesSection.SetActive(false);
        boostsSection.SetActive(true);
        currencySection.SetActive(false);
    }

    public void ShowCurrencySection()
    {
        activeSection = currencySection;
        upgradesSection.SetActive(false);
        boostsSection.SetActive(false);
        currencySection.SetActive(true);
    }

    public bool IsSectionActive(Section section)
    {
        switch (section)
        {
            case Section.Upgrades:
                return activeSection == upgradesSection;
            case Section.Boosts:
                return activeSection == boostsSection;
            case Section.Currency:
                return activeSection == currencySection;
            default:
                Debug.LogWarning("NavigationBar.cs: Invalid section!");
                return false;
        }
    }
}
