using UnityEngine;
using System.Collections;
using System.Collections.Generic; // pour le type List<T>

/// <summary>
/// Cette classe sert à gérer l'inventaire du joueur.
/// Elle possède des membres statiques, par mesure de simplicité du code dans les autres scripts
/// on peut donc directement y accéder depuis n'importe quel autre script sur la scène du jeu.
/// </summary>

public class GestionnaireObjets : MonoBehaviour {

	public enum Ramassable {
		CarteEtudiant,
		TOEIC,
		Conventions,
		Certificat,
		Bulletin,
		Ventoline
	}

	public List<GameObject> ramassables; // affecté dans l'éditeur
	public List<AudioClip> ambiances; // affecté dans l'éditeur

	private static List<Ramassable> listeObjets; // liste des objets ramassés

	private static GestionnaireObjets instance; // référence à soi meme, singleton


	// Cette fonction est appelée lorsque la scène principale est chargée. On initialise l'inventaire.
	void Start () {
		// singleton
		instance = this;
		// on commence avec aucun objet
		listeObjets = new List<Ramassable> ();
		// on positionne aléatoirement les objets à ramasser dans le niveau
		placerObjets ();
	}

	private void placerObjets(){
		List<GameObject> spawnPoints = new List<GameObject>();
		// récupérer les emplacements de spawn possibles
		// il s'agit des gameobjects qu'on aura placés avec le tag ObjectSpawn
		GameObject[] spawnPointsArray = GameObject.FindGameObjectsWithTag ("ObjectSpawn");
		if (spawnPointsArray.Length < ramassables.Count) {
			throw new UnityException("Il n'y a pas assez de spawnPoints pour tous les objets à ramasser.");
		}
		for (int i=0; i<spawnPointsArray.Length; i++){
			spawnPoints.Add(spawnPointsArray[i]);
		}
		spawnPointsArray = null;
		// placer les objets à des points aléatoires :
		for (int i=0; i<ramassables.Count; i++){
			// tirer un emplacement au hasard
			GameObject position = spawnPoints[Random.Range (0, spawnPoints.Count)]; // 0 <= range < count
			// placer l'objet
			Instantiate (
				ramassables[i], // instancier le prefab correspondant
				position.transform.position, // à la position du spawnpoint
				position.transform.rotation // rotation définie par le spawnpoint
			);
			// supprimer le spawn de la liste pour ne pas le réutiliser
			spawnPoints.Remove(position);
		}
	}


	// retourne true si on a déjà ramassé l'objet, false sinon
	public static bool possedeObjet(Ramassable objet){
		return listeObjets.Contains (objet);
	}

	// ajoute l'objet passé en paramètre dans la liste des objets ramassés
	// lève une exception si on a déjà ramassé cet objet
	// chaque objet ne doit etre ramassé qu'une seule fois
	public static void ramasserObjet(Ramassable objet){
		if (listeObjets.Contains (objet)) {
			//UnityException ex = new UnityException ("Un objet vient d'etre ramassé plusieurs fois. Chaque objet ne doit etre ramassé qu'une seule fois.");
			//throw ex;
			Debug.Log("L'inventaire contient déjà cet objet.");
		} else {
			// ajouter l'obj à l'inventaire
			listeObjets.Add (objet);
			//Debug.Log ("L'objet " + objet.ToString () + " a été ramassé.");
			// augmenter la difficulté
			GameObject slender = GameObject.Find("slender");
			((SlenderDeplacement) slender.GetComponent("SlenderDeplacement")).radius -= 15;
			((SphereCollider) slender.GetComponent("SphereCollider")).radius += 5;
			((SlenderDeplacement) slender.GetComponent("SlenderDeplacement")).timeWarping -= 10;
			// changer l'ambiance sonore
			AudioSource ambiance = (AudioSource) instance.gameObject.GetComponent("AudioSource");
			if (ambiance.clip != instance.ambiances[(listeObjets.Count-1)/2]){
				ambiance.clip = instance.ambiances[(listeObjets.Count-1)/2];
				ambiance.Play();
			}
		}
	}

	public static int nombreObjets(){
		return listeObjets.Count;
	}
	

}
