using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryWithDelay : MonoBehaviour
{

	public float Delay;
	
	void Start () {
		Destroy(gameObject, Delay);
	}
}
