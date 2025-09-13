using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject creditsCanvas;
    private Canvas creditsCanvasComponent;

    public GameObject menuCanvas;
    private Canvas menuCanvasComponent;

    private void Start()
    {
        creditsCanvasComponent = creditsCanvas.GetComponent<Canvas>();
        menuCanvasComponent = menuCanvas.GetComponent<Canvas>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("CutsceneBegin");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ToggleCredits()
    {
        creditsCanvasComponent.enabled = !creditsCanvasComponent.enabled;
        menuCanvasComponent.enabled = !menuCanvasComponent.enabled;
    }
}
