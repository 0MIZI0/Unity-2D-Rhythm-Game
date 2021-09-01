using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class NoteController : MonoBehaviour
{
	//노트 클래스 정의 
	class Note
	{
		public int noteType { get; set;}
		public int order { get; set;}
		public Note(int noteType, int order)
		{
			this.noteType = noteType;
			this.order = order;
			
		}
	}
	
	public GameObject[] Notes;
	
	private ObjectPooler noteObjectPooler;
	private List<Note> notes = new List<Note>();
	private float x, z, startY = 8.0f;
	
	void MakeNote(Note note)
	{
		GameObject obj = noteObjectPooler.getObject(note.noteType);
		//시작위치 결정
		x = obj.transform.position.x;
		z = obj.transform.position.z;
		obj.transform.position = new Vector3(x, startY, z);
		obj.GetComponent<NoteBehavior>().Initialize();
		obj.SetActive(true);
	}
	
	//bpm notespeed:default60(4bun),now8bun startingtime
	private string musicTitle;
	private string musicArtist;
	private int bpm;
	private int divider;
	private float startingPoint;
	private float beatCount;
	private float beatInterval;
	
	
	//코루틴 함수 ~ 유니티 엔진에게 명령을 내려용
	IEnumerator AwaitMakeNote(Note note)
	{
		int noteType = note.noteType;
		int order = note.order;
		yield return new WaitForSeconds(startingPoint + order * beatInterval);
		MakeNote(note); //1~4 -> 0~3
	}
	
    // Start is called before the first frame update
    void Start()
    {
		noteObjectPooler = gameObject.GetComponent<ObjectPooler>();
		//read txt file
		TextAsset textAsset = Resources.Load<TextAsset>("Beats/"+GameManager.instance.music);
		StringReader reader = new StringReader(textAsset.text);
		//musicTitle
		musicTitle = reader.ReadLine();
		//artist
		musicArtist = reader.ReadLine();
		string beatInformation = reader.ReadLine();
		bpm = Convert.ToInt32(beatInformation.Split(' ')[0]);
		divider = Convert.ToInt32(beatInformation.Split(' ')[1]);
		startingPoint = (float)Convert.ToDouble(beatInformation.Split(' ')[2]);
		//beatCount
		beatCount = (float)bpm / divider;
		//beatInterval
		beatInterval = 1 / beatCount;
		string line;
		while((line = reader.ReadLine()) != null)
		{
			Note note = new Note(
				Convert.ToInt32(line.Split(' ')[0]) + 1,
				Convert.ToInt32(line.Split(' ')[1])
			);
			notes.Add(note);
		}
		//노트 출발 ~
		for(int i = 0 ; i < notes.Count;i++)
		{
			StartCoroutine(AwaitMakeNote(notes[i]));
		}
		
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
