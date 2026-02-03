using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtons : MonoBehaviour
{
    [SerializeField]
    Canvas startCanvas, settingsCanvas, creditsCanvas;

    [SerializeField]
    string sceneToGoTo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(false);
    }

    public void StartButton()
    {
        SceneManager.LoadSceneAsync(sceneToGoTo);
    }

    public void GoToSettings()
    {
        startCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
        creditsCanvas.gameObject.SetActive(false);
    }

    public void GoToCredits()
    {
        startCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(true);
    }

    public void BackToStart()
    {
        startCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(false);
    }
}
