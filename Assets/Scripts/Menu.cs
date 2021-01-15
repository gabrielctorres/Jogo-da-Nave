using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour {

    public Canvas tutorial;
    public Canvas menu;

    public AudioSource btnJogar;
    public AudioSource btnTutorial;
    public AudioSource btnVoltar;

    // Use this for initialization
    private void Awake()
    {
        tutorial.enabled = false;
        menu.enabled = true;
    }	
	// Update is called once per frame
	void Update () {
		
	}

   public void Jogar()
    {
        btnJogar.Play();
        SceneManager.LoadScene("game");

    }
   public void Tutorial()
    {
        btnTutorial.Play();
        tutorial.enabled = true;
        menu.enabled = false;

    }
   public void Voltar()
    {
        btnVoltar.Play();
        tutorial.enabled = false;
        menu.enabled = true;
    }

}
