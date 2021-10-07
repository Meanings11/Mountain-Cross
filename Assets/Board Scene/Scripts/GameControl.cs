using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {
    public GameObject landscapeCanvas;
    public GameObject portraitCanvas;

    // private static GameObject winsTextShadow;
    private static GameObject playerMoveCount;

    private static GameObject player;

    private static GameObject sceneManager;

    public static int diceSideThrown = 0;

    public static bool gameOver = false;

    public static bool hasFinishedReward = true;

    HashSet<int> minigamesIndexes = new HashSet<int>();
    private int[] waypoints_reward = {0, 0, 4, 0, 0, 0, 0,
                                      0, 0, 0, -10, 0, 0,
                                      0, 0, 0, 0, 0, -2,
                                      0, 0, 0, 0, 0, 0};
    private int[] score_rewad = {0, 0, 0, 0, 0, 0, 0,
                                0, -200, 0, 0, 0, 0,
                                0, 0, 0, 0, 340, 0,
                                0, -120, 0, 0, 0, 0};
    
    // Use this for initialization
    void Start () {
        // Screen.orientation = ScreenOrientation.LandscapeLeft;
        // Screen.orientation = ScreenOrientation.AutoRotation;

        /*if (Application.platform != RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.Android) {
            landscapeCanvas.gameObject.SetActive(true);
            portraitCanvas.gameObject.SetActive(false);
        } else {
            if (Screen.width / Screen.height > 1.4f) {
                if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight) {
                    landscapeCanvas.gameObject.SetActive(true);
                    portraitCanvas.gameObject.SetActive(false);
                }
            } else {
                landscapeCanvas.gameObject.SetActive(false);
                portraitCanvas.gameObject.SetActive(true);
            }
        }*/
        ScreenRotate();

        minigamesIndexes.Add(4);
        minigamesIndexes.Add(5);
        minigamesIndexes.Add(6);
        minigamesIndexes.Add(9);
        minigamesIndexes.Add(10);
        minigamesIndexes.Add(12);
        minigamesIndexes.Add(13);
        minigamesIndexes.Add(15);
        minigamesIndexes.Add(19);
        minigamesIndexes.Add(21);
        minigamesIndexes.Add(23);

        // winsTextShadow = GameObject.Find("WinsText");
        playerMoveCount = GameObject.Find("PlayerMoveCount");
        player = GameObject.Find("Player");
        sceneManager = GameObject.Find("SceneManager");

        player.GetComponent<PlayerMovement>().moveAllowed = false;

        // winsTextShadow.gameObject.SetActive(false);
        playerMoveCount.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        // Debug.Log(PlayerPrefs.GetInt("lastWaypointIndex", 0));
        // Screen.orientation = ScreenOrientation.AutoRotation;

        /*if (Application.platform != RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.Android) {
            landscapeCanvas.gameObject.SetActive(true);
            portraitCanvas.gameObject.SetActive(false);
        } else {
            if (Screen.width / Screen.height > 1.4f) {
                if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight) {
                    landscapeCanvas.gameObject.SetActive(true);
                    portraitCanvas.gameObject.SetActive(false);
                }
            } else {
                landscapeCanvas.gameObject.SetActive(false);
                portraitCanvas.gameObject.SetActive(true);
            }
        }*/
        ScreenRotate();

        // Check if the player finish the movement this round
        if (player.GetComponent<PlayerMovement>().moveFinished) {
            player.GetComponent<PlayerMovement>().moveAllowed = false;
            player.GetComponent<PlayerMovement>().currentWaypointIndex = player.GetComponent<PlayerMovement>().destinationWaypointIndex;
            PlayerPrefs.SetInt("lastWaypointIndex", player.GetComponent<PlayerMovement>().currentWaypointIndex);

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

    public void ScreenRotate() {
        if (landscapeCanvas.gameObject.activeSelf) {
            portraitCanvas.gameObject.SetActive(false);
        } else if (portraitCanvas.gameObject.activeSelf) {
            landscapeCanvas.gameObject.SetActive(false);
        }
        if (Application.platform != RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.Android) {
            landscapeCanvas.gameObject.SetActive(true);
            portraitCanvas.gameObject.SetActive(false);
        } else {
            if ((Screen.height / Screen.width > 1.99f || Screen.width / Screen.height > 1.99f) && (Application.platform == RuntimePlatform.Android)) {
                landscapeCanvas.gameObject.SetActive(true);
                portraitCanvas.gameObject.SetActive(false);
                if (Input.deviceOrientation == DeviceOrientation.Portrait || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown
                    || Input.deviceOrientation == DeviceOrientation.FaceUp || Input.deviceOrientation == DeviceOrientation.FaceDown) {
                    Screen.orientation = ScreenOrientation.LandscapeLeft;
                }
            } else {
                if (Screen.width / Screen.height > 1.4f) {
                    if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight) {
                        landscapeCanvas.gameObject.SetActive(true);
                        portraitCanvas.gameObject.SetActive(false);
                    } else if (Input.deviceOrientation == DeviceOrientation.Portrait || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown) {
                        landscapeCanvas.gameObject.SetActive(false);
                        portraitCanvas.gameObject.SetActive(true);
                    } else {
                        if (landscapeCanvas.gameObject.activeSelf) {
                            portraitCanvas.gameObject.SetActive(false);
                        } else if (portraitCanvas.gameObject.activeSelf) {
                            landscapeCanvas.gameObject.SetActive(false);
                        }
                    }
                } else {
                    landscapeCanvas.gameObject.SetActive(false);
                    portraitCanvas.gameObject.SetActive(true);
                }
            }
        }
    }

    public static void MovePlayer() {
        // Reset reward status
        hasFinishedReward = false;

        // Setup UI
        playerMoveCount.gameObject.SetActive(true);
        playerMoveCount.GetComponent<Text>().text = "Move " + diceSideThrown.ToString() + " steps";

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
            int currentGameScore = PlayerPrefs.GetInt("totalGameScore", 0);

            // Adjust gamescore if it is scroe rewad
            if (score_rewad[currentIndex] != 0) {
                // Show reward
                playerMoveCount.gameObject.SetActive(true);
                if (score_rewad[currentIndex] > 0) {
                    playerMoveCount.GetComponent<Text>().text = "Rewarded with $" + score_rewad[currentIndex];
                } else {
                    if (currentGameScore + score_rewad[currentIndex] <= 0) {
                        playerMoveCount.GetComponent<Text>().text = "Lose all the money...";
                    } else {
                        playerMoveCount.GetComponent<Text>().text = "Lose $" + Mathf.Abs(score_rewad[currentIndex]);
                    }
                }

                // Adjust score
                int newGameScore = Math.Max(currentGameScore + score_rewad[currentIndex], 0);
                PlayerPrefs.SetInt("totalGameScore", newGameScore);

                hasFinishedReward = true;
            } else {
                if (minigamesIndexes.Contains(currentIndex)) { // Load mini game if no reward and is in minigamesIndexes
                    // Show reward
                    playerMoveCount.gameObject.SetActive(true);
                    playerMoveCount.GetComponent<Text>().text = "Game Time!";

                    // Go to mini-game
                    if (currentIndex == 5) {
                        sceneManager.GetComponent<SceneTransitions>().loadScene(sceneName: "ParachuteScene");
                    } else if (currentIndex == 6) {
                        sceneManager.GetComponent<SceneTransitions>().loadScene(sceneName: "CorgiScene");
                    } else if (currentIndex == 21) {
                        sceneManager.GetComponent<SceneTransitions>().loadScene(sceneName: "MosquitoScene");
                    } else {
                        // random go to unassigned games
                        int randomIndex = UnityEngine.Random.Range(0, 4); // random decide for now
                        // if (randomIndex == 0) {
                        //     sceneManager.GetComponent<SceneTransitions>().loadScene(sceneName: "MosquitoScene");
                        // } else {
                        //     sceneManager.GetComponent<SceneTransitions>().loadScene(sceneName: "HexgonScene");
                        // }

                        switch (randomIndex)
                        {
                            case 0: sceneManager.GetComponent<SceneTransitions>().loadScene(sceneName: "MosquitoScene"); break;
                            case 1: sceneManager.GetComponent<SceneTransitions>().loadScene(sceneName: "CorgiScene"); break;
                            case 2: sceneManager.GetComponent<SceneTransitions>().loadScene(sceneName: "ParachuteScene"); break;
                            case 3: sceneManager.GetComponent<SceneTransitions>().loadScene(sceneName: "Whack-A-Mole"); break;
                        }
                    }

                    StartCoroutine(disableDice());
                } else {
                    if (currentIndex == 1 || currentIndex == 11 || currentIndex == 22) {
                        playerMoveCount.GetComponent<Text>().text = "Skip";
                    } else if (currentIndex == 7) {
                        int newGameScore = 0;
                        playerMoveCount.GetComponent<Text>().text = "Lose all the money...";
                        PlayerPrefs.SetInt("totalGameScore", newGameScore);
                    } else if (currentIndex == 14) {
                        int newGameScore = currentGameScore * 2;
                        playerMoveCount.GetComponent<Text>().text = "Double your money!";
                        PlayerPrefs.SetInt("totalGameScore", newGameScore);
                    } else if (currentIndex == 3 || currentIndex == 16) {
                        int randomReward = UnityEngine.Random.Range(-100, 101);
                        int adjustedReward = randomReward * 10;
                        if (adjustedReward > 0) {
                            playerMoveCount.GetComponent<Text>().text = "Surprise! You get $" + adjustedReward;
                        } else {
                            if (currentGameScore + adjustedReward <= 0) {
                                playerMoveCount.GetComponent<Text>().text = "Surprise! You lose all your money...";
                            } else {
                                playerMoveCount.GetComponent<Text>().text = "Surprise! You lose $" + Mathf.Abs(adjustedReward);
                            }
                        }
                        // Adjust score
                        int newGameScore = Math.Max(currentGameScore + adjustedReward, 0);
                        PlayerPrefs.SetInt("totalGameScore", newGameScore);
                    }
                    hasFinishedReward = true;
                }
            }
            
            return;
        }

        IEnumerator disableDice() {
            yield return new WaitForSeconds(1.5f);
            hasFinishedReward = true;
        }

        // Setup UI
        playerMoveCount.gameObject.SetActive(true);

        // Setup player movement
        if (rewardedSteps > 0) {
            playerMoveCount.GetComponent<Text>().text = "Move " + rewardedSteps + " more steps";
            player.GetComponent<PlayerMovement>().moveForward = true;
        } else if (rewardedSteps < 0) {
            if (currentIndex == 10) {
                playerMoveCount.GetComponent<Text>().text = "Move back to origin";
            } else {
                playerMoveCount.GetComponent<Text>().text = "Reverse " + Mathf.Abs(rewardedSteps) + " steps";
            }
            player.GetComponent<PlayerMovement>().moveForward = false;
        }

        player.GetComponent<PlayerMovement>().destinationWaypointIndex = Math.Max(player.GetComponent<PlayerMovement>().currentWaypointIndex + rewardedSteps, 0);
        player.GetComponent<PlayerMovement>().moveFinished = false;
        player.GetComponent<PlayerMovement>().moveAllowed = true;
    }
}
