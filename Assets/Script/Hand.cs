using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Hand : MonoBehaviour
{
    public SteamVR_Action_Boolean m_GrabAction = null;
    private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;
    private Interactable m_CurrentInteractable = null;
    public List<Interactable> m_ContactInteractables = new List<Interactable>();

    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }
    private void Update()
    {
        if (m_GrabAction.GetStateDown(m_Pose.inputSource))
        {
            print(m_Pose.inputSource + "Trigger Down");
            Pickup();
        }
        if (m_GrabAction.GetStateUp(m_Pose.inputSource))
        {
            print(m_Pose.inputSource + "Trigger Up");
            Drop();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
            return;
        m_ContactInteractables.Add(other.gameObject.GetComponent<Interactable>());
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
            return;
        m_ContactInteractables.Remove(other.gameObject.GetComponent<Interactable>());
    }

    public void Pickup()
    {
        // Get nearest
        m_CurrentInteractable = GetNearestInteractable();
        // Null check
        if (!m_CurrentInteractable) return;
        // Already held, check
        if (m_CurrentInteractable.m_ActiveHand)
            m_CurrentInteractable.m_ActiveHand.Drop();

        // Store the original scale before changing it
        Vector3 originalScale = m_CurrentInteractable.transform.localScale;

        // Scale down the object by half
        m_CurrentInteractable.transform.localScale = originalScale * 0.25f;

        // Calculate the position offset relative to the hand's position
        Vector3 positionOffset = m_CurrentInteractable.transform.position - transform.position;

        // Apply the scaling to the position offset
        positionOffset *= 0.25f;

        // Update the object's position
        m_CurrentInteractable.transform.position = transform.position + positionOffset;

        // Attach
        Rigidbody targetbody = m_CurrentInteractable.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targetbody;

        // Set active hand
        m_CurrentInteractable.m_ActiveHand = this;
    }
    public void Drop()
    {
        // Null check
        if (!m_CurrentInteractable) return;

        // Restore the object's original scale
        m_CurrentInteractable.transform.localScale = m_CurrentInteractable.OriginalScale;

        // Apply velocity
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = m_Pose.GetVelocity();
        targetBody.angularVelocity = m_Pose.GetAngularVelocity();

        // Detach
        m_Joint.connectedBody = null;

        // Clear
        m_CurrentInteractable.m_ActiveHand = null;
        m_CurrentInteractable = null;
    }

    private Interactable GetNearestInteractable()
    {
        Interactable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0;

        foreach (Interactable interactable in m_ContactInteractables)
        {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interactable;

            }
        }

        return nearest;
    }
}
