using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveInimiga03 : MonoBehaviour
{

    public int vida;
    int quantidadeBalas = 13;
    bool movendoPraDireita;
    bool andarProLado;    
    float primeiroAngulo = 90f, ultimoAngulo = 270f;
    public float velocidade;
    public float frequencia;
    public float magnitude;    
    public SpriteRenderer sprite;
    public Animator anima;
    Player p = new Player();
    // Use this for initialization
    void Start()
    {
        movendoPraDireita = true;

        

        InvokeRepeating("Atirar", 0f, 0.7f );
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 10f)
        {
            movendoPraDireita = false;
        }
        else if (transform.position.x < -10f)
        {
            movendoPraDireita = true;
        }

        if (movendoPraDireita)
        {
            MovimentoDireita();
        }
        else
        {
            MovimentoEsquerda();
        }
    }

    void Atirar()
    {
        GetComponent<AudioSource>().Play();
        float anguloDirecao = (ultimoAngulo - primeiroAngulo) / quantidadeBalas;
        float angulo = primeiroAngulo;

        for (int i = 0; i < quantidadeBalas + 1; i++)
        {
           
            float BalaDirX = transform.position.x + Mathf.Sin((angulo * Mathf.PI) / 180f);
            float BalaDirY = transform.position.y + Mathf.Cos((angulo * Mathf.PI) / 180f);

            Vector3 balaMovimento = new Vector3(BalaDirX, BalaDirY, 0f);
            Vector2 balaDirecao = (balaMovimento - transform.position).normalized;

            GameObject bala = ListaBalas.listaBalaGerada.PegaBala();
            bala.transform.position = transform.position;
            bala.transform.rotation = transform.rotation;
            bala.SetActive(true);
            bala.GetComponent<Bullet3>().SetarDirecaoMovimento(balaDirecao);

            angulo += anguloDirecao;

        }

    }
    void MovimentoDireita()
    {
        transform.position += transform.right * Time.deltaTime * velocidade;
        transform.position = transform.position + transform.up * Mathf.Sin(Time.time * frequencia) * magnitude;
    }
    void MovimentoEsquerda()
    {
        transform.position -= transform.right * Time.deltaTime * velocidade;
        transform.position = transform.position + transform.up * Mathf.Sin(Time.time * frequencia) * magnitude;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TiroPlayer")
        {
            vida = vida - p.dano;
            p.pontos = 100;
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

    }
    IEnumerator Piscou()
    {
        sprite.color = new Color(0, 0, 0, .5f);
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
    }

}

