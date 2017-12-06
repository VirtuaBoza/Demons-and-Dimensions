using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StartIndexAndLength
{
	public int StartIndex { get; private set; }
	public int Length { get; private set; }

	public StartIndexAndLength(int startIndex, int length)
	{
		StartIndex = startIndex;
		Length = length;
	}
}
