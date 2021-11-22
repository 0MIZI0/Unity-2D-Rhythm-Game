using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class LoginBehavior : MonoBehaviour
{
    public void Login() {
        
        string PATH = System.IO.Directory.GetCurrentDirectory();
        PATH += "\\NonStop\\logininfo.txt";
        // print(PATH);

        string[] playerInfo = ReadTxt(PATH).Split('\n');

        PlayerInformation.id = playerInfo[0];
        PlayerInformation.profile = playerInfo[1];
        PlayerInformation.totScore = playerInfo[2];
        PlayerInformation.playTime = playerInfo[3];
    }

    string ReadTxt(string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        string value = "";

        if (fileInfo.Exists)
        {
            StreamReader reader = new StreamReader(filePath);
            value = reader.ReadToEnd();
            reader.Close();           
        }

        else
            value = "NonFileError";

        return value;
    }
}
