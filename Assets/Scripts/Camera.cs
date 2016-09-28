using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	private Player player;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(player.transform.position.x,player.transform.position.y,transform.position.z);
	}
}
