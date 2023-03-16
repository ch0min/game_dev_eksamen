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

    // PATROLLING WAIT TIME
    float waitTime;
    public float startWaitTime = 1f;

    // CHASING
    public float distToPlayer = 5.0f; // Radius
    public float chaseRadius = 10f;
    public float facePlayerFactor = 20f;

    // AI SIGHT
    public bool playerInLOS = false;
    public float fieldOfViewAngle = 160f; // degrees forward facing
    public float radiusLOS = 45f;

    // AI MEMORY
    bool memorizesPlayerAI = false;
    public float memoryStartTime = 15f;
    float increasingMemoryTime;

    // AI HEARING
    Vector3 noisePosition;
    bool heardPlayerAI = false;
    public float noiseTravelDistance = 5f;
    public float spinSpeed = 3f;
    bool canSpin = false;
    float isSpinningTime; // Searching at player-noise-position
    public float spinTime = 3f;


    /* AI STRAFING */
    // float randomStrafeStartTime;
    // float waitStrafeTime;
    // public float t_minStrafe; // min and max time AI waits once it has reached the "strafe" position before strafing again.
    // public float t_maxStrafe;
    // int randomStrafeDir;
    // public Transform strafeLeft;
    // public Transform strafeRight;


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
        if (distance <= radiusLOS) {
            LOSCheck();
        }

        if (navAgent.isActiveAndEnabled) { // Patrolling
            if (playerInLOS == false && memorizesPlayerAI == false && heardPlayerAI == false) {
                Patrol();
                NoiseCheck();
                StopCoroutine(AIMemory());
            }
            else if (playerInLOS == false && memorizesPlayerAI == false && heardPlayerAI == true) { // If AI hears something, go search the noise.
                canSpin = true;
                NoisePositionCheck();
            }
            else if (playerInLOS == true) { // If AI sees Player, chase and remember the player for seconds~
                memorizesPlayerAI = true;
                FacePlayer();
                ChasePlayer();
            }
            else if (playerInLOS == false && memorizesPlayerAI == true) { // AI forgets about the player after seconds~
                ChasePlayer();
                StartCoroutine(AIMemory());
            }
        }
    }

    void NoiseCheck() {
        float distance = Vector3.Distance(PlayerMovement.playerPos, transform.position);
        if (distance <= noiseTravelDistance) {
            if (Input.GetButton("Fire1")) {
                noisePosition = PlayerMovement.playerPos;
                heardPlayerAI = true;
            }
            else {
                heardPlayerAI = false;
                canSpin = false;
            }
        }
    }

    void NoisePositionCheck() {
        navAgent.SetDestination(noisePosition);
        if (Vector3.Distance(transform.position, noisePosition) <= 5f && canSpin == true) {
            isSpinningTime += Time.deltaTime;

            transform.Rotate(Vector3.up * spinSpeed, Space.World);

            if (isSpinningTime >= spinTime) {
                canSpin = false;
                heardPlayerAI = false;
                isSpinningTime = 0f;
            }
        }
    }

    IEnumerator AIMemory() {
        increasingMemoryTime = 0;

        while (increasingMemoryTime < memoryStartTime) {
            increasingMemoryTime += Time.deltaTime;
            memorizesPlayerAI = true;
            yield return null;
        }
        heardPlayerAI = false;
        memorizesPlayerAI = false;
    }

    void LOSCheck() {
        Vector3 direction = PlayerMovement.playerPos - transform.position;

        float angle = Vector3.Angle(direction, transform.forward);

        if (angle < fieldOfViewAngle * 0.5f) {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, direction.normalized, out hit, radiusLOS)) {
                if (hit.collider.tag == "Player") {
                    playerInLOS = true;
                    memorizesPlayerAI = true;
                }
                else {
                    playerInLOS = false;
                }
            }
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

    void FacePlayer() {
        Vector3
            direction = (PlayerMovement.playerPos - transform.position).normalized; // Vector3.normalized vector keeps the same direction but its length is 1.0.
        Quaternion
            lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0,
                direction.z)); // Quaternion is used for rotations. y=0 because it's a top-down game.
        // Use this to create a rotation which smoothly interpolates between the first quaternion a to the second quaternion b, based on the value of the parameter t.
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * facePlayerFactor);

    }

    void ChasePlayer() {
        float distance = Vector3.Distance(PlayerMovement.playerPos, transform.position);

        if (distance <= chaseRadius && distance > distToPlayer) {
            navAgent.SetDestination(PlayerMovement.playerPos);
        }

        // STRAFING
    //     else if (navAgent.isActiveAndEnabled && distance <= distToPlayer) {
    //         randomStrafeDir = Random.Range(0, 2);
    //         randomStrafeStartTime = Random.Range(t_minStrafe, t_maxStrafe);
    //     
    //         if (waitStrafeTime <= 0) {
    //             if (randomStrafeDir == 0) {
    //                 navAgent.SetDestination(strafeLeft.position);
    //             }
    //             else if (randomStrafeDir == 1) {
    //                 navAgent.SetDestination(strafeRight.position);
    //             }
    //             waitStrafeTime = randomStrafeStartTime;
    //         }
    //         else {
    //             waitStrafeTime -= Time.deltaTime;
    //         }
    //     }
    }

    // void LateUpdate() {     // Helps the AI a bit to go around a wall towards the player.
    //     if (memorizesPlayerAI == true && playerInLOS == false) {
    //         distToPlayer = 5.0f;
    //     }
    //     else {
    //         distToPlayer = 0.0f;
    //     }
    // }
}
