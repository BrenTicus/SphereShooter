using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour {	
	void Update () {
		if(Input.GetAxis("Enter") > 0)
		{
			Application.LoadLevel(1);
		}
	}
}
