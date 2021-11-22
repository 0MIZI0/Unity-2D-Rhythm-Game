using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResultManager : MonoBehaviour
{
    public Text musicTitleText;
    public Text scoreText;
    public Text maxComboText;
    public Text idText;
    public Image RankImage;

    void Start()
    {
        
        idText.text = PlayerInformation.id;
        musicTitleText.text = PlayerInformation.musicTitle.ToString();
        scoreText.text = PlayerInformation.score.ToString();
        maxComboText.text = PlayerInformation.maxCombo.ToString();

        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + PlayerInformation.selectedMusic);
        StringReader reader = new StringReader(textAsset.text);

        reader.ReadLine();
        reader.ReadLine();

        string beatInformation = reader.ReadLine();
        double scoreS = PlayerInformation.noteCount * 200 * 0.9; 
        double scoreA = PlayerInformation.noteCount * 200 * 0.8;
        double scoreB = PlayerInformation.noteCount * 200 * 0.7;

        if (PlayerInformation.score >= scoreS)
        {
            RankImage.sprite = Resources.Load<Sprite>("Sprites/Rank S");
        }else if (PlayerInformation.score >= scoreA)
        {
            RankImage.sprite = Resources.Load<Sprite>("Sprites/Rank A");
        }
        else if (PlayerInformation.score >= scoreB)
        {
            RankImage.sprite = Resources.Load<Sprite>("Sprites/Rank B");
        }
        else
        {
            RankImage.sprite = Resources.Load<Sprite>("Sprites/Rank C");
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene("SongSelectScene");
    }
}
