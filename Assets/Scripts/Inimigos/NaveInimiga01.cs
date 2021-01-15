using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveInimiga01 : MonoBehaviour {

   public int vida;

    public Transform spawnPosition;

    public GameObject buffVida;

    public SpriteRenderer sprite;
    bool andarPracima;
    bool podeAtirar;
    bool andarProLado;
    static bool spawnou;
    float VelocidadeLado = 8.0f;  
    float velocidadeCima = 5.0f;
    int EstadoAleatorio;

    public GameObject bullet; 
    Transform player;
    Player p = new Player();   
    public Animator anima;

    // Use this for initialization
    void Start () {       

        andarPracima = false;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        spawnou = true;

        EstadoAleatorio = Random.Range(1,4);

        InvokeRepeating("SpawnBullet", 0f, 0.5f);

    }
	
	// Update is called once per frame
	void Update () {

        if (EstadoAleatorio == 1)
        {
            podeAtirar = true;
            MovimentoProLado();
        }else if (EstadoAleatorio == 2)
        {
            podeAtirar = true;
            MovimentoPraCima();
            MovimentoProLado();
        }else if(EstadoAleatorio == 3)
        {
            podeAtirar = false;
            MovimentoReto();
        }


        p.pontos++;


    }

    void SpawnBullet()
    {

        if (spawnou)
        {
            if (podeAtirar)
            {
                Instantiate(bullet, spawnPosition.position, spawnPosition.rotation);
            }
            
        }

    }

    void MovimentoProLado()
    {
        if (transform.position.x > 10.7f)
        {
            andarProLado = false;
        }
        else if (transform.position.x < -12f)
        {
            andarProLado = true;
        }

        if (andarProLado)
        {
            transform.position = new Vector2(transform.position.x + VelocidadeLado * Time.deltaTime, transform.position.y );
        }
        else
        {
            transform.position = new Vector2(transform.position.x - VelocidadeLado * Time.deltaTime, transform.position.y );
        }
    }
    void MovimentoPraCima()
    {
        if (transform.position.y > 13.22f)
        {
            andarPracima = false;
        }
        else if (transform.position.y < 9.4f)
        {
            andarPracima = true;
        }

        if (andarPracima)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + velocidadeCima* Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - velocidadeCima * Time.deltaTime);
        }
    }
    void MovimentoReto()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * velocidadeCima);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TiroPlayer")
        {
            p.SomarPontos(100);
            vida = vida - p.dano;            
            StartCoroutine("Piscou");
            if (vida <= 0)
            {
                p.SomarPontos(300);
                GetComponent<AudioSource>().Play();
                anima.SetBool("Morreu", true);
                StartCoroutine("EsperaMorte");
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        
    }
    
    IEnumerator EsperaMorte()
    {
        if (EstadoAleatorio == 2)
        {
            Instantiate(buffVida, spawnPosition.position, Quaternion.identity);            
        }
        yield return new WaitForSeconds(0.9f);
        Destroy(gameObject);
    }
    IEnumerator Piscou()
    {
        sprite.color = new Color(0, 0, 0, .5f);
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
