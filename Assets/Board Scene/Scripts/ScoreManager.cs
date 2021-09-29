using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
  public Text TotalGameScore;
  int totalgamescore = 0;

  // Start is called before the first frame update
  void Start()
  {
    totalgamescore = PlayerPrefs.GetInt("totalGameScore", 0);
    TotalGameScore.text = "$" + totalgamescore.ToString();
  }

  // Update is called once per frame
  void Update()
  {
    totalgamescore = PlayerPrefs.GetInt("totalGameScore", 0);
    TotalGameScore.text = "$" + totalgamescore.ToString();
  }
}
