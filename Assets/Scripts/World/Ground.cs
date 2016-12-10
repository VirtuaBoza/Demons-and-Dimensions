using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {

	private Player player;

	void Start () {
		player = FindObjectOfType<Player>();
	}

	void OnMouseDown () {
		Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		target.z = player.transform.position.z;
		player.MovePlayer(target);
	}
}
