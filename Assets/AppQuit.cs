using UnityEngine;
using System.Collections;

public class AppQuit : MonoBehaviour 
{
	private bool isDlgShown = false;
	private int dlgResult;
	private string[] dlgOptions = new string[]{"No", "Yes"};
	private Rect dlgWndRect;

	void Start()
	{
		dlgWndRect = new Rect(Screen.width/2 - 100, Screen.height/2 - 40, 200, 80);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			isDlgShown = true;
			dlgResult = -1;
		}
	}
	
	void OnGUI()
	{
		if (GUI.Button (new Rect (Screen.width - 130, Screen.height - 130, 120, 30), "Quit"))
		{
			isDlgShown = true;
			dlgResult = -1;
		}
		if (isDlgShown)	
			GUI.Window(0, dlgWndRect, QuitDlg, "Really want to quit?");
	}
	
	void QuitDlg(int windowID)
	{
		dlgResult = GUI.SelectionGrid (new Rect (20, 30, 160, 40), dlgResult, dlgOptions, 2);
		switch (dlgResult)
		{
			case 0:
				isDlgShown = false;
				break;
			case 1:
				Application.Quit();
				break;
		}
	}
}