using UnityEngine;
using System.Collections;

public class TouchList : MonoBehaviour
{
	private Touch[] m_touches = null;
	private uint m_errors = 0;

	void Start () 
	{
		m_touches = new Touch[32];
	}
	
	void Update () 
	{
		for (int i = 0; i < Input.touchCount; i++)
		{
			// Check for illegal state changes
			//    Phase should progress as
			//        began --+--> stationary or moved --.
			//                |                          |
			//                `--------------------------+-----> ended/canceled
			//    Anything else is considered an error.
			//
			//	Getting touches as list of Touch objects within frame (Allocates temporary variables) 
			if (m_touches[Input.touches[i].fingerId].tapCount != 0)		// uninit'd touches have tapcount = 0
			{
				uint errorBit = (uint)(1 << Input.touches[i].fingerId);
				if ((m_errors & errorBit) != 0)
					continue;

				TouchPhase newPhase = Input.touches[i].phase;
				switch(m_touches[Input.touches[i].fingerId].phase)
				{
					case TouchPhase.Began:
					case TouchPhase.Stationary:
					case TouchPhase.Moved:
					{
						if (newPhase != TouchPhase.Moved &&
							newPhase != TouchPhase.Stationary &&
							newPhase != TouchPhase.Ended &&
							newPhase != TouchPhase.Canceled)
								m_errors |= errorBit;
						break;
					}
					case TouchPhase.Ended:
					case TouchPhase.Canceled:
					{
						if (newPhase != TouchPhase.Began)
							m_errors |= errorBit;
						break;
					}
				}

				if ((m_errors & errorBit) != 0)
					Debug.Log("oldPhase == " + m_touches[Input.touches[i].fingerId].phase + " ; newPhase == " + newPhase);

			}
			m_touches[Input.touches[i].fingerId] = Input.touches[i];
		}
	}

	void OnGUI()
	{
		GUI.Label(new Rect(10,Screen.height/3, 360, 22), "Complete list of touch events and phase changing check --- >");
		
		for (int i = 0; i < m_touches.Length; ++i)
		{
			Touch touch = m_touches[i];
			if (touch.fingerId != i || touch.tapCount == 0)
				continue;
		    GUI.Label(new Rect(10,Screen.height/3 + 20 + 13 * i,200,22), "#" + touch.fingerId + " = " + touch.phase);
		    GUI.Label(new Rect(120,Screen.height/3 + 20 + 13 * i,200,22), " / tapCount = " + touch.tapCount);
			
			uint errorBit = (uint)(1 << i);
   			if ((m_errors & errorBit) != 0)
			    GUI.Label(new Rect(220,10 + 13 * i,200,22), "ERROR - Incorrect phase state change = " + touch.phase);
		}
		
        if (GUI.Button (new Rect (Screen.width - 130, Screen.height/3, 120, 30), "Reset"))
            m_touches = new Touch[32];
	}
}