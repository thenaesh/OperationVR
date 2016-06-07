using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class LeapMotionInput : MonoBehaviour
{
	public const int recvPort = 10000;
	private UdpClient recv;
	private IPEndPoint ep;

	void Start()
	{
		recv = new UdpClient(recvPort);
		ep = new IPEndPoint(IPAddress.Any, recvPort);
	}

	void Update()
	{
		try
		{
			byte[] recvBytes = recv.Receive(ref ep);
			modifyTransformProperties(recvBytes);
		}
		catch (Exception e)
		{
		}
		finally
		{
			recv.Close();
		}
	}

	void modifyTransformProperties(byte[] data)
	{
	}
}

