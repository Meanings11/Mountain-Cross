using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardBoardControl : MonoBehaviour {

    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offsetX = 4f;
    public const float offsetY = 5f;

    [SerializeField] private MainCard originalCard;
    [SerializeField] private Sprite[] images;

    private MainCard _firstRevealed;
    private MainCard _secondRevealed;

    private int correctNum = 0;
    private int trial = 0;

    // UI
    [SerializeField] private Text scoreLabel;
    public Text gamerOverText;

    // ---------------------------------
    public AudioSource audioSource;
    public AudioClip flipSound; 
    public AudioClip scoreSound; 
    public AudioClip failSound; 

    private void Start()
    {   
        audioSource = GetComponent<AudioSource>();

        // hide gameover
        gamerOverText = GameObject.Find("GamerOverText").GetComponent<Text>();
        gamerOverText.gameObject.SetActive(false);

        Vector3 startPos = originalCard.transform.position; //The position of the first card. All other cards are offset from here.

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3};
        numbers = ShuffleArray(numbers); //This is a function we will create in a minute!

        for(int i = 0; i < gridCols; i++)
        {
            for(int j = 0; j < gridRows; j++)
            {
                MainCard card;
                if(i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MainCard;
                }

                int index = j * gridCols + i;
                int id = numbers[index];
                card.ChangeSprite(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = (offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for(int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------
    public bool canReveal
    {
        get { return _secondRevealed == null; }
    }

    public void CardRevealed(MainCard card)
    {  

        //increment trial
        trial++;
        scoreLabel.text = "Trial: " + trial;
        
        // play sound 
        audioSource.PlayOneShot(flipSound);

        if(_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }

        if (correctNum == (gridCols*gridRows/2)) {
            ExitGame();
        }
    }

    private IEnumerator CheckMatch()
    {
        if(_firstRevealed.id == _secondRevealed.id)
        {
            correctNum++;
            //scoreLabel.text = "Score: " + correctNum;
            
            //sound
            audioSource.PlayOneShot(scoreSound);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();

            
            //sound
            audioSource.PlayOneShot(failSound);
        }

        _firstRevealed = null;
        _secondRevealed = null;

    }

    public void ExitGame(){
        gamerOverText.gameObject.SetActive(true);
        int finalScore = (200 - (trial-12)*10);
        gamerOverText.text = "You got $ " + finalScore + " in this game!";
        Debug.Log("final score: " +  finalScore);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scene_001");
    }

}
