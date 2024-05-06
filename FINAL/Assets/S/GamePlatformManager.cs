

using UnityEngine;
using TMPro;

public class GamePlatformManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public GameObject cube;
    public GameObject platformPrefab;
    public Transform armBase;
    public float timeLimit = 60f;
    public float minDistance = 3f;
    public float maxDistance = 10f;
    public float maxHeight = 5f;

    private Transform platform;
    private float timer;
    private float platformStayTimer = 0f;
    private bool isCubeOnPlatform = false;
    private bool grabbedScoreGiven = false;
    private bool gameActive = true;
    private int score = 0;
    private float initialDropHeight;

    void Start()
    {
        timer = timeLimit;
        UpdateTimerDisplay();
        GenerateAndPositionPlatform();
    }

    void Update()
    {
        if (gameActive)
        {
            timer -= Time.deltaTime;
            UpdateTimerDisplay();
            if (timer <= 0)
            {
                EndGame(false);
            }

            if (isCubeOnPlatform)
            {
                platformStayTimer += Time.deltaTime;
                if (platformStayTimer >= 3.0f)
                {
                    score += 3;
                    UpdateScoreDisplay();
                    EndGame(true);
                }
            }
            else
            {
                platformStayTimer = 0;
            }
        }
    }

    public void SetCubeOnPlatform(bool onPlatform)
    {
        isCubeOnPlatform = onPlatform;
        if (!onPlatform)
        {
            platformStayTimer = 0;  // Reset timer if cube is no longer on the platform
        }
    }

    void GenerateAndPositionPlatform()
    {
        float distance = Random.Range(minDistance, maxDistance);
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 position = armBase.position + new Vector3(
            distance * Mathf.Cos(angle),
            Random.Range(0.5f, maxHeight),
            distance * Mathf.Sin(angle)
        );

        GameObject newPlatform = Instantiate(platformPrefab, position, Quaternion.identity);
        newPlatform.tag = "Platform";
        platform = newPlatform.transform;
        Debug.Log("Platform generated at: " + platform.position);
    }

    public void CubeTouched()
    {
        if (!grabbedScoreGiven)
        {
            score += 1;
            grabbedScoreGiven = true;
            UpdateScoreDisplay();
            initialDropHeight = cube.transform.position.y;
        }
    }

    public void CubeReleased()
    {
        CheckIfCubeOnPlatform();
    }

    public void CubeHitGround(float height)
    {
        float dropHeight = initialDropHeight - height;
        if (dropHeight > 1.0f)
        {
            score -= 2;
            UpdateScoreDisplay();
            Debug.Log("Penalty applied for dropping the cube from height: " + dropHeight);
        }
    }

    private void CheckIfCubeOnPlatform()
    {
        isCubeOnPlatform = Vector3.Distance(cube.transform.position, platform.position) < 1.0f &&
                           Mathf.Abs(cube.transform.position.y - platform.position.y) < 0.5f;
    }

    private void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + score;
    }

    private void UpdateTimerDisplay()
    {
        timerText.text = "Time Left: " + Mathf.Ceil(timer).ToString() + "s";
    }

    void EndGame(bool success)
    {
        gameActive = false;
        timerText.text = success ? "Success!" : "Game Over";
    }
}
