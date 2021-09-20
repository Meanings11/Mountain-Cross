using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Transform[] waypoints;

    [SerializeField]
    private float moveSpeed = 1f;

    [HideInInspector]
    public int currentWaypointIndex = 0;
    public int destinationWaypointIndex = 0;
    public bool moveAllowed = false;
    public bool moveForward = true;
    public bool moveFinished = false;

	// Use this for initialization
	private void Start () {
        // Load data from Playerprefs -- This might be from the previous scene, or meaybe even from the previous execution.
        currentWaypointIndex = PlayerPrefs.GetInt("lastWaypointIndex", 0);
        transform.position = waypoints[currentWaypointIndex].transform.position;
	}
	
	// Update is called once per frame
	private void Update () {
        if (moveAllowed)
            moveToDestination();
	}

    void OnDestroy() {
        // Before we get destroyed, we save data to our save file.
        PlayerPrefs.SetInt("lastWaypointIndex", destinationWaypointIndex);
    }

    private void moveToDestination() {
        if (moveForward) {
            if (currentWaypointIndex <= destinationWaypointIndex && currentWaypointIndex < waypoints.Length)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                waypoints[currentWaypointIndex].transform.position,
                moveSpeed * Time.deltaTime);

                if (transform.position == waypoints[currentWaypointIndex].transform.position)
                {
                    currentWaypointIndex += 1;
                }
                moveFinished = false;
            } else {
                moveFinished = true;
            }
        } else {
            if (currentWaypointIndex >= destinationWaypointIndex && currentWaypointIndex >= 0)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                waypoints[currentWaypointIndex].transform.position,
                moveSpeed * Time.deltaTime);

                if (transform.position == waypoints[currentWaypointIndex].transform.position)
                {
                    currentWaypointIndex -= 1;
                }
                moveFinished = false;
            } else {
                moveFinished = true;
            }
        }
    }
}
