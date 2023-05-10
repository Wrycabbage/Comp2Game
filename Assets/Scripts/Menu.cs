using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class Menu : MonoBehaviourPunCallbacks
{
	

	public string menuName;
	public bool open;

	public void Open()
	{
		open = true;
		gameObject.SetActive(true);
	}

	public void Close()
	{
		open = false;
		gameObject.SetActive(false);
	}

}
