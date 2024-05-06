using UnityEngine;

public class PincherController : MonoBehaviour
{
    public GameObject fingerA;
    public GameObject fingerB;
    public float gripSpeed = 3.0f;
    private PincherFingerController fingerAController;
    private PincherFingerController fingerBController;
    private float grip = 0.0f;  // 0: fully open, 1: fully closed

    [Header("Keybinds")]
    public KeyCode closeKey = KeyCode.B;  // Default key to close the pinchers
    public KeyCode openKey = KeyCode.Space;  // Default key to open the pinchers

    void Start()
    {
        fingerAController = fingerA.GetComponent<PincherFingerController>();
        fingerBController = fingerB.GetComponent<PincherFingerController>();
    }

    void Update()
    {
        HandleInput();
        UpdateFingers();
    }

    void HandleInput()
    {
        if (Input.GetKey(closeKey)) // Closing grip
        {
            grip = Mathf.Clamp(grip + gripSpeed * Time.deltaTime, 0, 1);
        }
        else if (Input.GetKey(openKey)) // Opening grip
        {
            grip = Mathf.Clamp(grip - gripSpeed * Time.deltaTime, 0, 1);
        }
    }

    void UpdateFingers()
    {
        fingerAController.UpdateGrip(grip);
        fingerBController.UpdateGrip(grip);
    }

    public float GetCurrentGrip()
    {
        return grip;
    }

    // Add this method
    public void SetGrip(float newGrip)
    {
        grip = Mathf.Clamp(newGrip, 0, 1);
        UpdateFingers();
    }
}
