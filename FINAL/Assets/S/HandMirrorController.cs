using UnityEngine;

public class HandMirrorController : MonoBehaviour
{
    public PincherController userControlledPincher;
    public PincherFingerController mirroredFingerA;
    public PincherFingerController mirroredFingerB;
    public bool isBlocked = false;

    void Update()
    {
        if (!isBlocked && userControlledPincher != null)
        {
            // Synchronize the grip value from the user-controlled pinchers to the mirrored pinchers' fingers
            float currentGrip = userControlledPincher.GetCurrentGrip();
            mirroredFingerA.UpdateGrip(currentGrip);
            mirroredFingerB.UpdateGrip(currentGrip);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // You may want to specify what type of collisions cause blocking
        isBlocked = true;
    }

    void OnCollisionExit(Collision collision)
    {
        // Ensure that the blockage is genuinely cleared
        isBlocked = false;
    }
}
