private var touch : Touch;

function OnGUI () {

	if (GUI.Button(Rect(Screen.width - 210, 10, 200, 30), "multiTouch.enabled: " + Input.multiTouchEnabled))
		if (Input.multiTouchEnabled) 
			Input.multiTouchEnabled = false;
		else
			Input.multiTouchEnabled = true;
			
	GUI.Label(Rect(10, 10, 200, 30), "Touch count: " + Input.touchCount);
		
	for (var i = 0; i < Input.touchCount; ++i) {
		touch = Input.GetTouch(i);
		GUI.Box(Rect((touch.position.x - 160), (Screen.height - touch.position.y - 95), 120, 25), " " + touch.fingerId + " " + touch.phase);
		GUI.Box(Rect((touch.position.x - 160), (Screen.height - touch.position.y - 135), 60, 25), "tapC: " + touch.tapCount);
	}
}