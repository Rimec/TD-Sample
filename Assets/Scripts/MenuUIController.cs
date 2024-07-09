using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviour
{
    [SerializeField] private GameObject playBt, gameDificulty;

    public void PlayBt(){
        playBt.SetActive(false);
        gameDificulty.SetActive(true);
    }
    public void SelectDifficulty(string difficulty){
        SceneManager.LoadScene("TowerDeffense");
        GameController.gameController.SetDifficulty(difficulty);
    }
    public void QuitBt(){
        Application.Quit();
    }
}
