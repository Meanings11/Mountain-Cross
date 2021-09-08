using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    private static GameObject winsTextShadow, playerMoveCount;

    private static GameObject player1;

    public static int diceSideThrown = 0;
    public static int player1StartWaypoint = 0;

    public static bool gameOver = false;

    // Use this for initialization
    void Start () {

        winsTextShadow = GameObject.Find("WinsText");
        playerMoveCount = GameObject.Find("Player1MoveCount");

        player1 = GameObject.Find("Player1");

        player1.GetComponent<FollowThePath>().moveAllowed = false;

        winsTextShadow.gameObject.SetActive(false);
        playerMoveCount.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.GetComponent<FollowThePath>().waypointIndex > 
            player1StartWaypoint + diceSideThrown)
        {
            player1.GetComponent<FollowThePath>().moveAllowed = false;
            playerMoveCount.gameObject.SetActive(false);
            player1StartWaypoint = player1.GetComponent<FollowThePath>().waypointIndex - 1;
        }

        if (player1.GetComponent<FollowThePath>().waypointIndex == 
            player1.GetComponent<FollowThePath>().waypoints.Length)
        {
            winsTextShadow.gameObject.SetActive(true);
            playerMoveCount.gameObject.SetActive(false);
            winsTextShadow.GetComponent<Text>().text = "You Wins";
            gameOver = true;
        }
    }

    public static void MovePlayer()
    {
        playerMoveCount.gameObject.SetActive(true);
        playerMoveCount.GetComponent<Text>().text = "You move " + diceSideThrown.ToString() + " steps";
        player1.GetComponent<FollowThePath>().moveAllowed = true;
    }
}
