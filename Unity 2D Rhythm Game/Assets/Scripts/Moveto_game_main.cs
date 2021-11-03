using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Moveto_game_main : MonoBehaviour
{
    public void SceneChange(){
        SceneManager.LoadScene("GameScene");
    }
}
