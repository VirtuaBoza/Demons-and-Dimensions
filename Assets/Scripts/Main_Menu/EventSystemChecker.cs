using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EventSystemChecker : MonoBehaviour
{
	void OnEnable()
	{
		//Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		//Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		//If there is no EventSystem (needed for UI interactivity) present
		if(!FindObjectOfType<EventSystem>())
		{
			//The following code instantiates a new object called EventSystem
			GameObject obj = new GameObject("EventSystem");

			//And adds the required components
			obj.AddComponent<EventSystem>();
			obj.AddComponent<StandaloneInputModule>().forceModuleActive = true;
		}
	}
}
