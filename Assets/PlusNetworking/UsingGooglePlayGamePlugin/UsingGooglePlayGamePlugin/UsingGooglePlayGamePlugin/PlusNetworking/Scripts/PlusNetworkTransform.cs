using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;


public class PlusNetworkTransform : MonoBehaviour
{
	/////////////////////////////////////////////////////////
	/// DO NOT TOUCH THIS CODE
	/// If you make changes, it is at your own risk.
	/// Made by Gamozome 30 / 9 - 2015
	/////////////////////////////////////////////////////////

	bool moveBntBool;
	private float _nextBroadcastTime = 0;
	private Vector3 _startPos;
	private Vector3 _destinationPos;
	private Quaternion _startRot;
	private Quaternion _destinationRot;
	private float _lastUpdateTime;
	//private float _timePerUpdate = 0.083f;
	private Vector3 _lastKnownVel;
	private int id;
	[Range(1.0f, 60.0f)]
	public float dataTransferRate = 10.0f;
	float rangeTransfer;
	Invitation mIncomingInvitation = null;
	 
//	Text Text1;
//	Text Text2;
//	Text Text4;
//	Text Text5;
//	Text Text6;
//	Text Text7;
//	Text Text8;
//	Text Text9;
//	Text Text10;


	Rigidbody2D rb2D;
	Rigidbody rb3D;
	bool onetime;
//	public enum pType
//	{
//		RELIABLE,
//		UNRELIABLE
//	};
//	public pType packageType;
	public bool packageTypeReliable;

	public enum tType
	{
		none,
		transform, 
		rigidbody2D, 
		rigidbody3D, 
	};
	[HideInInspector]
	public tType transformationType ;

	public enum rType
	{
		none,
		x,
		y_Topdown2D,
		z_Sideon2D,
		xY_FPS,
		yZ,
		xZ,
		xYZ_Full3D
	};
	public rType rotationType;

	void Start () 
	{
		rangeTransfer = 1.0f/dataTransferRate;
		_startPos = transform.position;
		_startRot = transform.rotation;
		if (gameObject.tag == "plusFriend" || gameObject.tag == "plusEnemy") {
			Destroy (gameObject.GetComponent<Rigidbody> ());
		} 
		else 
		{
			gameObject.GetComponent<Rigidbody>().useGravity = true;
			gameObject.GetComponent<Rigidbody>().detectCollisions  = true;	
		}
	}
	
	void Update () 
	{
		//if(gameObject.tag == "plusMyPlayer" || gameObject.tag == "plusServerPlayer")
		if(gameObject.tag == "plusMyPlayer")			
		{
			SendData();
		}

		if (moveBntBool)
		{			
			gameObject.transform.Translate(0.1f,0,0);
			gameObject. transform.Rotate(0.5f,0.5f,0.5f);
			rb3D.AddForce(Vector3.up * 20);
		}

		if(gameObject.name != PlayerPrefs.GetInt("myPlayerPos").ToString())
		{
			float pctDone = (Time.time - _lastUpdateTime) / rangeTransfer;

			if(transformationType == tType.transform)
			{
				if (pctDone <= 1.0) 
				{	
					transform.position = Vector3.Lerp (_startPos, _destinationPos, pctDone);
					transform.rotation = Quaternion.Slerp (_startRot, _destinationRot, pctDone);
				}  
			}

			else if(transformationType == tType.rigidbody2D || transformationType == tType.rigidbody3D )
			{
				if (pctDone <= 1.0) 
				{	
					transform.position = Vector3.Lerp (_startPos, _destinationPos, pctDone);
					transform.rotation = Quaternion.Slerp (_startRot, _destinationRot, pctDone);

				}  
				else
				{					
					transform.position = transform.position + (_lastKnownVel * Time.deltaTime);
				}
			}
		}			 
	}

	public void moveBtnUp()
	{
		moveBntBool = false;
	}

	public void moveBtn1()
	{
		moveBntBool = true;
	}

