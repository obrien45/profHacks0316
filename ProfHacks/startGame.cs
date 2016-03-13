using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class startGame : MonoBehaviour {

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
			quitMenu.enabled = false;
			creditsCanvas.GetComponent<Canvas>().enabled = false;
			returnToMenu.enabled = false;
		}

	public void ExitPress()
		{
			quitMenu.enabled = true;
			startText.enabled = false;
			exitText.enabled = false;
		}

	public void NoPress()
		{
			quitMenu.enabled = false;
			startText.enabled = true;
			exitText.enabled = true;
		}

	public void CreditsPressed()
		{
			creditsCanvas.enabled = true;
			returnToMenu.enabled = true;
		}

	public void ReturnToMenu()
	{
			creditsCanvas.enabled = false;
			NoPress ();
	}

	// Update is called once per frame

	public void startButton() 
		{
			SceneManager.LoadScene("CalibrationLevel");
		}

	public void exitGame()
		{
			Application.Quit();
		}
}
