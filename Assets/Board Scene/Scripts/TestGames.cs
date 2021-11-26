using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestGames : MonoBehaviour
{
    public Button mole;
    public Button corgi;
    public Button fruit;
    public Button parachute;
    public Button rhythm;
    public Button mosquito;
    public Button card;
    public Button crossing;
    public Button store;

    public GameObject shop;

    // Start is called before the first frame update
    void Start() {
        Button moleBtn = mole.GetComponent<Button>();
        Button corgiBtn = corgi.GetComponent<Button>();
        Button fruitBtn = fruit.GetComponent<Button>();
        Button parachuteBtn = parachute.GetComponent<Button>();
        Button rhythmBtn = rhythm.GetComponent<Button>();
        Button mosquitoBtn = mosquito.GetComponent<Button>();
        Button cardBtn = card.GetComponent<Button>();
        Button crossingBtn = crossing.GetComponent<Button>();
        Button storeBtn = store.GetComponent<Button>();

		moleBtn.onClick.AddListener(TaskOnMole);
        corgiBtn.onClick.AddListener(TaskOnCorgi);
        fruitBtn.onClick.AddListener(TaskOnFruit);
        parachuteBtn.onClick.AddListener(TaskOnParachute);
        rhythmBtn.onClick.AddListener(TaskOnRhythm);
        mosquitoBtn.onClick.AddListener(TaskOnMosquito);
        cardBtn.onClick.AddListener(TaskOnCard);
        crossingBtn.onClick.AddListener(TaskOnCrossing);
        storeBtn.onClick.AddListener(TaskOnStore);
    }

    // Update is called once per frame
    void Update() {
    }

    void TaskOnMole() {
        SceneManager.LoadScene("Whack-A-Mole");
    }

    void TaskOnCorgi() {
        SceneManager.LoadScene("CorgiScene");
    }

    void TaskOnFruit() {
        SceneManager.LoadScene("FruitScene");
    }

    void TaskOnParachute() {
        SceneManager.LoadScene("ParachuteScene");
    }

    void TaskOnRhythm() {
        SceneManager.LoadScene("RhythmScene");
    }

    void TaskOnMosquito() {
        SceneManager.LoadScene("MosquitoScene");
    }

    void TaskOnCard() {
        SceneManager.LoadScene("CardScene");
    }

    void TaskOnCrossing() {
        SceneManager.LoadScene("CrossingScene");
    }

    void TaskOnStore() {
        shop.gameObject.SetActive(true);
        GameControl.isInStore = true;

        // hide test buttons
        GameObject.Find("TestGames").SetActive(false);
    }
}
