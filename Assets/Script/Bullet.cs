using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;

    public void Seek(Transform _target){  //Recherche de cible
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null){
            Destroy(gameObject);  //Si la balle n'a plus de cible alors on detruit la balle
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed + Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame){    //Magnitude = distance entre nous et l'ennemi
        
            HitTarget();
            return;
        }
         transform.Translate(dir.normalized * distanceThisFrame,Space.World);
    }
      void HitTarget(){
       Debug.Log("La balle à atteint sa cible");

    }
  
}
