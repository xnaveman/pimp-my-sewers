using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel_options;
	[SerializeField] private GameObject panel_credits;

	[SerializeField] private GameObject panel_controls;
	[SerializeField] private GameObject panel_display;
	[SerializeField] private GameObject panel_sound;
	[SerializeField] private GameObject panel_language;


	
	void Start(){
		UnshowOptions();
	}
	
	public void PlayGame(){
		SceneManager.LoadScene("Level1");
	}
	
	public void ShowOptions(){
		panel_options.SetActive(true);
	}
	
	public void UnshowOptions(){
		panel_options.SetActive(false);
	}

	public void ShowCredits(){
		panel_credits.SetActive(true);
	}

	public void UnshowCredits(){
		panel_credits.SetActive(false);
	}

	public void ShowControls(){
		panel_controls.SetActive(true);
	}

	public void UnshowControls(){
		panel_controls.SetActive(false);
	}

	public void ShowDisplay(){
		panel_display.SetActive(true);
	}

	public void UnshowDisplay(){
		panel_display.SetActive(false);
	}

	public void ShowSound(){
		panel_sound.SetActive(true);
	}

	public void UnshowSound(){
		panel_sound.SetActive(false);
	}

	public void ShowLanguage(){
		panel_language.SetActive(true);
	}

	public void UnshowLanguage(){
		panel_language.SetActive(false);
	}
	
	public void QuitGame(){
		Application.Quit();
	}
}
