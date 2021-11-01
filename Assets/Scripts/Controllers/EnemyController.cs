using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BieleStudios.GitHubGameOff.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Monster Stats")]
        public float Speed = 0.01f;

        private LevelController levelController;

        private Vector2 currentWaypoint;
        private int currentWaypointCursor;

        // Start is called before the first frame update
        void Start()
        {
            levelController = GameObject.Find("SceneScriptsObject").GetComponent<LevelController>();

            currentWaypointCursor = 0;
            currentWaypoint = levelController.Waypoints[currentWaypointCursor].transform.position;
        }

        public int GetCurrentWaypoint()
        {
            return currentWaypointCursor;
        }

        public float GetPredictedDistance(float time)
        {
            return Speed * time*50; 
        }

        public Vector2 GetPredictedLocation(float distance)
        {
            int i = 0;
            Vector2 previousPredictedWaypoint = transform.position;
            Vector2 currentPredictedWaypoint = levelController.Waypoints[currentWaypointCursor + i].transform.position;

            while (distance > Vector2.Distance(transform.position, currentPredictedWaypoint))
            {
                distance = distance - Vector2.Distance(transform.position, currentPredictedWaypoint);
                //Debug.Log("Remaining Distance is... " + distance);

                previousPredictedWaypoint = levelController.Waypoints[currentWaypointCursor + i].transform.position;
                i++;
                currentPredictedWaypoint = levelController.Waypoints[currentWaypointCursor + i].transform.position;
            }

            return Vector2.MoveTowards(previousPredictedWaypoint, currentPredictedWaypoint, distance);
        }

        void FixedUpdate()
        {
            if (Vector2.Distance(this.transform.position, currentWaypoint) < 0.01f)
            {
                currentWaypointCursor++;
                currentWaypoint = levelController.Waypoints[currentWaypointCursor].transform.position;
            }

            this.transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, Speed);
        }
    }
}

