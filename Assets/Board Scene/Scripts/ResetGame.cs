using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetGame : MonoBehaviour
{
    public Button resetButton;
    public GameObject player;
    private PlayerMovement playerMovement;
    
    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        Button btn = resetButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        // Delete all of the PlayerPrefs settings by pressing this Button
        PlayerPrefs.DeleteAll();

        // Reset player place back to origin
        player.transform.position = playerMovement.waypoints[0].transform.position;
    }
}
