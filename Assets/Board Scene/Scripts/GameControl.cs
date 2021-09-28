using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {

    private static GameObject winsTextShadow, playerMoveCount;

    private static GameObject player;

    private static GameObject sceneManager;

    public static int diceSideThrown = 0;

    public static bool gameOver = false;

    public static bool hasFinishedReward = true;

    HashSet<int> minigamesIndexes = new HashSet<int>();
    private int[] waypoints_reward = {0, 0, 4, 0, 0, 0, 0,
                                      0, 0, 0, -10, 0, 0,
                                      0, 0, 0, -1, 0, -2,
                                      0, 0, 0, 0, 0, 0};
    // Use this for initialization
    void Start () {
        // Only play the board with landscape mode
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        minigamesIndexes.Add(3);
        minigamesIndexes.Add(4);
        minigamesIndexes.Add(5);
        minigamesIndexes.Add(6);
        minigamesIndexes.Add(9);
        minigamesIndexes.Add(10);
        minigamesIndexes.Add(12);
        minigamesIndexes.Add(13);
        minigamesIndexes.Add(15);
        minigamesIndexes.Add(16);
        minigamesIndexes.Add(19);
        minigamesIndexes.Add(21);
        minigamesIndexes.Add(23);

        winsTextShadow = GameObject.Find("WinsText");
        playerMoveCount = GameObject.Find("PlayerMoveCount");
        player = GameObject.Find("Player");
        sceneManager = GameObject.Find("SceneManager");

        player.GetComponent<PlayerMovement>().moveAllowed = false;

        winsTextShadow.gameObject.SetActive(false);
        playerMoveCount.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        // Check if the player finish the movement this round
        if (player.GetComponent<PlayerMovement>().moveFinished) {
            player.GetComponent<PlayerMovement>().moveAllowed = false;
            player.GetComponent<PlayerMovement>().currentWaypointIndex = player.GetComponent<PlayerMovement>().destinationWaypointIndex;

            // Check for rewarded steps
            if (!hasFinishedReward) {
                rewardPlayer();
            }
        }

        // // Check if finish the board once
        // if (player.GetComponent<PlayerMovement>().moveFinished && player.GetComponent<PlayerMovement>().currentWaypointIndex == player.GetComponent<PlayerMovement>().waypoints.Length) {
        //    print(player.GetComponent<PlayerMovement>().destinationWaypointIndex);
        //    print(player.GetComponent<PlayerMovement>().currentWaypointIndex);
        //    player.GetComponent<PlayerMovement>().destinationWaypointIndex = player.GetComponent<PlayerMovement>().destinationWaypointIndex - player.GetComponent<PlayerMovement>().currentWaypointIndex;
        //    player.GetComponent<PlayerMovement>().currentWaypointIndex = 0;
        //    print("Destination Set");
        //    print(player.GetComponent<PlayerMovement>().destinationWaypointIndex);

        //     // TODO: Add dialog to show the reward when every round is finished.
        //     // winsTextShadow.gameObject.SetActive(true);
        //     // playerMoveCount.gameObject.SetActive(false);
        //     // winsTextShadow.GetComponent<Text>().text = "Your first round finished, HERE IS THE REWARD OPTIONS...";
        //     // gameOver = true; The gameover will be used when the user finished the game
        // }
    }

    public static void MovePlayer() {
        // Reset reward status
        hasFinishedReward = false;

        // Setup UI
        playerMoveCount.gameObject.SetActive(true);
        playerMoveCount.GetComponent<Text>().text = "You move " + diceSideThrown.ToString() + " steps";

        // Setup player movement
        player.GetComponent<PlayerMovement>().moveForward = true;
        player.GetComponent<PlayerMovement>().destinationWaypointIndex = player.GetComponent<PlayerMovement>().currentWaypointIndex + diceSideThrown;
        player.GetComponent<PlayerMovement>().moveFinished = false;
        player.GetComponent<PlayerMovement>().moveAllowed = true;
    }

    private void rewardPlayer() {
        // Current Index
        int currentIndex = player.GetComponent<PlayerMovement>().currentWaypointIndex;

        // Check steps
        int rewardedSteps = waypoints_reward[currentIndex];

        // Set reward finish if no reward
        if (rewardedSteps == 0) {
            hasFinishedReward = true;

            // Load mini game if no reward and is in minigamesIndexes
            if (minigamesIndexes.Contains(currentIndex)) {
            sceneManager.GetComponent<SceneTransitions>().loadScene(sceneName: "HexgonScene");
            }
            return;
        }

        // Setup UI
        playerMoveCount.gameObject.SetActive(true);
        playerMoveCount.GetComponent<Text>().text = "You are rewarded to move " + rewardedSteps + " steps";

        // Setup player movement
        if (rewardedSteps > 0) {
            player.GetComponent<PlayerMovement>().moveForward = true;
        } else if (rewardedSteps < 0) {
            player.GetComponent<PlayerMovement>().moveForward = false;
        }

        player.GetComponent<PlayerMovement>().destinationWaypointIndex = Math.Max(player.GetComponent<PlayerMovement>().currentWaypointIndex + rewardedSteps, 0);
        player.GetComponent<PlayerMovement>().moveFinished = false;
        player.GetComponent<PlayerMovement>().moveAllowed = true;
    }
}
