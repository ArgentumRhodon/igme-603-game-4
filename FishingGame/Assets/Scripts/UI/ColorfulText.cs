using UnityEngine;
using TMPro;

public class TextColorCycler : MonoBehaviour
{
    public TextMeshProUGUI textMeshProText;
    public float cycleSpeed = 0.3f;
    private Color[] colors = { Color.red, Color.blue, Color.green, Color.yellow, Color.magenta, Color.cyan };
    private int currentIndex = 0;
    private float timer = 0f;

    void Update()
    {
        // Increment timer
        timer += Time.deltaTime;

        // Check if it's time to change color
        if (timer >= cycleSpeed)
        {
            // Reset timer
            timer = 0f;

            // Change color
            currentIndex = (currentIndex + 1) % colors.Length;
            textMeshProText.color = colors[currentIndex];
        }
    }
}
