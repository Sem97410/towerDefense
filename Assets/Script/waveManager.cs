using UnityEngine;
using System.Collections; //Permet d'utiliser les coroutines
using UnityEngine.UI; //Permet de récuperer les textes

public class waveManager : MonoBehaviour
{
    [SerializeField]
    private Transform enemyPrefab;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private float timeBetweenWaves = 5f;

    private float countdown = 2f; //C'est un compte à rebours

    [SerializeField]
    private Text waveCountDownTimer;

    private int waveIndex = 0;

    void Update()
    {
        if(countdown <= 0f){        //Si le compte à rebours = 0 alors on lance la vague d'ennemis.
        
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves; //On relance le compte à rebours (si il est à 0 on remet du temps pour que le decompte peut se faire à nouveau) 
        }

        countdown -= Time.deltaTime; //On diminue de 1 par seconde le compte à rebours

        waveCountDownTimer.text = Mathf.Round(countdown).ToString();
    }

    IEnumerator SpawnWave(){

         waveIndex++; //Defini la manche dans laquelle on est

        for (var i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

         

    }

    void SpawnEnemy(){
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
