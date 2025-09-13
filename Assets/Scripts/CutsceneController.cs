using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    public Sprite sceneTwo;
    public Sprite sceneThree;

    private float currentScene = 1;

    public GameObject imageCanvas;
    private Image imageComponent;

    public TextMeshProUGUI nextButton;
    public string lastButtonTxt;
    public string sceneToLoad;

    public GameObject pageTurn;
    private AudioSource pageTurnSource;

    private void Start()
    {
        pageTurnSource = pageTurn.GetComponent<AudioSource>();
        imageComponent = imageCanvas.GetComponent<Image>();
    }

    public void NextScene()
    {
        if (currentScene == 1)
        {
            imageComponent.sprite = sceneTwo;
            pageTurnSource.Play();
        }
        else if (currentScene == 2)
        {
            imageComponent.sprite = sceneThree;
            nextButton.text = lastButtonTxt;
            pageTurnSource.Play();
        }
        else if (currentScene == 3)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
            currentScene++;
    }
}
