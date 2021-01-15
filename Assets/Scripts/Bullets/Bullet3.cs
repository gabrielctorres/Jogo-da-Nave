using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet3 : MonoBehaviour {

    Vector2 movimentoDir;
    float velocidade;

	// Use this for initialization
	void Start () {
        
        velocidade = 5.5f;
        
	}

    private void OnEnable()
    {
        Invoke("Destruindo", 6f);
    }
    private void Destruindo()
    {
        gameObject.SetActive(false);
    }

    public void SetarDirecaoMovimento(Vector2 dir)
    {
        movimentoDir = dir;
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(movimentoDir * velocidade * Time.deltaTime);
	}

  

}
