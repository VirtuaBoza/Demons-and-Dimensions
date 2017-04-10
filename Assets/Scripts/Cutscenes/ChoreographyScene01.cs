using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoreographyScene01 : Choreographer {

	public Actor crystal;

	public override void CueBlocking(int index) {
		switch (index) {
		case 0:
			crystal.MovePlayer(new Vector3(15.8f,-9.1f));
			break;
		case 1:
			crystal.MovePlayer(new Vector3(18f,-8f));
			break;
		default:
			break;
		}
	}
}
