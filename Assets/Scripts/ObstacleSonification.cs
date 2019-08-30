using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSonification : MonoBehaviour
{
    [Tooltip("Game object to use as obstacle beacon.")]
    public GameObject obstacleBeacon;

    public GameObject handBeacon;

    [Tooltip("Maximum headDistance to sonify obstacles.")]
    public float maxDist = 5;

    [Tooltip("Vertical distance to allow sonification. Only objects within X meters vertically of the user's head will sonify.")]
    public float vertLimit = 1;

    [Tooltip("Audio clips to use for obstacle detection.")]
    public AudioClip[] audioClips;

    public bool obstacleMode = false;
    public bool handBeaconOn = false;
    private float detectionRadius = 0.05f;

    public float headDistance;
    public float handDistance;
    private float distanceRatio;
    private float verticalDelta;
    private bool hitDetected;

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
                headDistance = Vector3.Distance(headPosition, hit.point);
                verticalDelta = Mathf.Abs(hit.point.y - headPosition.y);
                //Debug.Log("Hit! Position = " + obstacleBeacon.transform.position);

                hitDetected = true;

            }

            else
            {
                obstacleBeacon.transform.position = headPosition + gazeDirection * maxDist;
                headDistance = maxDist;
                hitDetected = false;
            }



            //Change pitch based on headDistance

            //Old system modified pitch programatically based on distance
            //float distanceRatio = (Mathf.Pow(headDistance, 2) / Mathf.Pow(maxDist, 2));
            //obstacleBeacon.GetComponent<AudioSource>().pitch = 2f - 0.1f*Mathf.Round(distanceRatio*10f);

            //New system swaps in chord note based on stepwise progression
            distanceRatio = headDistance / maxDist;
            int i = Mathf.RoundToInt(distanceRatio * audioClips.Length);
            if (i >= audioClips.Length)
                i = audioClips.Length - 1;
            if (i < 0)
                i = 0;
            obstacleBeacon.GetComponent<AudioSource>().clip = audioClips[audioClips.Length - 1 - i];
            if (!obstacleBeacon.GetComponent<AudioSource>().isPlaying)
                obstacleBeacon.GetComponent<AudioSource>().Play();

            //If not within vertical limit or no hit, mute beacon
            if (verticalDelta <= vertLimit && hitDetected)
                obstacleBeacon.GetComponent<AudioSource>().volume = 1;
            else
                obstacleBeacon.GetComponent<AudioSource>().volume = 0;

            //Debug.Log("Audio clip: " + i);
            //Debug.Log("Is audio clip playing? = " + obstacleBeacon.GetComponent<AudioSource>().isPlaying);
            //Debug.Log("Audio clip volume: " + obstacleBeacon.GetComponent<AudioSource>().volume);

        }

        //If hand beacon mode is on, place a beacon at the user's hand, whose audio qualities vary based on a raycast from the hand.
        if (handBeaconOn)
        {
            //var handPosition = 
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

    public void HandBeaconOn()
    {
        if (!handBeaconOn)
        {
            //Toggle on obstacle mode.
            handBeaconOn = true;

            //Activate obstacle beacon
            handBeacon.SetActive(true);

            Debug.Log("Hand Beacon on.");

        }

        //Play "Hand Beacon On" speech
        //insert code
    }

    public void HandBeaconOff()
    {
        if (handBeaconOn)
        {
            handBeaconOn = false;

            //Deactivate obstacle beacon
            handBeacon.SetActive(false);

            Debug.Log("Hand Beacon off.");

        }

        //Play "Hand Beacon off" speech
        //insert code
    }
}

