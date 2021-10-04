using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoreManager : MonoBehaviour
{
  public Text TotalGameScore;
  private int totalgamescore = 0;

  // Start is called before the first frame update
  void Start()
  {
    totalgamescore = PlayerPrefs.GetInt("totalGameScore", 0);
    if (totalgamescore == 0 || totalgamescore == 00) {
      TotalGameScore.text = "$0";
    } else {
      TotalGameScore.text = "$" + string.Format("{0:0,0}", Int16.Parse(totalgamescore.ToString()));
    }
  }

  // Update is called once per frame
  void Update()
  {
    totalgamescore = PlayerPrefs.GetInt("totalGameScore", 0);
    if (totalgamescore == 0 || totalgamescore == 00) {
      TotalGameScore.text = "$0";
    } else {
      TotalGameScore.text = "$" + string.Format("{0:0,0}", Int16.Parse(totalgamescore.ToString()));
    }
  }
}
