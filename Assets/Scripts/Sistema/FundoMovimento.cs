using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FundoMovimento : MonoBehaviour {

    Material material;
    Vector2 offset;
    GameObject mainCamera;
    float velocidadeX =0, velocidadeY;

    // Use this for initialization

    private void Awake()
    {
        Player p = new Player();
        velocidadeY = p.GetVelocity();
        material = GetComponent<Renderer>().material;
        mainCamera = GameObject.Find("Camera");
    }

    void Start () {


        offset = new Vector2(velocidadeX, velocidadeY);

    }

    // Update is called once per frame
    void Update()
    {

        material.mainTextureOffset += offset * Time.deltaTime;

    }
}
