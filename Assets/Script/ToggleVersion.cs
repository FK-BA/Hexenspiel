using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToggleObjects : MonoBehaviour
{
    public GameObject kristalleV1;
    public GameObject kristalleV2;
    public Button buttonV1;
    public Button buttonV2;

    private Color color1 = new Color(0.506f, 0.886f, 1.0f); // Color for button 1 (81E2FF)
    private Color color2 = Color.white; // Color for button 2 (white)

    private void Start()
    {
        // Attach the button click listeners
        buttonV1.onClick.AddListener(ActivateKristalleV1);
        buttonV2.onClick.AddListener(ActivateKristalleV2);

        // Initially, activate KristalleV1 and set button colors accordingly
        ActivateKristalleV1();
    }

    private void ActivateKristalleV1()
    {
        kristalleV1.SetActive(true);
        kristalleV2.SetActive(false);
        // Set button colors
        SetButtonColors(buttonV1, color1);
        SetButtonColors(buttonV2, color2);
    }

    private void ActivateKristalleV2()
    {
        kristalleV1.SetActive(false);
        kristalleV2.SetActive(true);
        // Set button colors
        SetButtonColors(buttonV1, color2);
        SetButtonColors(buttonV2, color1);
    }

    private void SetButtonColors(Button button, Color color)
    {
        // Get the button's image component
        Image image = button.GetComponent<Image>();
        // Set the button's color
        if (image != null)
        {
            image.color = color;
        }
    }
}