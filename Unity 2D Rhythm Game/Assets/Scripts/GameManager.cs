using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	
	public GameObject scoreUI;
	private float score;
	private Text scoreText;
	
	public GameObject comboUI;
	private int combo;
	private Text comboText;
	private Animator comboAnimator;
	
	
	public enum judges { NONE = 0, BAD, GREAT, PERFECT, MISS };
	public GameObject judgeUI;
	private Sprite[] judgeSprites;
	private Image judgementSpriteRenderer;
	private Animator judgementSpriteAnimator;
	/*
	MISS : 4
	BAD : 1
	GREAT : 2
	PERFECT : 3
	*/
	public GameObject[] trails;
	private SpriteRenderer[] trailSpriteRenderers;
	
	private AudioSource audioSource;
	public string music = "1";
	
	void MusicStart()
	{
		AudioClip audioClip = Resources.Load<AudioClip>("Beats/"+music);
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = audioClip;
		audioSource.Play();
	}
	
    void Start()
    {
		Invoke("MusicStart", 2);
		judgementSpriteRenderer = judgeUI.GetComponent<Image>();
		judgementSpriteAnimator = judgeUI.GetComponent<Animator>();
		scoreText = scoreUI.GetComponent<Text>();
		comboText = comboUI.GetComponent<Text>();
		comboAnimator = comboUI.GetComponent<Animator>();
		
		judgeSprites = new Sprite[4];
		judgeSprites[0] = Resources.Load<Sprite>("Sprites/bad");
		judgeSprites[1] = Resources.Load<Sprite>("Sprites/great");
		judgeSprites[2] = Resources.Load<Sprite>("Sprites/miss");
		judgeSprites[3] = Resources.Load<Sprite>("Sprites/perfect");
		
			trailSpriteRenderers = new SpriteRenderer[trails.Length];
			for(int i = 0; i < trails.Length; i++) {
				trailSpriteRenderers[i] = trails[i].GetComponent<SpriteRenderer>();
			}
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D)) ShineTrail(0);
		if(Input.GetKey(KeyCode.F)) ShineTrail(1);
		if(Input.GetKey(KeyCode.J)) ShineTrail(2);
		if(Input.GetKey(KeyCode.K)) ShineTrail(3);
		for(int i = 0; i < trailSpriteRenderers.Length; i++) 
		{
			Color color = trailSpriteRenderers[i].color;
			color.a -= 0.015f;
			trailSpriteRenderers[i].color = color;
		}
    }
	
	public void ShineTrail(int index){
		Color color = trailSpriteRenderers[index].color;
		color.a = 0.32f;
		trailSpriteRenderers[index].color = color;
	}
	
	//judge
	void showJudgement() 
	{
		string scoreFormat = "000000";
		scoreText.text = score.ToString(scoreFormat);
		
		judgementSpriteAnimator.SetTrigger("Show");
		if( (combo % 10 == 0) && combo >= 50 )
		{
			comboText.text = "COMBO" + combo.ToString();
			comboAnimator.SetTrigger("Show");
		}
	}
	
	public void processJudge(judges judge, int noteType)
	{
		if(judge == judges.NONE) return;
		if(judge == judges.MISS)
		{
			judgementSpriteRenderer.sprite = judgeSprites[2];
			combo = 0;
		}
		else if(judge == judges.BAD)
		{
			judgementSpriteRenderer.sprite = judgeSprites[0];
			combo = 0;
			score += 50;
		}
		else
		{
			if(judge == judges.PERFECT)
			{
				judgementSpriteRenderer.sprite = judgeSprites[3];
				score+=200;
			}
			else if(judge == judges.GREAT)
			{
				judgementSpriteRenderer.sprite = judgeSprites[1];
				score += 100;
			}
			combo += 1;
			if(combo >= 50) score+=(float)combo*0.2f;
		}
		showJudgement();
	}
			
			
}
