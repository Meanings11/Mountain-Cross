using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Button planeButton;
    public Button stepNumButton;

    public static ItemControl instance;

    public bool isChooseSteps = false;

    public bool isShowingStepBtn;
    public bool isClosingStepBtn;


    private void Awake() {
        if (instance == null) {
            instance = this;
        }    
        else if (instance != this) {
            Destroy(gameObject);
        }    
    }

    void Start()
    {
        // planeButton = GameObject.Find("PlaneButton").GetComponent<Button>();
        // stepNumButton = GameObject.Find("StepNumButton").GetComponent<Button>();
        planeButton = planeButton.GetComponent<Button>();
        stepNumButton = stepNumButton.GetComponent<Button>();

        stepNumButton.gameObject.SetActive(false);
    }

    // void Update() {
        // if (isShowingStepBtn) {
        //     Vector2 currentPos = stepNumButton.gameObject.transform.localPosition;
		//     Vector2 endPos = new Vector2 (150, currentPos.y);
		//     stepNumButton.gameObject.transform.localPosition = endPos;
        //     isShowingStepBtn = false;
        // } else if (isClosingStepBtn) {
        //     Vector2 currentPos = stepNumButton.gameObject.transform.localPosition;
		//     Vector2 endPos = new Vector2 (64, currentPos.y);
		//     stepNumButton.gameObject.transform.localPosition = endPos;
        //     isCloStepBtn = false;
        // }
    // }

    public void refreshCurrentItems() 
    {
        int ticketNum = PlayerStats.getItemNum(PlayerStats.plantTicket); // get plane ticket count

        if (ticketNum > 0) planeButton.gameObject.SetActive(true);
        else planeButton.gameObject.SetActive(false);
    }

    public void toggleStepButton() {
        if (!isChooseSteps) {
            
            stepNumButton.gameObject.SetActive(true);

            Vector2 currentPos = stepNumButton.gameObject.transform.localPosition;
		    Vector2 endPos = new Vector2 (150, currentPos.y);
		    stepNumButton.gameObject.transform.localPosition = endPos;     

            isChooseSteps = true;
        } else {
            
            // Remove step num button
            Vector2 currentPos = stepNumButton.gameObject.transform.localPosition;
		    Vector2 endPos = new Vector2 (64, currentPos.y);
		    stepNumButton.gameObject.transform.localPosition = endPos;
            stepNumButton.gameObject.SetActive(false);

            // Move player and decrease item
            GameControl.MovePlayer(StepNumButton.num);
            PlayerStats.useOneItem(PlayerStats.plantTicket);
            refreshCurrentItems();
            isChooseSteps = false;
        }
    }
}
