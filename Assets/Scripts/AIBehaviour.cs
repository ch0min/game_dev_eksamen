using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

/*** ÆNDRE HURTIGHED PÅ FJENDEN NÅR VI HAR IMPLEMENTERET SKIN ***/

/***  ***/

public class AIBehaviour : MonoBehaviour
{
    public Transform[] moveSpots;
    NavMeshAgent navAgent;
    int randomSpot;
    
    // FIELD OF VIEW
    public bool canSeePlayer;
    public float radiusFOV;
    [Range(0, 360)]
    public float angleFOV;
    public GameObject playerRef;
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    // PATROLLING WAIT TIME
    float waitTime;
    public float startWaitTime = 1f;
    
    // AI MEMORY
    bool memorizesPlayerAI = false;
    public float memoryStartTime = 15f;
    float increasingMemoryTime;

    // AI HEARING
    Vector3 noisePosition;
    public bool heardPlayerAI = false;
    public float noiseTravelDistance = 50.0f;
    float isSpinningTime; // Searching at player-noise-position

    void Awake() {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.enabled = true;
    }

    void Start() {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
        
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    void Update() {
        if (navAgent.isActiveAndEnabled) { // Patrolling
            if (canSeePlayer == false && memorizesPlayerAI == false) {
                Patrol();
                NoiseCheck();
                StopCoroutine(AIMemory());
            }
            else if (canSeePlayer) { // If AI sees Player, chase and remember the player for seconds~
                memorizesPlayerAI = true;
                ChasePlayer();
            }
            else if (canSeePlayer == false && memorizesPlayerAI) { // AI forgets about the player after seconds~
                ChasePlayer();
                StartCoroutine(AIMemory());
            }
        }

        float hearingDistance = Vector3.Distance(PlayerMovement.playerPos, transform.position);
        if (hearingDistance <= noiseTravelDistance) {
            if (Input.GetButton("Fire1")) {
                noisePosition = PlayerMovement.playerPos;
                heardPlayerAI = true;
            }
            else {
                heardPlayerAI = false;
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
    
    IEnumerator FOVRoutine() {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        
        while (true) {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    void FieldOfViewCheck() {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radiusFOV, targetMask);

        if (rangeChecks.Length != 0) {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angleFOV / 2) {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) {
                    canSeePlayer = true;
                    ChasePlayer();
                    angleFOV = 300f;
                }
                else {
                    canSeePlayer = false;
                }
            }
            else {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer) {
            canSeePlayer = false;
        }
    }
    
    void NoiseCheck() {
        float distance = Vector3.Distance(PlayerMovement.playerPos, transform.position);
        if (distance <= noiseTravelDistance) {
            if (Input.GetButton("Fire1")) {
                ChasePlayer();
                heardPlayerAI = true;
                canSeePlayer = true;
                radiusFOV = 20f;
                angleFOV = 160f;
            }
            else {
                heardPlayerAI = false;
                radiusFOV = 15f;
                angleFOV = 110f;
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

    public void ChasePlayer() {
        if (canSeePlayer) {
            navAgent.SetDestination(PlayerMovement.playerPos);
        }
    }
    
}
