//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;
//
//public class SA_PartisipantUI1 : MonoBehaviour {
//
//
//	public GameObject avatar;
//	public Text id;
//	public Text status;
//	public Text playerId;
//	public Text playerName;
//
//	private Texture defaulttexture;
//
//	void Awake() {
//		defaulttexture = avatar.GetComponent<Renderer>().material.mainTexture;
//	}
//
//	public void SetParticipant(GP_Participant p) {
//
//		id.text = "";
//		playerId.text = "";
//		playerName.text = "";
//		status.text = GP_RTM_ParticipantStatus.STATUS_UNRESPONSIVE.ToString();
//
//		avatar.GetComponent<Renderer>().material.mainTexture = defaulttexture;
//
//
//		GooglePlayerTemplate player = GooglePlayManager.instance.GetPlayerById(p.playerId);
//		if(player != null) {
//			playerId.text = "Player Id: " + p.playerId;
//			playerName.text = "Name: " + player.name;
//
//			if(player.icon != null) {
//				avatar.GetComponent<Renderer>().material.mainTexture = player.icon;
//			}
//
//		}
//		id.text  = "ID: " +  p.id;
//		status.text = "Status: " + p.Status.ToString();
//
//
//	}
//}
