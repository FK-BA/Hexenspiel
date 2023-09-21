using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleCounter : MonoBehaviour
{
    public Toggle toggle;
    public GameObject[] objectsToActivate;
    public TextMeshProUGUI scoreText;
    public int newHighScoreThreshold = 20;
    public int defaultHighScoreThreshold = 15;

    // Reference to the Toggle's background image.
    private Image toggleBackground;

    private void Start()
    {
        toggle.isOn = false;
        toggle.onValueChanged.AddListener(OnToggleValueChanged);

        // Find the Toggle's background image within its children.
        toggleBackground = toggle.GetComponentInChildren<Image>();

        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(false);
        }

        scoreText.text = "0/" + defaultHighScoreThreshold;
    }

    private void OnToggleValueChanged(bool newValue)
    {
        if (newValue)
        {
            // Toggle turned on
            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
            }

            HighScoreIndicator highScoreIndicator = FindObjectOfType<HighScoreIndicator>();
            if (highScoreIndicator != null)
            {
                highScoreIndicator.highScoreThreshold = newHighScoreThreshold;
            }

            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.totalScore = newHighScoreThreshold;
            }

            scoreText.text = "0/" + newHighScoreThreshold;

            // Make the toggle's background transparent.
            if (toggleBackground != null)
            {
                Color backgroundColor = toggleBackground.color;
                backgroundColor.a = 0f;
                toggleBackground.color = backgroundColor;
            }
        }
        else
        {
            // Toggle turned off
            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(false);
            }

            HighScoreIndicator highScoreIndicator = FindObjectOfType<HighScoreIndicator>();
            if (highScoreIndicator != null)
            {
                highScoreIndicator.highScoreThreshold = defaultHighScoreThreshold;
            }

            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.totalScore = defaultHighScoreThreshold;
            }

            scoreText.text = "0/" + defaultHighScoreThreshold;

            // Restore the toggle's background color to its original state.
            if (toggleBackground != null)
            {
                Color backgroundColor = toggleBackground.color;
                backgroundColor.a = 1f;
                toggleBackground.color = backgroundColor;
            }
        }
    }
}