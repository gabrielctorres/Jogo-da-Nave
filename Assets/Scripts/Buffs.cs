using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffs : MonoBehaviour {

    int força = 20;

	// Use this for initialization
	void Start () {
        transform.Translate(Vector2.up * força * Time.deltaTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
