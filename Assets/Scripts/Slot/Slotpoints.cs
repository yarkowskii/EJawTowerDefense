﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slotpoints : MonoBehaviour
{
	public static List<Transform> points;

	void Awake()
	{
		points = new List<Transform> { };
		for (int i = 0; i < transform.childCount; i++)
		{
			points.Add(transform.GetChild(i));
		}
	}
}