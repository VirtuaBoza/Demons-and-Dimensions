using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreen : MonoBehaviour {

	public float autoLoadNextLevelAfter;

	void Start(){
		if(autoLoadNextLevelAfter <= 0){
			Debug.Log("Level auto load disabled");
		} else {
			Invoke ("LoadMenu", autoLoadNextLevelAfter);
		}
	}

	void Update (){
		if (Input.anyKeyDown){
			LoadMenu();
		}
	}

	public void LoadMenu (){
		SceneManager.LoadScene("01_Menu");
	}

}
