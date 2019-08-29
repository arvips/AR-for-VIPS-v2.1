using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSonification : MonoBehaviour
{
    [Tooltip("Game object to use as obstacle beacon.")]
    public GameObject obstacleBeacon;

    [Tooltip("Maximum distance to sonify obstacles.")]
    public float maxDist = 5;

    public bool obstacleMode = false;
    private float detectionRadius = 0.05f;

    public float distance;

    void Start()
    {
        
    }

    void Update()
    {
        //While obstacle mode is on, move the obstacle beacon to where the user's gaze is, or straight out 10m if no hit is detected.
        if (obstacleMode)
        {
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            //Debug.Log("Head position: " + headPosition);
            //Debug.Log("Gaze Direction: " + gazeDirection);

            RaycastHit hit;
            if (Physics.SphereCast(headPosition, detectionRadius, gazeDirection, out hit, maxDist)) {
                obstacleBeacon.transform.position = hit.point;
                distance = Vector3.Distance(headPosition, hit.point);
                //Debug.Log("Hit! Position = " + obstacleBeacon.transform.position);
            }

            else
            {
                obstacleBeacon.transform.position = headPosition + gazeDirection * maxDist;
                distance = Vector3.Distance(headPosition, hit.point);
                //Debug.Log("Miss. Position = " + obstacleBeacon.transform.position);
            }

            //Change pitch based on distance
            obstacleBeacon.GetComponent<AudioSource>().pitch = 0.5 + (10f - distance)/5;
        }
    }

    public void ObstaclesOn()
    {
        if (!obstacleMode)
        {
            //Toggle on obstacle mode.
            obstacleMode = true;

            //Activate obstacle beacon
            obstacleBeacon.SetActive(true);

            Debug.Log("Obstacle mode on.");

        }

        //Play "Obstacles on" speech
        //insert code
    }

    public void ObstaclesOff()
    {
        if (obstacleMode)
        {
            obstacleMode = false;

            //Deactivate obstacle beacon
            obstacleBeacon.SetActive(false);

            Debug.Log("Obstacle mode off.");

        }

        //Play "Obstacles off" speech
        //insert code
    }
}
