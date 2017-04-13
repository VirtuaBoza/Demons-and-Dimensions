using UnityEngine;
using System.Collections;

public class MyUI : MonoBehaviour {

	public static MyUI instance = null;

	void Start()
	{
		if (instance != null) {
			Destroy(this.gameObject);
		} else {
			instance = this;
		}
		//Causes UI object not to be destroyed when loading a new scene. If you want it to be destroyed, destroy it manually via script.
		DontDestroyOnLoad(this.gameObject);
	}
}
