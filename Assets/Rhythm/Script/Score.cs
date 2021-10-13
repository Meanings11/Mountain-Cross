using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

  public Text score;

  public GameObject Beat;
  public Tempo rhythm;

  public int scorecount = 0;

  // Use this for initialization
  void Start () {

    scorecount = 0;
    rhythm = Beat.GetComponent<Tempo>();

  }


  private void Update() {
    if (rhythm.ScoreUp == true){
      scorecount = scorecount + 10;
      rhythm.ScoreUp = false;
    } else if (rhythm.ScoreDown == true){    
      scorecount = scorecount - 5;
      rhythm.ScoreDown = false;
    }

    scorecount = scorecount < 0 ? 0 : scorecount;
    score.text = scorecount.ToString ();
}

}
