using UnityEngine;
using TMPro;

public class DisplayPositions : MonoBehaviour
{
    public Transform cube;
    public Transform armBase;
    public Transform tcp; // The TCP (Tool Center Point) of the arm
    public TMP_Text positionDisplayText;

    void Update()
    {
        if (cube == null || armBase == null || tcp == null || positionDisplayText == null)
        {
            Debug.LogError("One or more references are not set in the DisplayPositions script.");
            return;
        }

        // Calculate local position of the cube relative to the arm base
        Vector3 cubeLocalPosition = armBase.InverseTransformPoint(cube.position);
        // Calculate local position of the TCP relative to the arm base
        Vector3 tcpLocalPosition = armBase.InverseTransformPoint(tcp.position);

        // Update the UI text to show the positions
        positionDisplayText.text = $"Cube Position (relative to arm base):\nX: {cubeLocalPosition.x:F2}\nY: {cubeLocalPosition.y:F2}\nZ: {cubeLocalPosition.z:F2}\n\n" +
                                   $"TCP Position (relative to arm base):\nX: {tcpLocalPosition.x:F2}\nY: {tcpLocalPosition.y:F2}\nZ: {tcpLocalPosition.z:F2}";
    }
}
