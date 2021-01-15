using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WaveSpawner : MonoBehaviour {

    public enum SpawnState { SPAWNING,WATTING, COUNTTIG};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public List<Wave> waves = new List<Wave>();
    private int nextWave = 0;

    public Text Anunciador;
  
    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.COUNTTIG;

    private GameObject player;

    void Start()
    {
        waveCountdown = timeBetweenWaves;

        player = GameObject.Find("Player");     

    } 

   void Update()
    {

        if (player == null)
        {
            SceneManager.LoadScene("game");

        }



        if (state == SpawnState.WATTING)
        {
            if (!EnemyIsAlive())
            {
               WaveCompleted();
            }
            else
            {
                return;
            }            
        }

        if (waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave ( waves[nextWave] ) );
               
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
            var a = (int)waveCountdown;
            if (nextWave == 0)
            {
                Anunciador.text = ("Iniciando Em " + a.ToString());
            }
            else
            {
                Anunciador.text = ("Proxima Wave " + a.ToString());
            }
            
        }

        

    }
    void WaveCompleted()
    {
        Debug.Log("Wave Completa");

        state = SpawnState.COUNTTIG;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Count - 1)
        {
            nextWave = 0;           
            Debug.Log("Todas as waves completas");
        }
        else
        {
            nextWave++;
        }        
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Inimigo") == null)
            {
                Debug.Log("Morreu");
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {

        //Debug.Log("Voce esta na  Wave: " + _wave.name);
       
        state = SpawnState.SPAWNING;
        Anunciador.text = ("Wave:         " + _wave.name);   
        for(int i = 0; i < _wave.count; i++)
        {            
            SpawnEnemy(_wave.enemy);            
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WATTING;

        yield break; 
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Gerando Inimigo : " + _enemy.name);
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);      
        
    }




}
