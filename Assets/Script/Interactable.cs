using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public Hand m_ActiveHand = null;

    // Store the original scale when the object is first initialized
    private Vector3 m_OriginalScale;

    private void Start()
    {
        m_OriginalScale = transform.localScale;
    }

    // You can use this getter to access the original scale from other scripts
    public Vector3 OriginalScale
    {
        get { return m_OriginalScale; }
    }
}
