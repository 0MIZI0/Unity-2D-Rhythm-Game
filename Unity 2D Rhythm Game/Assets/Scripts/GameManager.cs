using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //single tone 
	public static GameManager instance { get; set; }
	private void Awake()
	{
		if(instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);
		
	}
	
	public float noteSpeed;
	
	public enum judges { NONE = 0, BAD, GREAT, PERFECT, MISS };
	/*
	MISS : 4
	BAD : 1
	GREAT : 2
	PERFECT : 3
	*/
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
