using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {

    private static GameObject winsTextShadow, playerMoveCount;

    private static GameObject player1;

    private static GameObject sceneManager;

    public static int diceSideThrown = 0;

    public static bool gameOver = false;

    private static bool isRewarded = true;

    HashSet<int> minigamesIndexes = new HashSet<int>();

    // Use this for initialization
    void Start () {
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
        playerMoveCount = GameObject.Find("Player1MoveCount");
        player1 = GameObject.Find("Player1");
        sceneManager = GameObject.Find("SceneManager");

        player1.GetComponent<PlayerMovement>().moveAllowed = false;

        winsTextShadow.gameObject.SetActive(false);
        playerMoveCount.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        // Check if the player finish the movement this round
        if (player1.GetComponent<PlayerMovement>().moveFinished) {
            player1.GetComponent<PlayerMovement>().moveAllowed = false;
            // playerMoveCount.gameObject.SetActive(false);
            player1.GetComponent<PlayerMovement>().currentWaypointIndex = player1.GetComponent<PlayerMovement>().destinationWaypointIndex;

            // Check for rewarded steps
            if (!isRewarded) {
                rewardPlayer();
            }
        }

        // // Check if finish the board
        // if (player1.GetComponent<PlayerMovement>().currentWaypointIndex == player1.GetComponent<PlayerMovement>().waypoints.Length) {
        //     winsTextShadow.gameObject.SetActive(true);
        //     playerMoveCount.gameObject.SetActive(false);
        //     winsTextShadow.GetComponent<Text>().text = "You Wins";
        //     gameOver = true;
        // }
    }

    public static void MovePlayer() {
        // Reset Reward
        isRewarded = false;

        // Setup UI
        playerMoveCount.gameObject.SetActive(true);
        playerMoveCount.GetComponent<Text>().text = "You move " + diceSideThrown.ToString() + " steps";

        // Setup player movement
        player1.GetComponent<PlayerMovement>().moveForward = true;
        player1.GetComponent<PlayerMovement>().destinationWaypointIndex = player1.GetComponent<PlayerMovement>().currentWaypointIndex + diceSideThrown;
        player1.GetComponent<PlayerMovement>().moveFinished = false;
        player1.GetComponent<PlayerMovement>().moveAllowed = true;
    }

    private void rewardPlayer() {
        // Set rewarded
        isRewarded = true;

        // Check steps
        // int randomRewardIndex = UnityEngine.Random.Range(0, waypoints_reward.Length - 1);
        // int rewardedSteps = waypoints_reward[randomRewardIndex];

        // Load mini game if no reward -> for testing
        int currentIndex = player1.GetComponent<PlayerMovement>().currentWaypointIndex;
        if (minigamesIndexes.Contains(currentIndex)) {
            sceneManager.GetComponent<SceneTransitions>().loadScene(sceneName: "MiniGame-1");
            return;
        }

        // Setup UI
        // playerMoveCount.gameObject.SetActive(true);
        // playerMoveCount.GetComponent<Text>().text = "You are rewarded to move" + rewardedSteps + " steps";

        // // Setup player movement
        // if (rewardedSteps > 0) {
        //     player1.GetComponent<PlayerMovement>().moveForward = true;
        // } else if (rewardedSteps < 0) {
        //     player1.GetComponent<PlayerMovement>().moveForward = false;
        // }

        // player1.GetComponent<PlayerMovement>().destinationWaypointIndex = Math.Max(player1.GetComponent<PlayerMovement>().currentWaypointIndex + rewardedSteps, 0);
        // player1.GetComponent<PlayerMovement>().moveFinished = false;
        // player1.GetComponent<PlayerMovement>().moveAllowed = true;
    }
}
