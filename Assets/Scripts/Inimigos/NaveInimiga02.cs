using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveInimiga02 : MonoBehaviour {


    public int vida;
    public SpriteRenderer sprite;
    public  Animator anima;
    float velocidade;  
    bool movendoPraDireita;

    int quantidadeBalas = 15;

    float primeiroAngulo = 90f, ultimoAngulo = 270f;
    Player p = new Player();

    // Use this for initialization
    void Start () { 

        velocidade = 2f;
        movendoPraDireita = true;        
    }
	
	// Update is called once per frame
	void Update () {

       // transform.Rotate(0, 0, Time.deltaTime * força);

        if (transform.position.x > 5f)
        {
            movendoPraDireita = false;
        }else if(transform.position.x < -5f)
        {
            movendoPraDireita = true;
        }

        if (movendoPraDireita)
        {
            transform.position = new Vector2(transform.position.x + velocidade * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - velocidade * Time.deltaTime, transform.position.y);
        }

    }
    void OnTriggerEnter2D(Collider2D colisor)
    {
        if (colisor.gameObject.tag == "TiroPlayer")
        {
            p.pontos += 30;
            vida = vida - (p.dano + 2);
            StartCoroutine("Piscou");
            if (vida <= 0)
            {
                GetComponent<AudioSource>().Play();
                anima.SetBool("Morreu", true);
                StartCoroutine("EsperaMorte");
            }
        }
    }

    IEnumerator EsperaMorte()
    {

        yield return new WaitForSeconds(0.36f);
        Destroy(gameObject);
        p.pontos += 1000;
    }
    IEnumerator Piscou()
    {
        sprite.color = new Color(202f, 0f, 42f, .5f);
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
    }
}
