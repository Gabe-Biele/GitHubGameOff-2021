using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private LevelController levelController;

    private Vector2 currentWaypoint;
    private int currentWaypointCursor;

    // Start is called before the first frame update
    void Start()
    {
        levelController = GameObject.Find("SceneScriptsObject").GetComponent<LevelController>();

        currentWaypointCursor = 0;
        currentWaypoint = levelController.Waypoints[currentWaypointCursor];
    }

    void FixedUpdate()
    {
        if(Vector2.Distance(this.transform.position, currentWaypoint) < 0.01f)
        {
            currentWaypointCursor++;
            currentWaypoint = levelController.Waypoints[currentWaypointCursor];
        }

        this.transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, 0.05f);
    }
}
