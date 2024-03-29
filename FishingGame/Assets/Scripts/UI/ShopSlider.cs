using UnityEngine;
using UnityEngine.UI;

public class ShopSlider : MonoBehaviour
{
    Scrollbar scrollbar;

    // Manual override of inital value
    // Content size fitter always sets it to 0.5
    void Start()
    {
        scrollbar = gameObject.GetComponent<Scrollbar>();
        scrollbar.value = 0;
    }

}
