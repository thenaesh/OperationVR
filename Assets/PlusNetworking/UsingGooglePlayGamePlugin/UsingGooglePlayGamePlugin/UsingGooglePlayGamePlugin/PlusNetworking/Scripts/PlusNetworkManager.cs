/*
 * Copyright (C) 2014 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */


using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class PlusNetworkManager : MonoBehaviour,RealTimeMultiplayerListener
//public class PlusNetworkManager : MonoBehaviour
{
	public static PlusNetworkManager sInstance = null;

	private bool mAuthOnStart = true;
	private bool mSigningIn = false;
	Invitation mIncomingInvitation = null;
	private bool showingWaitingRoom = false;



	//ravi
	public static PlusNetworkManager PNInstance;
	[HideInInspector]
	public Text playerLabel;
	[HideInInspector]
	public Text gameState;
	[HideInInspector]
	public Text parisipants;
	bool plusTransBool;
	const int minPlayers = 1;
	const int maxPlayersUpto8 = 0;
	const int GameVariant = 0;
	[HideInInspector]
	public bool teamMatch;
	[HideInInspector]
	public int noOfFriend;
	[HideInInspector]
	public int noOfEnemy;
	public Transform[] spawnPosition;
	public GameObject[] spawnPrefab;
	[HideInInspector]
	public string title = "Connect";
	bool oneTime;
	string myID = "";
	int myPlayerPos1;
	GameObject myPlayerPrefab;
	PlusNetworkTransform[] myPlayerController = new PlusNetworkTransform[9];
	bool oneTime1;
	bool moveBtnOneTime;
	bool OneTime2;
	int ESOther;
	string myGame = "";
//	public Text TP;
//	public Text TP1;
	bool DrawParticipantsBool;
	[HideInInspector]
	bool processed;
	private Invitation mInvitation = null;
	private bool mShouldAutoAccept = false;

	void Awake()
	{
		sInstance = new PlusNetworkManager();
//		if(PNInstance != null)
//			GameObject.Destroy(PNInstance);
//		else
//			PNInstance = this;
//		
//		DontDestroyOnLoad(this);
	}


	void Start()
	{
	
		int n = 0;
		foreach(GameObject PrefabInGame in spawnPrefab)
		{
			PlayerPrefs.SetString("prefabInGame"+n,PrefabInGame.name);
			PlayerPrefs.SetInt("prefabLen",n);
			n++;
		}

		Social.localUser.Authenticate((bool success) => {
			if (success)
			{
				
			}
			else
			{
				Debug.Log("Auth failed!!");
			}
		});
			
	
		
		// try silent authentication
		if (mAuthOnStart)
		{
			Authorize();
		}

		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			// enables saving game progress.
			//.EnableSavedGames()
				// registers a callback to handle game invitations received while the game is not running.
				.WithInvitationDelegate(OnInvitationReceived)
				// registers a callback for turn based match notifications received while the
				// game is not running.
//				.WithMatchDelegate(googleplusSocialgraph)
				// require access to a player's Google+ social graph to sign in
				.RequireGooglePlus()
				.Build();
		
		PlayGamesPlatform.InitializeInstance(config);
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();
	}

//	void googleplusSocialgraph()
//	{
//	}

	void Update()
	{
		UpdateInvitation();
	}

	//connect Btn
	public void OnPlayClicked()
	{
		Authorize();
	}
	
	//Starts the signin process.
	void Authorize()
	{
		Social.localUser.Authenticate((bool success) => {
			if (success)
			{
				
			}
			else
			{
				Debug.Log("Auth failed!!");
			}
		});
	}

	public void OnRoomSetupProgress(float progress) {
		// show the default waiting room.
		if (!showingWaitingRoom) {
			showingWaitingRoom = true;
			PlayGamesPlatform.Instance.RealTime.ShowWaitingRoomUI();
		}
	}

	void OnGUI() {
		if (mIncomingInvitation != null) 
		{
			// show the popup
		//string who = "hello";
			string who = (mIncomingInvitation.Inviter != null && mIncomingInvitation.Inviter.DisplayName != null) ? mIncomingInvitation.Inviter.DisplayName : "Someone";
			GUI.Label(new Rect(100, 60, 100, 20), who + " is challenging you to a race!");
			if (GUI.Button(new Rect(100, 80, 100, 20), "Accept!")) {
				// user wants to accept the invitation!
				PlayGamesPlatform.Instance.RealTime.AcceptInvitation(mIncomingInvitation.InvitationId, this);
			}
			if (GUI.Button(new Rect(100, 100, 100, 20), "Decline")) {
				// user wants to decline the invitation
				PlayGamesPlatform.Instance.RealTime.DeclineInvitation(mIncomingInvitation.InvitationId);

			}
		}
	}

	// quickMatch btn
	public void OnQuickMatch()
	{
		//sInstance = new PlusNetworkManager();
		PlayGamesPlatform.Instance.RealTime.CreateQuickGame(minPlayers, maxPlayersUpto8, GameVariant, this);

		//TP.text = "quickMatch";
	}

	// invite friend btn
	public void OnInviteFriend()
	{
		//sInstance = new PlusNetworkManager();
		PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen(minPlayers, maxPlayersUpto8, GameVariant, this);
		//TP.text = "OnInviteFriend";
	}

	//Invitation inbox
	public void InboxBtn()
	{
		if (processed)
		{
			return;
		}
		processed = true;
		AcceptFromInbox();
	}
	public static void AcceptFromInbox()
	{
		//sInstance = new PlusNetworkManager();
		PlayGamesPlatform.Instance.RealTime.AcceptFromInbox(sInstance);
	}

	public void OnInvitationReceived(Invitation invitation, bool shouldAutoAccept) 
	{
		mInvitation = invitation;
		mShouldAutoAccept = shouldAutoAccept;
	}

	public void UpdateInvitation()
	{		
		// if an invitation arrived, switch to the "invitation incoming" GUI
		// or directly to the game, if the invitation came from the notification
		Invitation inv = mInvitation;
		if (inv != null)
		{
			if (mShouldAutoAccept)
			{
				// jump straight into the game, since the user already indicated
				// they want to accept the invitation!
				mInvitation = null;
				mShouldAutoAccept = false;
				AcceptInvitation(inv.InvitationId);
				OnRoomConnected(true);
			}
			else
			{
				InboxBtn();
			}
		}
	}

	public static void AcceptInvitation(string invitationId)
	{
	//	sInstance = new PlusNetworkManager();
		PlayGamesPlatform.Instance.RealTime.AcceptInvitation(invitationId, sInstance);
	}

	//after connection of player (min,max)
	public void OnRoomConnected(bool success)
	{
		if (success)
		{
			Participant myself = PlayGamesPlatform.Instance.RealTime.GetSelf();
			myID = myself.ParticipantId.ToString();
			GameObject player;
			int i = 0;		

			foreach(Participant p in PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants()) 
			{
				if(p.ParticipantId.Equals(myID))
				{
					myPlayerPrefab = spawnPrefab[PlayerPrefs.GetInt("ElementInUse")];
					player = (GameObject) Instantiate(myPlayerPrefab,spawnPosition[i].transform.position,spawnPosition[i].transform.rotation);
					myPlayerPrefab = player;
					myPlayerPos1 = i;
					PlayerPrefs.SetInt("myPlayerPos",myPlayerPos1);
					player.name = i.ToString();
					player.tag = "plusMyPlayer";
				}
				i++;
			}
			sendElement();
		}
		else
		{
			Debug.Log ("Not connected to room");
		}
	}


	public void OnLeftRoom()
	{		
		SendDestoryElement();
		PlayGamesPlatform.Instance.RealTime.LeaveRoom();
		Destroy(GameObject.Find(myPlayerPos1.ToString()));	
	}
	
	public void OnPeersConnected(string[] peers)
	{
	}
	
	public void OnParticipantLeft(Participant participant)
	{
	}
	
	public void OnPeersDisconnected(string[] peers)
	{
	}

	//send data
	public void sendElement()
	{
		string msg = "";
		int MsgID = 0;
		int i = PlayerPrefs.GetInt("myPlayerPos");
		int ES = PlayerPrefs.GetInt("ElementInUse");
		msg = MsgID.ToString() +","+ i.ToString() +","+ ES.ToString();	
		System.Text.UTF8Encoding  encoding = new System.Text.UTF8Encoding();
		byte[] data = encoding.GetBytes(msg);
		PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, data);
	}

	public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data) 
	{
		System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
		string str = enc.GetString(data);
		string[] string1 = str.Split(","[0]);

		int MsgID = int.Parse(string1[0]);
		if(MsgID == 0)
		{
			int i = int.Parse(string1[1]);
			ESOther = int.Parse(string1[2]);
			GameObject player;
			player = (GameObject) Instantiate(spawnPrefab[ESOther],spawnPosition[i].transform.position,spawnPosition[i].transform.rotation);	
			spawnPrefab[ESOther] = player;
			player.name = i.ToString();
			//TP.text = "other name "+player.name;
			if(!teamMatch)
			{
				player.tag = "enemy";
			}
			
			else
			{
				if(myPlayerPos1 < noOfFriend)
				{
					if( i < noOfFriend)
					{						
						player.tag = "plusFriend";
					}
					
					else
					{
						player.tag = "plusEnemy";
					}
				}
				
				else
				{
					if( i < noOfFriend)
					{						
						player.tag = "plusEnemy";
					}
					
					else
					{
						player.tag = "plusFriend";
					}
				}
			}
		}
		
		else if(MsgID == 6)
		{
			int i = int.Parse(string1[1]);
			string method = string1[2];
			if(i != PlayerPrefs.GetInt("myPlayerPos"))
			{
				Invoke(method, 0);
			}
		}

		int j = 0;		
		if(!OneTime2)
		{
			Invoke("findObjectDemo",0.5f);

			foreach(Participant p in PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants()) 
			{					
				myGame = j.ToString();
				myPlayerController[j] = GameObject.Find(myGame).GetComponent<PlusNetworkTransform>();
				myPlayerController[j].MessageReceived(str);
				j++;
			}
			OneTime2 = true;
		}
		else
		{
		
			foreach(Participant p in PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants()) 
			{
				myPlayerController[j].MessageReceived(str);
				j++;
			}
		}
	}

	public void SendDestoryElement()
	{		
		string msg = "";
		int MsgID = 5;
		int i = PlayerPrefs.GetInt("myPlayerPos");		
		msg = MsgID.ToString() +","+ i.ToString();
		System.Text.UTF8Encoding  encoding = new System.Text.UTF8Encoding();
		byte[] data = encoding.GetBytes(msg);
		PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, data);
		//PlayGamesPlatform.Instance.RealTime.SendMessage(true, participantId, data);
	}

	public void Disconnect()
	{
		PlayGamesPlatform.Instance.SignOut();
	}


	public void moveBtn()
	{
		int i = 0;
		
		if(!moveBtnOneTime)
		{
			foreach(Participant p in PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants()) 
			{
				if(p.ParticipantId.Equals(myID))
				{		
					myGame = i.ToString();
					myPlayerController[myPlayerPos1] = GameObject.Find(myGame).GetComponent<PlusNetworkTransform>();
					myPlayerController[myPlayerPos1].moveBtn1();
				}
				i++;
			}
			moveBtnOneTime = true;
		}
		else
		{
			myPlayerController[myPlayerPos1].moveBtn1();
		}
	}
	
	public void moveBtnUp()
	{
		myPlayerController[myPlayerPos1].moveBtnUp();
	}

	public void SendMethodName(string methodName)
	{
		string msg = "";
		int MsgID = 6;
		int i = PlayerPrefs.GetInt("myPlayerPos");		
		msg = MsgID.ToString() +","+ i.ToString() +","+ methodName;
		System.Text.UTF8Encoding  encoding = new System.Text.UTF8Encoding();
		byte[] data = encoding.GetBytes(msg);
		PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, data);
	}

	void findObjectDemo()
	{
		//this method is just to wait for few sec
	}
}


