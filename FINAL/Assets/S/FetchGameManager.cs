using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Transform armHand;
    public Transform armBase; // This should be assigned to the hand of the arm in the Inspector
    public GameObject targetPrefab;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI positionText;
    public int totalTargets = 10;
    private Transform currentTarget;
    private int targetsReached = 0;
    private float startTime;
    private float totalTime;

    void Start()
    {
        SpawnTarget();
        startTime = Time.time;
    }

    void Update()
    {
        if (currentTarget != null)
        {
            UpdateUITargetPosition();
            CheckIfTargetReached();
        }
    }

    void SpawnTarget()
    {
        if (targetsReached >= totalTargets)
        {
            EndGame();
            return;
        }

        Vector3 targetPosition = RandomPointAroundArm();
        currentTarget = Instantiate(targetPrefab, targetPosition, Quaternion.identity).transform;
    }

    Vector3 RandomPointAroundArm()
    {
        float radius = 5f; // Adjust as needed for gameplay
        Vector3 randomDirection = Random.onUnitSphere;
        randomDirection.y = Mathf.Abs(randomDirection.y); // Ensure it's above the base
        return armBase.position + randomDirection * radius;
    }

    void CheckIfTargetReached()
    {
        // Adjust the distance as necessary for your specific setup
        if (Vector3.Distance(armHand.position, currentTarget.position) < 0.5f) 
        {
            targetsReached++;
            totalTime += Time.time - startTime;
            startTime = Time.time; // Reset start time for next target
            Destroy(currentTarget.gameObject);
            SpawnTarget();
        }
    }

    void UpdateUITargetPosition()
    {
        Vector3 relativePosition = armBase.InverseTransformPoint(currentTarget.position);
        positionText.text = $"Target Position: ({relativePosition.x:F2}, {relativePosition.y:F2}, {relativePosition.z:F2})";
    }

    void EndGame()
    {
        timeText.text = $"Total Time: {totalTime:F2}s";
        Debug.Log("Game Ended. Total Time: " + totalTime);
    }
}
