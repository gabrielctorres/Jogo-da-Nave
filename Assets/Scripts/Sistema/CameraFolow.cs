using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFolow : MonoBehaviour {
    public float cameraDistOffset = 10;
    private Camera mainCamera;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()  {
        
      
        if (player == null)
        {
            SceneManager.LoadScene("game");

        }
        else
        {
            Vector3 playerTransform = player.transform.position;
            mainCamera.transform.position = new Vector3(playerTransform.x, playerTransform.y, playerTransform.z - cameraDistOffset);
            LimitandoTela();            
        }

    }

    void LimitandoTela()
    {
        if (transform.position.x <= 0.29f || transform.position.x >= -0.29f)
        {

            //Criando Limite na posicao Minima -5.6 e na Maxima 5.6
            float xPos = Mathf.Clamp(transform.position.x, -0.29f, 0.29f);

            //Limitando a posicao da nave  com o limite criado no xPos
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);

        }
        if (transform.position.y <= 87f || transform.position.y >= -0.26f)
        {

            //Criando Limite na posicao Minima -5.6 e na Maxima 5.6
            float YPos = Mathf.Clamp(transform.position.y, -0.26f, 87f);

            //Limitando a posicao da nave  com o limite criado no xPos
            transform.position = new Vector3(transform.position.x, YPos, transform.position.z);

        }
    }
}
