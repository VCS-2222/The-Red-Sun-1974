using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTrackSystem : MonoBehaviour
{
    [Serializable]
    public struct trainCart
    {
        public GameObject cart;
        public int currentWaypointTarget;
        public Vector3 startLerpPosition;

        public Vector3 targetWaypoint;
        public float elapsedTime;

        public void ResetStartTrans()
        {
            startLerpPosition = cart.transform.position;
        }
    }

    public trainCart[] trainCar;

    public float speed;
    public float maximumSpeed;
    public float speedChangerMultiplier;
    public Transform[] waypoints;
    public bool isClosedTrack;
    public bool trainIsStopped;
    public bool trainIsStopping;

    public enum trainStates
    {
        stationary,
        departure,
        moving,
        arriving
    }

    public trainStates trainSpeedStates;

    public AudioSource cartStarting;
    public AudioSource cartMoving;
    public AudioSource cartStopping;

    private void Start()
    {
        for (int t = 0; t < trainCar.Length; t++)
        {
            trainCar[t].ResetStartTrans();
            trainCar[t].targetWaypoint = waypoints[0].position;
        }
    }

    private void Update()
    {
        TrainCartLogic();

        if (trainIsStopping)
        {
            StopOverTime();
        }

        if (!trainIsStopping)
        {
            StartOverTime();
        }
    }

    private void FixedUpdate()
    {
        CheckTrainStates();
        HandleTrainAudio();
    }

    void HandleTrainAudio()
    {
        switch (trainSpeedStates)
        {
            case trainStates.stationary:
                cartMoving.Stop();
                cartStarting.Stop();
                cartStopping.Stop();
                break;

            case trainStates.departure:
                cartStopping.Stop();
                cartMoving.Stop();

                if (cartStarting.isPlaying) return;
                cartStarting.Play();
                break;

            case trainStates.moving:
                cartStarting.Stop();
                cartStopping.Stop();

                if (cartMoving.isPlaying) return;
                cartMoving.Play();
                break;

            case trainStates.arriving:
                cartMoving.Stop();
                cartStarting.Stop();

                if (cartStopping.isPlaying) return;
                cartStopping.Play();
                break;
        }
    }

    void CheckTrainStates()
    {
        if (speed <= 0.2f && trainIsStopping)
        {
            trainSpeedStates = trainStates.arriving;
        }
        if (speed >= 0.01f && !trainIsStopping)
        {
            trainSpeedStates = trainStates.departure;
        }
        if (speed >= 0.1f && !trainIsStopping)
        {
            trainSpeedStates = trainStates.moving;
        }
        if (speed == 0)
        {
            trainSpeedStates = trainStates.stationary;
        }
    }

    public void StopOverTime()
    {
        if(speed > 0)
        {
            speed -= speedChangerMultiplier;
        }
        
        if(speed < 0)
        {
            speed = 0;
        }
    }

    public void StartOverTime()
    {
        if (speed < maximumSpeed)
        {
            speed += speedChangerMultiplier;
        }

        if (speed >= maximumSpeed)
        {
            speed = maximumSpeed;
        }
    }

    void TrainCartLogic()
    {
        for (int t = 0; t < trainCar.Length; t++)
        {
            trainCar[t].elapsedTime += speed;

            trainCar[t].cart.transform.position = Vector3.MoveTowards(trainCar[t].startLerpPosition, trainCar[t].targetWaypoint, trainCar[t].elapsedTime);

            if (trainCar[t].cart.transform.position == trainCar[t].targetWaypoint)
            {
                trainCar[t].currentWaypointTarget++;

                if (trainCar[t].currentWaypointTarget > waypoints.Length && !isClosedTrack)
                {
                    trainIsStopped = true;
                }
            }

            for (int i = 0; i < trainCar[t].currentWaypointTarget; i++)
            {
                if (trainCar[t].currentWaypointTarget > waypoints.Length && isClosedTrack)
                {
                    trainIsStopped = false;
                    trainCar[t].currentWaypointTarget = 0;
                }
                else
                {
                    if (trainIsStopped) return;
                    trainCar[t].targetWaypoint = waypoints[i].position;
                    trainCar[t].cart.transform.LookAt(waypoints[i].position);
                }
            }

            trainCar[t].elapsedTime = 0;
            trainCar[t].ResetStartTrans();
        }
    }
}