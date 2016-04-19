using UnityEngine;
using System.Collections;
using System;

public class SetFrameRate : MonoBehaviour 
{
	public int curFrameRate = 30;
	private int fpsSelected = 7;
	private string[] fpsVariants = new string[]{"5", "10", "12", "15", "20", "25", "30", "60"};
	private bool isFPSChangeAllowed = false;
	
	void Awake () 
	{
		Application.targetFrameRate = curFrameRate;
	}
	
	void OnGUI()
	{
		isFPSChangeAllowed = GUI.Toggle(new Rect(150, Screen.height - 157, 230, 50), isFPSChangeAllowed, " Allow to change FPS (default: 60)");
		
		if (isFPSChangeAllowed)
		{
			fpsSelected = GUI.SelectionGrid (new Rect (10, Screen.height - 60, Screen.width - 120, 30), fpsSelected, fpsVariants, 8);
			if (curFrameRate != System.Convert.ToInt32(fpsVariants[fpsSelected]))
			{
				curFrameRate = System.Convert.ToInt32(fpsVariants[fpsSelected]);
				Application.targetFrameRate = curFrameRate;
				Debug.Log ("frameRate changed to " + curFrameRate + " fps");
			}
			
		}
	}
}
