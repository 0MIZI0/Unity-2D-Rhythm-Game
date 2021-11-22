using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class phpConnect : MonoBehaviour
{
    public Text UploadBoolText;
    private UnityWebRequest www;

    IEnumerator enumerator;
    // Start is called before the first frame update
    void Start()
    {
        UploadBoolText.text = "Uploading..";

        enumerator = PostPlayerInfos();
        StartCoroutine(enumerator);
    }

    private IEnumerator PostPlayerInfos()
    {
        string url = "http://10.122.2.174/adduser/addresult.php";
        WWWForm form = new WWWForm();//php에 보낼 폼을 만듦
            
        //전해줄 정보 입력
        form.AddField("id", PlayerInformation.id);
        form.AddField("score",PlayerInformation.totScore);
        form.AddField("playtime", PlayerInformation.playTime);
        www = UnityWebRequest.Post(url, form);//127.0.0.1은 로컬 호스트 주소

        print(PlayerInformation.id);  
        print(PlayerInformation.totScore);   
        yield return www.SendWebRequest();
    
        if ( www.isNetworkError )
        {
            UploadBoolText.text = "Error!";
            print("error");
        }
        else//되면
        {
            UploadBoolText.text = "Upload Success!";
            print(www.downloadHandler.text);
        }
    }
}
