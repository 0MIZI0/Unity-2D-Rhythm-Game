using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class SongSelectManager : MonoBehaviour
{
    public Image musicImage;
    public Text musicTitleText;
    public Text bpmText;
    public Text autoText;

    private int musicIndex;
    private int musicCount = 0;
    ArrayList musicList = new ArrayList();

    private void UpdateSong(int musicIndex)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Stop();

        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + musicList[musicIndex]);
        StringReader stringReader = new StringReader(textAsset.text);

        musicTitleText.text = stringReader.ReadLine();

        stringReader.ReadLine();

        bpmText.text = "BPM: " + stringReader.ReadLine().Split(' ')[0];

        AudioClip audioClip = Resources.Load<AudioClip>("Beats/" + musicList[musicIndex]);
        audioSource.clip = audioClip;
        audioSource.Play();

        musicImage.sprite = Resources.Load<Sprite>("Sprites/Game_result");
        // musicImage.sprite = Resources.Load<Sprite>("Beats/" + musicList[musicIndex]);
    }

    public void Right()
    {
        musicIndex = musicIndex + 1;
        if (musicIndex > musicCount-1) musicIndex = 0;
        UpdateSong(musicIndex);
    }

    public void Left()
    {
        musicIndex = musicIndex - 1;
        if (musicIndex < 0) musicIndex = musicCount-1;
        UpdateSong(musicIndex);
    }

    void Start()
    {
        string path = System.IO.Directory.GetCurrentDirectory()+"\\Assets\\Resources\\Beats";
        DirectoryInfo di = new DirectoryInfo(path);
        FileInfo[] fi = di.GetFiles();

        for(int i=0; i<fi.Length; i++)
        {
            if (Path.GetExtension(fi[i].ToString()).Equals(".txt")){
                musicList.Add(Path.GetFileNameWithoutExtension(fi[i].ToString()));
            }
        }

        musicCount = musicList.Count;
        musicIndex = 0;
        UpdateSong(musicIndex);
    }

    public void GameStart()
    {
        PlayerInformation.selectedMusic = musicList[musicIndex].ToString();
        SceneManager.LoadScene("GameScene");
    }

    public void autoPlay(){
        if(autoText.text == "OFF"){
            GameManager.autoPerfect = true;
            autoText.text = "ON";
        }
        else if(autoText.text == "ON"){
            GameManager.autoPerfect = false;
            autoText.text = "OFF";
        }
        
    }
}
