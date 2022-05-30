using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    public float range = 15f; //Zone de detection de la tourelle

    public string enemyTag = "Enemy";

    public Transform partToRotate;

    private float turnSpeed = 7f;

    public float fireRate = 1f;
    private float fireCountdown; //Temps avant de pouvoir tirer

    public GameObject bulletPrefab;
    public Transform firePoint;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); //Permet d'appeler une fonction de manière répété. Update va appeler la fonction 60 fois par secondes, c'est trop pour ce que j'ai besoin de faire, je décide donc de l'appeler seulement 2 fois par secondes pour économiser de la ressource
    }

    void UpdateTarget(){
        GameObject[] ennemies = GameObject.FindGameObjectsWithTag(enemyTag); //On crée un array qui va contenir tout les ennemis detecté.
        float shortestDistance = Mathf.Infinity; //C'est pour eviter tout soucis : si pas d'ennemi, pas de distance donc pour eviter tout bug du à l'absence de distance on met infini
        GameObject nearestEnemy = null; //Ici sera enregistré l'ennemi le plus proche, cela va s'update avec le temps suivant le déplacement des ennemis

        foreach (GameObject enemy in ennemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); //On verifie pour chaque objet dans l'array la distance qui separe la tourelle de l'ennemi

            if(distanceToEnemy < shortestDistance){

                shortestDistance = distanceToEnemy; //Si la distance qu'on calcule est plus petite que la distance la plus faible enregistrée, alors elle devient la nouvelle distance la plus courte
                
                nearestEnemy = enemy; //Ca veut donc dire que l'ennemi qui traite actuellement dans la boucle est l'ennemi le plus proche
            }
        }

        if(nearestEnemy != null && shortestDistance <= range){ //Si il y a bien un ennemi dans le range de la tourelle

        target = nearestEnemy.transform;

        }else{
            target = null; //Si l'ennemi sort de la range alors la tourelle arrete de la regarder
        }
    }

    // Update is called once per frame
    void Update()
    {
        //rotation

        if(target == null){
            return;  //Si pas de cible alors pas de rotation
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir); //Unity utilise les Quaternion pour calculer une rotation. Sauf que le composant "Rotate" de "Transform" n'est pas un Quaternion mais un Euler angle il faut donc réaliser une conversion.

        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles; //Conversion
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        //SHOOT
        if(fireCountdown <= 0){
            Shoot();
            fireCountdown = 1 / fireRate;
        }

        fireCountdown -= Time.deltaTime;
        
    }

    void Shoot(){
       GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); //Créer une balle au moment de tirer

       Bullet bullet = bulletGO.GetComponent<Bullet>();  //Bullet en majuscule fait reference à la classe Bullet (l'autre script)

       if(bullet != null){
           bullet.Seek(target);
       }


    }

    private void OnDrawGizmosSelected(){  //Faire apparaitre visuellement la zone de détéction de la tourelle
    Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
