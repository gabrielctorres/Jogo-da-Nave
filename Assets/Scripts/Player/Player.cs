using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int dano = 2;
    public float timer = 5f;
    public float timer2 = 5f;
    int contadorKill;
    bool inimigoMorreu;
    protected float velocity = 6.0f;
    public bool buff = false;
    public float vida;
    private float vidaTotal = 8;
    public Text txtPontos;
    public Text txtPontosTotais;
    public int pontosTotais = 0;
    public int pontos = 0;
    public Image vidaGrafic;
    public GameObject bullet;
    public Animator graficPlayer;
    public SpriteRenderer sprite;  
    public List<Transform> spawnPoints = new List<Transform>();


    public float GetVelocity()
    {
        return velocity;
    }

    public float GetDano()
    {
        return dano;
    }


    // Use this for initialization
    void Start()
    {
        vida = vidaTotal;

    }

    // Update is called once per frame
    void Update()
    {
        txtPontos.text = pontos.ToString();
        txtPontosTotais.text = pontosTotais.ToString();
        float preenchimento = (vida / vidaTotal) / 1;
        vidaGrafic.fillAmount = preenchimento;
        LimitandoTela();
        Movimentacao();


        if (timer > 0.0f && buff)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            buff = false;
            timer = 5f;
        }

    }


    void LimitandoTela()
    {
        if (transform.position.x <= 12f || transform.position.x >= -11.9f)
        {

            //Criando Limite na posicao Minima -5.6 e na Maxima 5.6
            float xPos = Mathf.Clamp(transform.position.x, -11.9f, 12f);

            //Limitando a posicao da nave  com o limite criado no xPos
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);

        }
        if (transform.position.y <= 3f || transform.position.y >= -13.4f)
        {

            //Criando Limite na posicao Minima -5.6 e na Maxima 5.6
            float YPos = Mathf.Clamp(transform.position.y, -13.4f, 3f);

            //Limitando a posicao da nave  com o limite criado no xPos
            transform.position = new Vector3(transform.position.x, YPos, transform.position.z);

        }
    }
    void Movimentacao()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * Time.deltaTime * GetVelocity());
            graficPlayer.SetBool("Voando", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * Time.deltaTime * GetVelocity());
            graficPlayer.SetBool("Voando", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Time.deltaTime * GetVelocity());
            graficPlayer.SetBool("Voando", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * Time.deltaTime * GetVelocity());
        }
        else
        {
            graficPlayer.SetBool("Voando", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocity = 8.0f;
        }
        else
        {
            velocity = 6.0f;
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (!buff)
            {
                
                Instantiate(bullet, spawnPoints[0].position, Quaternion.identity);
            }
            else
            {
                
                GetComponent<AudioSource>().Play();
                Instantiate(bullet, spawnPoints[0].position, Quaternion.identity);
                Instantiate(bullet, spawnPoints[1].position, Quaternion.identity);
                Instantiate(bullet, spawnPoints[2].position, Quaternion.identity);
            }


        }
    }
    void OnTriggerEnter2D(Collider2D colisao)
    {
        if (colisao.gameObject.tag == "TiroInimigo01")
        {
            vida -= 2f;
            Destroy(colisao.gameObject);
            StartCoroutine("Piscou");
            if (vida <= 0f)
            {

                graficPlayer.SetBool("Morreu", true);
                StartCoroutine("EsperaMorte");
            }
        }
        if (colisao.gameObject.tag == "TiroInimigo02")
        {
            vida -= 1f;
            StartCoroutine("Piscou");
            if (vida <= 0f)
            {

                graficPlayer.SetBool("Morreu", true);
                StartCoroutine("EsperaMorte");
            }
        }
        if (colisao.gameObject.tag == "TiroInimigo03")
        {
           vida -= 1f;
           StartCoroutine("Piscou");
          if (vida <= 0f)
          {

            graficPlayer.SetBool("Morreu", true);
            StartCoroutine("EsperaMorte");
          }
        }
        if (colisao.gameObject.tag == "Inimigo")
        {
          vida -= 2f;     
          StartCoroutine("Piscou");
            if (vida <= 0f)
            {

                graficPlayer.SetBool("Morreu", true);
                StartCoroutine("EsperaMorte");
            }
        }
        if (colisao.gameObject.tag == "BuffVida")
        {
          vida = vidaTotal;
          Destroy(colisao.gameObject);
        }       


    }
    IEnumerator EsperaMorte()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.9f);
        Destroy(gameObject);
        pontosTotais += pontos;
        pontos = 0;
    }
    IEnumerator Piscou()
    {
        sprite.color = new Color(0, 0, 0, .5f);
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
    }


    public int SomarPontos(int valor)
    {
        return pontos += valor;
    }

}
