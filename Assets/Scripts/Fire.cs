using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    int quantidadeBalas = 20;

    float primeiroAngulo = 90f, ultimoAngulo = 270f;


    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Atirar", 0f, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void Atirar()
    {
        GetComponent<AudioSource>().Play();
        float anguloDirecao = (ultimoAngulo - primeiroAngulo) / quantidadeBalas;
        float angulo = primeiroAngulo;

        for (int i = 0; i < quantidadeBalas + 1; i++)
        {

            float BalaDirX = transform.position.x + Mathf.Sin((angulo * Mathf.PI) / 90f);
            float BalaDirY = transform.position.y + Mathf.Cos((angulo * Mathf.PI) / 90f);

            Vector3 balaMovimento = new Vector3(BalaDirX, BalaDirY, 0f);
            Vector2 balaDirecao = (balaMovimento - transform.position).normalized;

            GameObject bala = ListaBalas.listaBalaGerada.PegaBala();
            bala.transform.position = transform.position;
            bala.transform.rotation = transform.rotation;
            bala.SetActive(true);
            bala.GetComponent<Bullet3>().SetarDirecaoMovimento(balaDirecao);

            angulo -= anguloDirecao;

        }
    }
}