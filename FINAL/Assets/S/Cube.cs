using UnityEngine;

public class Cube : MonoBehaviour
{
    private StackGameManager gameManager;
    private Transform tcp;  // Reference to the TCP (Tool Center Point)
    private bool isBeingGrabbed = false;
    private bool scoreGivenForThisCube = false;
    private float grabTimer = 0f;
    private const float requiredGrabTime = 2.0f;  // Cube needs to be grabbed for at least 2 seconds

    public void SetGameManager(StackGameManager manager, Transform tcpTransform)
    {
        gameManager = manager;
        tcp = tcpTransform; // Assign the TCP from the GameManager or directly via the Inspector
    }

    void Update()
    {
        if (isBeingGrabbed)
        {
            // Check if the cube is near the TCP point
            if (Vector3.Distance(transform.position, tcp.position) < 0.5f)  // Adjust the distance as needed
            {
                grabTimer += Time.deltaTime;
            }
            else
            {
                // Reset the timer if the cube is moved away from the TCP
                grabTimer = 0;
            }

            // Check if the cube has been grabbed for the required time and the score has not been given yet
            if (grabTimer >= requiredGrabTime && !scoreGivenForThisCube)
            {
                gameManager.CubeGrabbed();
                scoreGivenForThisCube = true;  // Ensure score is only given once
                isBeingGrabbed = false;  // Optionally stop checking once scored
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finger"))
        {
            isBeingGrabbed = true;  // Start the grab check
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finger"))
        {
            isBeingGrabbed = false;
            grabTimer = 0;  // Reset the grab timer if the finger releases the cube
        }
    }
}
