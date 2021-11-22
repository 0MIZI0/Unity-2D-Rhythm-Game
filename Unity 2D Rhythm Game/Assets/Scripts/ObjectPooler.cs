using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	//Using 2-Dimension List
	
	public List<GameObject> Notes;
	private List<List<GameObject>> poolsOfNOtes;
	public int noteCount = 12;
	private bool more = true;
	
    // Start is called before the first frame update
    void Start()
    {
        poolsOfNOtes = new List<List<GameObject>>();
		for(int i = 0;i < Notes.Count;i++)//*4
		{
			poolsOfNOtes.Add(new List<GameObject>());
			for(int n = 0;n < noteCount; n++)//*15
			{
				GameObject obj = Instantiate(Notes[i]);
				obj.SetActive(false);
				poolsOfNOtes[i].Add(obj);
			}
		}
    }
	
	public GameObject getObject(int noteType)
	{
		// print($"noteType : {noteType}");
		foreach(GameObject obj in poolsOfNOtes[noteType - 1])
		{
			if(!obj.activeInHierarchy)
			{
				// print("노트 오브젝트 넘어간다");
				return obj;	
			}
		}
		if(more)
		{
			GameObject obj = Instantiate(Notes[noteType -1]);
			poolsOfNOtes[noteType -1].Add(obj);
			return obj;
		}
		// print("실패해서 NULL 넘어간다");
		return null;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
