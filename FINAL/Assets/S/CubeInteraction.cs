using UnityEngine;

public class CubeInteraction : MonoBehaviour
{
    private GamePlatformManager gameManager;
    private bool isBeingTouched = false;

    void Start()
    {
        gameManager = FindObjectOfType<GamePlatformManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finger"))
        {
            isBeingTouched = true;
            gameManager.CubeTouched();
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            gameManager.CubeHitGround(transform.position.y);
        }
        else if (collision.gameObject.CompareTag("Platform"))
        {
            gameManager.SetCubeOnPlatform(true);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finger"))
        {
            isBeingTouched = false;
            gameManager.CubeReleased();
        }
        else if (collision.gameObject.CompareTag("Platform"))
        {
            gameManager.SetCubeOnPlatform(false);
        }
    }

    public bool IsBeingTouchedByFinger()
    {
        return isBeingTouched;
    }
}
