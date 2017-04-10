using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingHelper : MonoBehaviour {

	public Actor actor;

	void OnMouseDown () {

		Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		target.z = actor.transform.position.z;
		Debug.Log(target);
		actor.MovePlayer(target);
	}

}
