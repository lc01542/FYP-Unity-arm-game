using UnityEngine;

public class ArmMirrorController : MonoBehaviour
{
    public ArticulationBody[] userControlledJoints;
    public ArticulationBody[] mirroredJoints;
    public float angleTolerance = 10.0f; // Tolerance in degrees
    private bool isBlocked = false; // Properly declared at class level

    void Update()
    {
        // Check if any joint exceeds the angular tolerance
        isBlocked = CheckIfBlockedDueToAngleDifference();

        if (!isBlocked)
        {
            // Normal mirroring behavior
            MirrorJoints(userControlledJoints, mirroredJoints);
        }
        else
        {
            // Feedback loop: Update user-controlled joints to mirror the actual positions of the mirrored joints
            MirrorJoints(mirroredJoints, userControlledJoints);
        }
    }

    private bool CheckIfBlockedDueToAngleDifference()
    {
        for (int i = 0; i < userControlledJoints.Length; i++)
        {
            float angleDifference = Quaternion.Angle(userControlledJoints[i].transform.localRotation, mirroredJoints[i].transform.localRotation);
            if (angleDifference > angleTolerance)
            {
                return true; // Blocked if any joint exceeds the tolerance
            }
        }
        return false; // Not blocked if all joints are within tolerance
    }

    private void MirrorJoints(ArticulationBody[] sourceJoints, ArticulationBody[] targetJoints)
    {
        for (int i = 0; i < sourceJoints.Length; i++)
        {
            ArticulationDrive xDrive = targetJoints[i].xDrive;
            xDrive.target = sourceJoints[i].xDrive.target;
            targetJoints[i].xDrive = xDrive;

        }
    }
}
