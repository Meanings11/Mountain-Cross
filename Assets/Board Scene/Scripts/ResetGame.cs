using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetGame : MonoBehaviour
{
    public Button resetButton;
    private static GameObject player;
    
    void Start()
    {
        player = GameObject.Find("Player");

        Button btn = resetButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        // Delete all of the PlayerPrefs settings by pressing this Button
        PlayerPrefs.DeleteAll();

        // Setup player movement
        // player.transform.position = new Vector2(player.GetComponent<PlayerMovement>().waypoints[0].transform.position.x,
        //     player.GetComponent<PlayerMovement>().waypoints[0].transform.position.y);
        player.GetComponent<PlayerMovement>().destinationWaypointIndex = 0;
        player.GetComponent<PlayerMovement>().moveForward = false;
        player.GetComponent<PlayerMovement>().moveAllowed = true;
    }
}
