using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HoldBroom : MonoBehaviour
{
    public GameObject BroomOnPlayer;
    private SteamVR_Action_Boolean grabAction;

    private void Awake()
    {
        // Get the SteamVR input action for the boolean button you want to use
        grabAction = SteamVR_Input.GetBooleanAction("GrabPinch");
    }

    void Start()
    {
        BroomOnPlayer.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the collider belongs to the player based on the tag
        if (other.CompareTag("Player"))
        {
            // Check if the boolean button is pressed on the Vive controller
            if (grabAction.GetStateDown(SteamVR_Input_Sources.Any))
            {
                this.gameObject.SetActive(false);
                BroomOnPlayer.SetActive(true);
            }
        }
    }
}


