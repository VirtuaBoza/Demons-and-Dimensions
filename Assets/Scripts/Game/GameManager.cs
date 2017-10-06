using UnityEngine;

public class GameManager : MonoBehaviour
{

    // This is part of a singleton pattern that ensures there is only a single instance of GameManager.
    // The GameManager has a public static (there can only be one) field called "instance" which is null.
    public static GameManager instance = null;

    void Start()
    {
        if (instance != null)
        { // If the static "instance" is not actually null, then this must not be the first, so kill this object.
            Destroy(this.gameObject);
        }
        else
        { // Otherwise, assign the "instance" THIS instance, so the next time GameManager tries to start, it gets killed.
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject); // Ensures the gameobject this script is attached to is not destroyed when loading a new scene
    }
}
