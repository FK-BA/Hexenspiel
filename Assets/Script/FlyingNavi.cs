using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(SteamVR_Behaviour_Pose))]

public class FlyingNavi : MonoBehaviour
{
    public Rigidbody NaviBase;
    public float ThrustForce;
    public float MaxSpeed; // Maximum speed limit
    private SteamVR_Behaviour_Pose trackedObj;
    public SteamVR_Action_Boolean Button;

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_Behaviour_Pose>();
    }

    void FixedUpdate()
    {
        bool b = Button.GetState(trackedObj.inputSource);

        // add force
        if (b)
        {
            Vector3 controllerForward = trackedObj.transform.forward; 
            NaviBase.AddForce(controllerForward * ThrustForce);
            // Clamp the velocity to the maximum speed limit
            if (NaviBase.velocity.magnitude > MaxSpeed)
            {
                NaviBase.velocity = NaviBase.velocity.normalized * MaxSpeed;
            }

            NaviBase.maxAngularVelocity = 2f;
        }


    }
}