using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListaBalas : MonoBehaviour {

   public  static ListaBalas listaBalaGerada;

   public  GameObject grupoBalas;

    bool naoExisteBala = true;

    private List<GameObject> balas;
    // Use this for initialization

    private void Awake()
    {
        listaBalaGerada = this;
    }
    void Start () {
        balas = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject PegaBala()
    {
        if(balas.Count > 0)
        {
            for(int i =0; i< balas.Count; i++)
            {
                if (!balas[i].activeInHierarchy)
                {
                    return balas[i];
                }
            }
         
        }
        if (naoExisteBala)
        {
            GameObject bala = Instantiate(grupoBalas);
            bala.SetActive(false);
            balas.Add(bala);
            return bala;
        }
        return null;
    }
}
