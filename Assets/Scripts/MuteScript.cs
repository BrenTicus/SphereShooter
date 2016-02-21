using UnityEngine;
using System.Collections;

public class MuteScript : MonoBehaviour
{
	private bool muted = false;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Backslash))
		{
			muted = !muted;
			AudioListener.volume = muted ? 0.0f : 1.0f;
		}
	}
}
