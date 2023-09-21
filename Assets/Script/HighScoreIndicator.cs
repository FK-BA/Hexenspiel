using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreIndicator : MonoBehaviour
{
    public int highScoreThreshold = 15; // Set the high score threshold here
    public Material highScoreMaterial; // Assign the high score material in the editor

    private Renderer cauldronRenderer;
    private Material originalMaterial;
    private bool highScoreReached = false;

    void Start()
    {
        cauldronRenderer = GetComponent<Renderer>();
        originalMaterial = cauldronRenderer.material;
    }

    void Update()
    {
        // Check if the current score has reached or exceeded the high score threshold
        if (ScoreManager.instance.score >= highScoreThreshold && !highScoreReached)
        {
            // Change the material to the high score material
            cauldronRenderer.material = highScoreMaterial;
            highScoreReached = true;
        }
        else if (ScoreManager.instance.score < highScoreThreshold && highScoreReached)
        {
            // If the score drops below the high score threshold, revert the material to the original
            cauldronRenderer.material = originalMaterial;
            highScoreReached = false;
        }
    }
}