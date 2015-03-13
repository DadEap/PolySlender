using UnityEngine;
using System.Collections;

public class RamassableObjet : MonoBehaviour {

	public GestionnaireObjets.Ramassable type;

	void OnTriggerEnter(Collider other){
		// vérifier que c'est le joueur qui vient d'entrer en collision avec la page
		if (other.tag == "Player") {
			// jouer le son de ramasage de page
			AudioSource bruitPage = (AudioSource) gameObject.GetComponent("AudioSource");
			bruitPage.Play();
			// faire disparaitre l'objet mais ne pas le supprimer pour permettre au son de jouer
			this.renderer.enabled=false;
			// ajouter l'objet à l'inventaire
			GestionnaireObjets.ramasserObjet(type);
			// détruire le gameobject lorsque le son a fini de jouer
			Destroy(this.gameObject, bruitPage.clip.length );
			AfficherTexte.Afficher("Vous avez ramassé l'objet "+type);
		}
	}
}
