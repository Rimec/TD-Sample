using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController gameController;

    public string difficulty = "normal";

    private void Awake() {
        if (gameController == null)
        {
            gameController = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    public void SetDifficulty(string _difficulty){
        difficulty = _difficulty;
    }
}
