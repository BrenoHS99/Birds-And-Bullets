using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool isPaused = false;
    public bool onCutscene = false;

    public GameObject pause;
    private Canvas pauseCanvas;

    public GameObject cutscene;
    private Canvas cutsceneCanvas;

    public GameObject aleTheme;
    private AudioSource aleThemeSource;

    private void Start()
    {
        if (cutscene != null)
        {
            cutsceneCanvas = cutscene.GetComponent<Canvas>();
        }
        pauseCanvas = pause.GetComponent<Canvas>();
        if (aleTheme != null)
        {
            aleThemeSource = aleTheme.GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !onCutscene)
        {
            PauseGame();
        }
    }

    public void StartCutscene()
    {
        cutsceneCanvas.enabled = true;
        onCutscene = true;
        Time.timeScale = 0f;
    }

    public void CloseCutscene()
    {
        cutsceneCanvas.enabled = false;
        onCutscene = false;
        Time.timeScale = 1f;
        aleThemeSource.Play();
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
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
