using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AIBehaviour : MonoBehaviour
{
    public Transform[] moveSpots;
    NavMeshAgent navAgent;
    int randomSpot;

    // Wait time at waypoint for patrolling
    float waitTime;
    public float startWaitTime = 1f;

    // AI Strafe
    public float distToPlayer = 5.0f; // Strafe Radius

    float randomStrafeStartTime;
    float waitStrafeTime;
    public float t_minStrafe; // min and max time AI waits once it has reached the "strafe" position before strafing again.
    public float t_maxStrafe;
    int randomStrafeDir;
    public Transform strafeLeft;
    public Transform strafeRight;

    // When to chase
    public float chaseRadius = 20f;
    public float facePlayerFactor = 20f;


    void Awake() {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.enabled = true;
    }

    void Start() {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    void Update() {
        float distance = Vector3.Distance(PlayerMovement.playerPos, transform.position);

        if (distance > chaseRadius) {
            Patrol();
        }
        else if (distance <= chaseRadius) {
            ChasePlayer();
            FacePlayer();
        }
    }
    
        void Patrol() {
            navAgent.SetDestination(moveSpots[randomSpot].position);

            if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 2.0f) {
                if (waitTime <= 0) {
                    randomSpot = Random.Range(0, moveSpots.Length);
                    waitTime = startWaitTime;
                }
                else {
                    waitTime -= Time.deltaTime;
                }
            }
        }

        void ChasePlayer() {
            float distance = Vector3.Distance(PlayerMovement.playerPos, transform.position);

            if (distance <= chaseRadius && distance > distToPlayer) {
                navAgent.SetDestination(PlayerMovement.playerPos);
            }
            else if (navAgent.isActiveAndEnabled && distance <= distToPlayer) {
                randomStrafeDir = Random.Range(0, 2);
                randomStrafeStartTime = Random.Range(t_minStrafe, t_maxStrafe);

                if (waitStrafeTime <= 0) {
                    if (randomStrafeDir == 0) {
                        navAgent.SetDestination(strafeLeft.position);
                    }
                    else if (randomStrafeDir == 1) {
                        navAgent.SetDestination(strafeRight.position);
                    }
                    waitStrafeTime = randomStrafeStartTime;
                }
                else {
                    waitStrafeTime -= Time.deltaTime;
                }
            }
        }

        void FacePlayer() {
            Vector3
                direction = (PlayerMovement.playerPos - transform.position).normalized; // Vector3.normalized vector keeps the same direction but its length is 1.0.
            Quaternion
                lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0,
                    direction.z)); // Quaternion is used for rotations. y=0 because it's a top-down game.
            // Use this to create a rotation which smoothly interpolates between the first quaternion a to the second quaternion b, based on the value of the parameter t.
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * facePlayerFactor);

        }


    }
