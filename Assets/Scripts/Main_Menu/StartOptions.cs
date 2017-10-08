using UnityEngine;
using UnityEngine.SceneManagement;


public class StartOptions : MonoBehaviour {

	public int sceneToStart = 1; //Index number in build settings of scene to load

	public bool inMainMenu = true; //If true, pause button disabled in main menu (Cancel in input manager, default escape key)

	public Animator animColorFade; //Reference to animator which will fade to and from black when starting game.
	[HideInInspector] public Animator animMenuAlpha; //Reference to animator that will fade out alpha of MenuPanel canvas group
	public AnimationClip fadeColorAnimationClip; //Animation clip fading to color (black default) when changing scenes
	[HideInInspector] public AnimationClip fadeAlphaAnimationClip; //Animation clip fading out UI elements alpha
    
	private Music music;										//Reference to Music script
	private float fastFadeIn = .01f;							//Very short fade time (10 milliseconds) to start playing music immediately without a click/glitch
	private ShowPanels showPanels;								//Reference to ShowPanels script on UI GameObject, to show and hide panels


	void Awake()
	{
		//Get a reference to ShowPanels attached to UI object
		showPanels = GetComponent<ShowPanels> ();

		//Get a reference to Music attached to UI object
		music = GetComponent<Music> ();
	}


	public void StartButtonClicked()
	{
		// Fade out volume of music group of AudioMixer by calling FadeDown function of Music, using length of fadeColorAnimationClip as time. 
		//To change fade time, change length of animation "FadeToColor"
		music.FadeDown(fadeColorAnimationClip.length);

		//Start fading and change scenes halfway through animation when screen is blocked by FadeImage
		//Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
		Invoke ("LoadDelayed", fadeColorAnimationClip.length * .5f);

		//Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
		animColorFade.SetTrigger ("fade");

	}

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
		music.PlayLevelMusic ();
	}

	public void LoadDelayed()
	{
		//Pause button now works if escape is pressed since we are no longer in Main menu.
		inMainMenu = false;

		//Hide the main menu UI element
		showPanels.HideMenu ();

		//Load the selected scene, by scene index number in build settings
		SceneManager.LoadScene (sceneToStart);
	}

	public void PlayNewMusic()
	{
		//Fade up music nearly instantly without a click 
		music.FadeUp (fastFadeIn);
		//Play music clip assigned to mainMusic in Music script
		music.PlaySelectedMusic (1);
	}
}
