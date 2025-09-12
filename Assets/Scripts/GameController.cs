using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool isPaused = false;

    public GameObject pause;
    private Canvas pauseCanvas;
    private void Start()
    {
        pauseCanvas = pause.GetComponent<Canvas>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        pauseCanvas.enabled = !pauseCanvas.enabled;
        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
