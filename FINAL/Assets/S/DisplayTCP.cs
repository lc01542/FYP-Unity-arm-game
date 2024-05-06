using UnityEngine;
using TMPro;

public class DisplayTCP : MonoBehaviour
{
    public Transform armBase;
    public Transform tcp; // The TCP (Tool Center Point) of the arm
    public TMP_Text positionDisplayText;

    void Update()
    {
        if (armBase == null || tcp == null || positionDisplayText == null)
        {
            Debug.LogError("One or more references are not set in the DisplayPositions script.");
            return;
        }

        // Calculate local position of the TCP relative to the arm base
        Vector3 tcpLocalPosition = armBase.InverseTransformPoint(tcp.position);

        // Update the UI text to show the positions
        positionDisplayText.text = $"TCP Position (relative to arm base):\nX: {tcpLocalPosition.x:F2}\nY: {tcpLocalPosition.y:F2}\nZ: {tcpLocalPosition.z:F2}";
    }
}
