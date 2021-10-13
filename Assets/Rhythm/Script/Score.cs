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
    score.text = scorecount.ToString ();
    if (rhythm.ScoreTrigger == true){
      scorecount = scorecount + 10;
      //Debug.Log ("score");
      rhythm.ScoreTrigger = false;
    }
}
}
