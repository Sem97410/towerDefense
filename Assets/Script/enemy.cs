using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private int waypointIndex = 0;

    void Start() {
        target = Waypoints.points[0]; //On selectionne la cible de l'ennemi : le premier waypoint
    }

    private void Update() {
        Vector3 dir = target.position - transform.position; //Déplacement de l'ennemi, on recupere la position de la cible qu'on soustrait à la position de l'ennemi.
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World); //On deplace l'objet vers la position de "dir"
        //Normalized va faire en sorte que le vecteur ai une magnitude de 1 max. Cela permet ensuite de multiplier le vecteur normalisé par la vitesse de marche (speed) et enfin on multiplie le resultat par Time.DeltaTime pour pouvoir se déplacer indépendament de la frameRate.
        // Space.Wold est utilisé pour préciser à Unity qu'on veut déplacer notre objet dans notre scène (mais n'est pas forcement obligatoire).

        if(Vector3.Distance(transform.position, target.position) <= 0.2){ //Unity parfois à du mal a atteindre exactement une position. De ce fais on crée une condition afin de créer une petite marge d'erreur pour unity.
            GetNexWaypoints();  //Lance une fonction détruisant l'ennemi quand il arrive au dernier waypoints.
        }
    }

    private void GetNexWaypoints(){
        if(waypointIndex >= Waypoints.points.Length -1){ //Si l'index des waypoints = le nombre d'element dans l'index (-1 car dans un tableau on commence avec 0)
            Destroy(gameObject);
            return; //Le return est utilisé car unity va tellement vite que parfois, alors qu'il est entrain de détruire l'objet il va quand même lire les autres lignes de code même si l'objet est en cours de destruction.
        }

        waypointIndex++;
        target = Waypoints.points[waypointIndex]; //Definition de la cible

    }
}
