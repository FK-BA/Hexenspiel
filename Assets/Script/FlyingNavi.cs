using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(SteamVR_Behaviour_Pose))]

public class FlyingNavi : MonoBehaviour
{
    public Rigidbody NaviBase;
    public float ThrustForce;
    public float MaxSpeed; // Maximum speed limit
    private SteamVR_Behaviour_Pose trackedObj;
    public SteamVR_Action_Vector2 TouchpadAxis; // Use the touchpad for input


    private bool isSlowingDown = false;

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_Behaviour_Pose>();
    }

    void FixedUpdate()
    {
        // Get the touchpad axis
        Vector2 touchpadAxis = TouchpadAxis.GetAxis(trackedObj.inputSource);

        // Calculate the thrust force based on touchpad position
        float thrustMultiplier = Mathf.Lerp(0f, 1f, (touchpadAxis.y + 1) / 2); // Adjust these values as needed

        if (touchpadAxis.y < 0)
        {
            isSlowingDown = true;
        }
        else
        {
            isSlowingDown = false;
        }

        // Add force only if the touchpad is being touched and not slowing down
        if (touchpadAxis != Vector2.zero && !isSlowingDown)
        {
            Vector3 controllerForward = trackedObj.transform.forward;
            NaviBase.AddForce(controllerForward * ThrustForce * thrustMultiplier);
        }

        // Apply drag to slow down if slowing down
        if (isSlowingDown)
        {
            NaviBase.drag = 1.7f;
        }
        else
        {
            NaviBase.drag = 0f;
        }

        // Clamp the velocity to the maximum speed limit
        if (NaviBase.velocity.magnitude > MaxSpeed)
        {
            NaviBase.velocity = NaviBase.velocity.normalized * MaxSpeed;
        }

        NaviBase.maxAngularVelocity = 2f;
    }
}