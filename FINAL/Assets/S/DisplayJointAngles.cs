using UnityEngine;
using TMPro;

public class DisplayJointAngles : MonoBehaviour
{
    public ArticulationBody[] joints;
    public TMP_Text angleDisplayText;

    void Update()
    {
        if (angleDisplayText == null || joints == null) return;

        string anglesText = "Joint Angles:\n";
        foreach (ArticulationBody joint in joints)
        {
            if (joint == null) continue;

            float rawAngle = joint.jointPosition[0]; // Assuming jointPosition[0] is the rotation angle
            float angleInDegrees = rawAngle * Mathf.Rad2Deg; // Convert from radians to degrees if necessary

            // Debug log to inspect the raw angle
            //Debug.Log("Raw angle (radians): " + rawAngle + ", Degrees: " + angleInDegrees);

            anglesText += $"{angleInDegrees.ToString("F2")}°\n";
        }
        angleDisplayText.text = anglesText;
    }
}
