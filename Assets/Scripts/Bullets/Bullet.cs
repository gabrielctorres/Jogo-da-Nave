using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    float speed = 15;
    public Vector3 target;
    public Rigidbody2D rb2d;
    // Use this for initialization
    void Start () {
        GetComponent<AudioSource>().Play();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        rb2d.velocity = new Vector2(0, speed);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);

    }
}
