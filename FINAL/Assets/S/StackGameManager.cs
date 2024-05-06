using UnityEngine;
using TMPro;
using System.Text; 

public class StackGameManager : MonoBehaviour
{
    public GameObject cubePrefab;
    public Transform armBase;
    public Transform tcpTransform; // TCP Transform reference
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI positionsText;
    public int numberOfCubes = 3;
    private GameObject[] cubes;
    private int score = 0;
    private bool gameActive = true;
    private float gameStartTime;

    void Start()
    {
        SpawnCubes();
        UpdateScoreDisplay();
        gameStartTime = Time.time;
    }

    void Update()
    {
        if (gameActive)
        {
            UpdateGameTimer();
            UpdateCubePositions();
            if (CheckIfStackedCorrectly())
            {
                statusText.text = "All cubes are stacked!";
                gameActive = false;  // Stop the game timer
            }
        }
    }

    void SpawnCubes()
    {
        cubes = new GameObject[numberOfCubes];
        float minRadius = 2.0f; // Minimum distance from the arm base
        float maxRadius = 5.0f; // Maximum distance from the arm base

        for (int i = 0; i < numberOfCubes; i++)
        {
            Vector3 spawnPosition = RandomPointAroundArm(minRadius, maxRadius);
            cubes[i] = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
            cubes[i].GetComponent<Cube>().SetGameManager(this, tcpTransform);
        }
    }

    Vector3 RandomPointAroundArm(float minRadius, float maxRadius)
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float distance = Random.Range(minRadius, maxRadius);
        Vector3 spawnPosition = new Vector3(
            Mathf.Cos(angle) * distance,
            0.5f, // Keep this value if you want all cubes to spawn at the same height
            Mathf.Sin(angle) * distance
        );

        return armBase.position + spawnPosition;
    }



    public void CubeGrabbed()
    {
        score += 1;
        UpdateScoreDisplay();
    }

    public void CubeStacked()
    {
        score += 2;
        UpdateScoreDisplay();
    }

    public void StackCollapsed()
    {
        score -= 1;
        UpdateScoreDisplay();
    }

    void UpdateGameTimer()
    {
        float timeElapsed = Time.time - gameStartTime;
        timerText.text = $"Time: {timeElapsed:F2}s";
    }

    void UpdateCubePositions()
    {
        StringBuilder positions = new StringBuilder("Cube Positions:\n");
        foreach (GameObject cube in cubes)
        {
            Vector3 relativePosition = armBase.InverseTransformPoint(cube.transform.position);
            positions.AppendLine($"({relativePosition.x:F2}, {relativePosition.y:F2}, {relativePosition.z:F2})");
        }
        positionsText.text = positions.ToString();
    }

    bool CheckIfStackedCorrectly()
    {
        bool allStacked = true;
        float lastY = 0f;
        foreach (GameObject cube in cubes)
        {
            if (cube.transform.position.y < lastY + 0.9f) // Simple check for vertical alignment
            {
                allStacked = false;
                break;
            }
            lastY = cube.transform.position.y;
        }
        return allStacked;
    }

    void UpdateScoreDisplay()
    {
        scoreText.text = $"Score: {score}";
    }
}
