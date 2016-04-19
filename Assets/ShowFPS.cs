using UnityEngine;
using System.Collections;

public class ShowFPS : MonoBehaviour 
{
	private GUIText gui;
	
	public float updateInterval = 1.0f;

	private float lastInterval;
	private int frames = 0;
	
	void Start ()
	{
		lastInterval = Time.realtimeSinceStartup;
		frames = 0;
		
		if (!gui)
		{
			GameObject fd = new GameObject("FPS Display", typeof(GUIText));
			fd.hideFlags = HideFlags.HideAndDontSave;
			fd.transform.position = new Vector3(0,0,0);
			gui = fd.GetComponent<GUIText>();
			gui.pixelOffset = new Vector2(10, 155);
		}
	}

	void OnDisable ()
	{
		if (gui)
			DestroyImmediate(gui.gameObject);
	}
	
	void Update () 
	{
		++frames;
		float timeNow = Time.realtimeSinceStartup;
		
		if (timeNow > lastInterval + updateInterval)
		{
			float fps = frames / (timeNow - lastInterval);
			float frametime = 1000.0f / Mathf.Max(fps, 0.00001f);
			
			gui.text = frametime.ToString("f2") + " ms / " + fps.ToString("f2") + " fps";
			
			frames = 0;
			lastInterval = timeNow;
		}
	}
}