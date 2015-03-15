using UnityEngine;
using System.Collections;

public class RamassableObjet : MonoBehaviour {

	public GestionnaireObjets.Ramassable type;

	private string nomObjet(GestionnaireObjets.Ramassable TypeObj){
		switch (TypeObj){
			case GestionnaireObjets.Ramassable.BulletinDeNotes: return "Bulletin de notes";
			case GestionnaireObjets.Ramassable.CarteEtudiante: return "Carte étudiante";
			case GestionnaireObjets.Ramassable.CertificatDeScolarite: return "Certificat de scolarité";
			case GestionnaireObjets.Ramassable.ConventionsDeStage: return "Conventions de stage";
			case GestionnaireObjets.Ramassable.DisqueDur: return "Disque dur";
			case GestionnaireObjets.Ramassable.GameboyColor: return "Game Boy Color";
			case GestionnaireObjets.Ramassable.SacDeCours: return "Sac de cours";
			case GestionnaireObjets.Ramassable.TOEIC: return "TOEIC";
			case GestionnaireObjets.Ramassable.Ventoline: return "Ventoline";
			default: 
				throw new UnityException("Type d'objet ramassé inexistant");
				return "???";
		}
	}

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
			AfficherTexte.Afficher(nomObjet(type)+ " ramassé ("+GestionnaireObjets.nombreObjets()+"/"+GestionnaireObjets.nombreObjetsTotal()+")");
		}
	}
}
