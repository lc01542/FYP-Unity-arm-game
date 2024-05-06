using UnityEngine;
using TMPro;

public class DisplayRelativePosition : MonoBehaviour
{
    public Transform cube; // Assign the cube transform here through the Inspector
    public Transform armBase; // Assign the base of the robotic arm here
    public TMP_Text positionDisplayText; // Assign your TextMeshPro UI element here

    void Update()
    {
        if (cube == null || armBase == null || positionDisplayText == null)
        {
            Debug.LogError("One or more references are not set in the DisplayRelativePosition script.");
            return;
        }

        // Calculate local position of the cube relative to the arm base
        Vector3 localPosition = armBase.InverseTransformPoint(cube.position);

        // Update the UI text to show the position
        positionDisplayText.text = $"Cube Position (relative to arm base):\nX: {localPosition.x:F2}\nY: {localPosition.y:F2}\nZ: {localPosition.z:F2}";
    }
}
