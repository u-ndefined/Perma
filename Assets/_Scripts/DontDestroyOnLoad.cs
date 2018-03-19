using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {

	private static DontDestroyOnLoad instance;
	public static DontDestroyOnLoad GetSingleton
	{
		get { return instance; }
	}

	public void SetSingleton()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this);
		}            
		else if (instance != this)
			Destroy(gameObject);
	}

	private void Awake()
	{
		SetSingleton();
	}
}
