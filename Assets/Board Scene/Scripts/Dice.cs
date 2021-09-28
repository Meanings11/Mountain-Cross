using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour {

    private Sprite[] diceSides;
    private SpriteRenderer rend;
    public AudioSource diceAudio;
    private bool coroutineAllowed = true;

	// Use this for initialization
	private void Start () {
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
        rend.sprite = diceSides[5];

        // Initialize audio source
        diceAudio = GetComponent<AudioSource> ();
        diceAudio.Stop();
	}

    private void OnMouseDown()
    {
        if (!GameControl.gameOver && GameControl.hasFinishedReward && coroutineAllowed)
            StartCoroutine("RollTheDice");
    }

    private IEnumerator RollTheDice()
    {
        // play sound
        diceAudio.Play();

        coroutineAllowed = false;
        int randomDiceSide = 0;
        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }

        // stop sound
        diceAudio.Stop();

        GameControl.diceSideThrown = randomDiceSide + 1;
        GameControl.MovePlayer();
        coroutineAllowed = true;
    }
}
