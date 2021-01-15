using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour {

    float speed = 6.6f;

    Player alvo;

    Vector3 Direcao;

	// Use this for initialization
	void Start () {
        GetComponent<AudioSource>().Play();        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();             
        rb.velocity = new Vector2(0,-speed);
    }
	
	// Update is called once per frame
	void Update () {
      
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