	public void SendData() 
	{

		if (Time.time > _nextBroadcastTime) 
		{
			string msg = "";
			int MsgID = 1;		
			int i = PlayerPrefs.GetInt("myPlayerPos");
			float x;
			float y;
			float z;
			float rx;
			float ry;
			float rz;
			float vx = 0.0f;
			float vy = 0.0f;
			float vz = 0.0f;
			x = gameObject.transform.position.x;
			y = gameObject.transform.position.y;
			z = gameObject.transform.position.z;

			if(transformationType == tType.transform)
			{
				if(rotationType == rType.x)
				{
					rx= gameObject.transform.rotation.eulerAngles.x;
					msg = MsgID.ToString() +","+ i.ToString() +","+ x.ToString() +","+ y.ToString() +","+ z.ToString() +","+ rx.ToString();
				}
				
				else if(rotationType == rType.y_Topdown2D)
				{
					ry= gameObject.transform.rotation.eulerAngles.y;
					msg = MsgID.ToString() +","+ i.ToString() +","+ x.ToString() +","+ y.ToString() +","+ z.ToString() +","+ ry.ToString();
				}
				
				else if(rotationType == rType.z_Sideon2D)
				{
					rz= gameObject.transform.rotation.eulerAngles.z;
					msg = MsgID.ToString() +","+ i.ToString() +","+ x.ToString() +","+ y.ToString() +","+ z.ToString() +","+ rz.ToString();
				}
				
				else if(rotationType == rType.xY_FPS)
				{
					rx= gameObject.transform.rotation.eulerAngles.x;
					ry= gameObject.transform.rotation.eulerAngles.y;
					msg = MsgID.ToString() +","+ i.ToString() +","+ x.ToString() +","+ y.ToString() +","+ z.ToString() +","+ rx.ToString() +","+ ry.ToString();
				}
				
				else if(rotationType == rType.yZ)
				{
					ry= gameObject.transform.rotation.eulerAngles.y;
					rz= gameObject.transform.rotation.eulerAngles.z;
					msg = MsgID.ToString() +","+ i.ToString() +","+ x.ToString() +","+ y.ToString() +","+ z.ToString() +","+ ry.ToString() +","+ rz.ToString();
				}
				
				else if(rotationType == rType.xZ)
				{
					rx= gameObject.transform.rotation.eulerAngles.x;
					rz= gameObject.transform.rotation.eulerAngles.z;
					msg = MsgID.ToString() +","+ i.ToString() +","+ x.ToString() +","+ y.ToString() +","+ z.ToString() +","+ rx.ToString() +","+ rz.ToString();
				}
				
				else if(rotationType == rType.xYZ_Full3D)
				{
					//AndroidMessage.Create("send","xyz");
					rx= gameObject.transform.rotation.eulerAngles.x;
					ry= gameObject.transform.rotation.eulerAngles.y;
					rz= gameObject.transform.rotation.eulerAngles.z;
					msg = MsgID.ToString() +","+ i.ToString() +","+ x.ToString() +","+ y.ToString() +","+ z.ToString() +","+ rx.ToString() +","+ ry.ToString() +","+ rz.ToString();
				}
			}

			else if(transformationType == tType.rigidbody2D || transformationType == tType.rigidbody3D )
			{
				if(transformationType == tType.rigidbody2D)
				{
					if(!onetime)
					{
						onetime = true;
						rb2D = GetComponent<Rigidbody2D>();
					}

					vx = rb2D.velocity.x;
					vy = rb2D.velocity.y;
					vz = 0.0f;			
				}
				if(transformationType == tType.rigidbody3D)
				{
					if(!onetime)
					{
						onetime = true;
						rb3D = GetComponent<Rigidbody>();
					}
					vx = rb3D.velocity.x;
					vy = rb3D.velocity.y;
					vz = rb3D.velocity.z;
				}

				if(rotationType == rType.x)
				{
					rx= gameObject.transform.rotation.eulerAngles.x;
					msg = MsgID.ToString() +","+ i.ToString() +","+ x.ToString() +","+ y.ToString() +","+ z.ToString() +","+ vx.ToString() +","+ vy.ToString() +","+ vz.ToString() +","+ rx.ToString();
				}
				
				else if(rotationType == rType.y_Topdown2D)
				{
					ry= gameObject.transform.rotation.eulerAngles.y;
					msg = MsgID.ToString() +","+ i.ToString() +","+ x.ToString() +","+ y.ToString() +","+ z.ToString() +","+ vx.ToString() +","+ vy.ToString() +","+ vz.ToString() +","+ ry.ToString();
				}
				
				else if(rotationType == rType.z_Sideon2D)
				{
					rz= gameObject.transform.rotation.eulerAngles.z;
					msg = MsgID.ToString() +","+ i.ToString() +","+ x.ToString() +","+ y.ToString() +","+ z.ToString() +","+ vx.ToString() +","+ vy.ToString() +","+ vz.ToString() +","+ rz.ToString();
				}
				
				else if(rotationType == rType.xY_FPS)
				{
					rx= gameObject.transform.rotation.eulerAngles.x;
					ry= gameObject.transform.rotation.eulerAngles.y;
					msg = MsgID.ToString() +","+ i.ToString() +","+ x.ToString() +","+ y.ToString() +","+ z.ToString() +","+ vx.ToString() +","+ vy.ToString() +","+ vz.ToString() +","+ rx.ToString() +","+ ry.ToString();
				}
				
				else if(rotationType == rType.yZ)
				{
					ry= gameObject.transform.rotation.eulerAngles.y;
					rz= gameObject.transform.rotation.eulerAngles.z;
					msg = MsgID.ToString() +","+ i.ToString() +","+ x.ToString() +","+ y.ToString() +","+ z.ToString() +","+ vx.ToString() +","+ vy.ToString() +","+ vz.ToString() +","+ ry.ToString() +","+ rz.ToString();
				}
				
				else if(rotationType == rType.xZ)
				{
					rx= gameObject.transform.rotation.eulerAngles.x;
					rz= gameObject.transform.rotation.eulerAngles.z;
					msg = MsgID.ToString() +","+ i.ToString() +","+ x.ToString() +","+ y.ToString() +","+ z.ToString() +","+ vx.ToString() +","+ vy.ToString() +","+ vz.ToString() +","+ rx.ToString() +","+ rz.ToString();
				}
				
				else if(rotationType == rType.xYZ_Full3D)
				{
					//AndroidMessage.Create("send","xyz");
					rx= gameObject.transform.rotation.eulerAngles.x;
					ry= gameObject.transform.rotation.eulerAngles.y;
					rz= gameObject.transform.rotation.eulerAngles.z;
					msg = MsgID.ToString() +","+ i.ToString() +","+ x.ToString() +","+ y.ToString() +","+ z.ToString() +","+ vx.ToString() +","+ vy.ToString() +","+ vz.ToString() +","+ rx.ToString() +","+ ry.ToString() +","+ rz.ToString();
				}
			}

			System.Text.UTF8Encoding  encoding = new System.Text.UTF8Encoding();
			byte[] data = encoding.GetBytes(msg);	

			if(packageTypeReliable)
			{
				PlayGamesPlatform.Instance.RealTime.SendMessageToAll(true, data);	
			
			}
			else if(!packageTypeReliable)
			{
				PlayGamesPlatform.Instance.RealTime.SendMessageToAll(false, data);	

			}
			_nextBroadcastTime = Time.time + rangeTransfer;
		}
	}

