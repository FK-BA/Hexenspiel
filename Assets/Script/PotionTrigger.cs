using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionTrigger : MonoBehaviour
{
    // Reference to the Particle System component
    public ParticleSystem myParticleSystem;

    // Use a HashSet to keep track of the objects that have triggered the collider
    private HashSet<GameObject> triggeredObjects = new HashSet<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        // Check if the other object has not triggered the collider before
        if (!triggeredObjects.Contains(other.gameObject) && other.gameObject.tag == "Trigger")
        {
            // Add the object to the HashSet
            triggeredObjects.Add(other.gameObject);

            // Increment the score by 1
            ScoreManager.instance.AddScore(1);

            // Play the Particle System
            if (myParticleSystem != null)
            {
                myParticleSystem.Play();
            }

            // Destroy the object after 0.1 second
            Destroy(gameObject, 0.1f);
        }
    }
}