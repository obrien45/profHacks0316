using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    public Canvas quitMenu;
    public Canvas creditsCanvas;
    public Button creditsText;
    public Button startText;
    public Button exitText;
    public Button returnToMenu;

    void Start()
    {
        //startText = startText.GetComponent<Button> ();
        //creditsText = creditsText.GetComponent<Button> ();
        //exitText = exitText.GetComponent<Button> ();
        //quitMenu = quitMenu.GetComponent<Canvas> ();
        //creditsCanvas = creditsCanvas.GetComponent<Canvas> ();
        quitMenu.GetComponent<Canvas>().enabled = false;
        creditsCanvas.GetComponent<Canvas>().enabled = false;
        returnToMenu.enabled = false;
    }

    public void OpenExitMenu()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
    }

    public void CloseExitMenu()
    {
        quitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
    }

    public void OpenCredits()
    {
        creditsCanvas.enabled = true;
        returnToMenu.enabled = true;
    }

    public void CloseCredits()
    {
        creditsCanvas.enabled = false;
        returnToMenu.enabled = false;
    }

    // Update is called once per frame

    public void StartGame()
    {
        SceneManager.LoadScene("ASLmap");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