	public void MessageReceived(string data)
	{
		//Text2.text = data;
		string[] string1 = data.Split(","[0]);		
		int MsgID = int.Parse(string1[0]);	

		if (MsgID == 1)
		{
			int i = int.Parse(string1[1]);
			float x =  float.Parse(string1[2]);
			float y =  float.Parse(string1[3]);
			float z =  float.Parse(string1[4]);
			float rx= gameObject.transform.rotation.eulerAngles.x;
			float ry= gameObject.transform.rotation.eulerAngles.y;
			float rz= gameObject.transform.rotation.eulerAngles.z;
			float vx = 0.0f;
			float vy = 0.0f;
			float vz = 0.0f;
			//Text1.text = gameObject.name;
			//Text2.text = i.ToString();
			if(gameObject.name.Equals(i.ToString()))
			{
				_startPos = transform.position;
				_startRot = transform.rotation;

				if(transformationType == tType.transform)
				{
					if(rotationType == rType.x)
					{
						rx = float.Parse(string1[5]);
					}
					
					else if(rotationType == rType.y_Topdown2D)
					{
						ry = float.Parse(string1[5]);
					}
					
					else if(rotationType == rType.z_Sideon2D)
					{
						rz = float.Parse(string1[5]);
					}
					
					else if(rotationType == rType.xY_FPS)
					{
						rx = float.Parse(string1[5]);
						ry = float.Parse(string1[6]);
					}
					
					else if(rotationType == rType.yZ)
					{
						ry = float.Parse(string1[5]);
						rz = float.Parse(string1[6]);
					}
					
					else if(rotationType == rType.xZ)
					{
						rx = float.Parse(string1[5]);
						rz = float.Parse(string1[6]);
					}
					
					else if(rotationType == rType.xYZ_Full3D)
					{					
						rx = float.Parse(string1[5]);
						ry = float.Parse(string1[6]);
						rz = float.Parse(string1[7]);
					}
				}
				
				else
				{
					vx = float.Parse(string1[5]);
					vy = float.Parse(string1[6]);
					vz = float.Parse(string1[7]);
					//Text4.text = "4 transfrom rec";

					if(rotationType == rType.x)
					{
						rx = float.Parse(string1[8]);
					}
					
					else if(rotationType == rType.y_Topdown2D)
					{
						ry = float.Parse(string1[8]);
					}
					
					else if(rotationType == rType.z_Sideon2D)
					{
						rz = float.Parse(string1[8]);
					}
					
					else if(rotationType == rType.xY_FPS)
					{
						rx = float.Parse(string1[8]);
						ry = float.Parse(string1[9]);
					}
					
					else if(rotationType == rType.yZ)
					{
						ry = float.Parse(string1[8]);
						rz = float.Parse(string1[9]);
					}
					
					else if(rotationType == rType.xZ)
					{
						rx = float.Parse(string1[8]);
						rz = float.Parse(string1[9]);
					}
					
					else if(rotationType == rType.xYZ_Full3D)
					{
						rx = float.Parse(string1[8]);
						ry = float.Parse(string1[9]);
						rz = float.Parse(string1[10]);
						//Text5.text = "5 rotation rec";
					}
				}

				_lastKnownVel = new Vector3 (vx, vy, vz);
				_destinationPos = new Vector3 (x, y, z);
				_destinationRot = Quaternion.Euler (rx, ry, rz);
				_lastUpdateTime = Time.time;
				//Text6.text = "6 all data rec";
			}
		}

		else if(MsgID == 5)
		{
			int i = int.Parse(string1[1]);
			Destroy(GameObject.Find(i.ToString()));
		}
	}
}

