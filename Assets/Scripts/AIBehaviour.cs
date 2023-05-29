using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AIBehaviour : MonoBehaviour
{
    public Transform[] moveSpots;
    private NavMeshAgent navAgent;
    private Animator anim;

    [SerializeField]
    private float attackDistance = 3f;
    public float damage = -10f;

    // Field of View
    public bool canSeePlayer;
    public float radiusFOV;
    [Range(0, 360)]
    public float angleFOV;
    public GameObject playerRef;
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    // Patrolling and AI Memory
    private float waitTime;
    public float startWaitTime = 1f;
    private bool memorizesPlayerAI = false;
    public float memoryStartTime = 15f;
    private float increasingMemoryTime;

    // AI Hearing
    private Vector3 noisePosition;
    public bool heardPlayerAI = false;
    public float noiseTravelDistance = 50.0f;



    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.enabled = true;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());

        waitTime = startWaitTime;
        int randomSpot = Random.Range(0, moveSpots.Length);
        navAgent.SetDestination(moveSpots[randomSpot].position);
    }

    private void Update()
    {
        if (!navAgent.isActiveAndEnabled) return;

        if (!canSeePlayer && !memorizesPlayerAI)
        {
            Patrol();
            NoiseCheck();
            StopCoroutine(AIMemory());
            anim.SetBool("canSeePlayer", false);
            anim.SetBool("attack", false);
        }
        else if (canSeePlayer)
        {
            memorizesPlayerAI = true;
            ChasePlayer();
            anim.SetBool("canSeePlayer", true);
            anim.SetBool("attack", false);

            if (Vector3.Distance(transform.position, playerRef.transform.position) < attackDistance)
            {
                anim.SetBool("attack", true);
            }
            else
            {
                anim.SetBool("attack", false);
            }
        }
        else if (!canSeePlayer && memorizesPlayerAI)
        {
            ChasePlayer();
            StartCoroutine(AIMemory());
            anim.SetBool("canSeePlayer", false);
            anim.SetBool("attack", false);
        }
        else
        {
            anim.SetBool("canSeePlayer", false);
            anim.SetBool("attack", false);
        }

        float hearingDistance = Vector3.Distance(PlayerController.playerPos, transform.position);
        if (hearingDistance <= noiseTravelDistance)
        {
            if (Input.GetButton("Fire1"))
            {
                noisePosition = PlayerController.playerPos;
                heardPlayerAI = true;
            }
            else
            {
                heardPlayerAI = false;
            }
        }
    }


    private IEnumerator AIMemory()
    {
        increasingMemoryTime = 0;
        while (increasingMemoryTime < memoryStartTime)
        {
            increasingMemoryTime += Time.deltaTime;
            memorizesPlayerAI = true;
            yield return null;
        }
        heardPlayerAI = false;
        memorizesPlayerAI = false;
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radiusFOV, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angleFOV / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                    ChasePlayer();
                    angleFOV = 300f;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }

    private void NoiseCheck()
    {
        float distance = Vector3.Distance(PlayerController.playerPos, transform.position);
        if (distance <= noiseTravelDistance)
        {
            if (Input.GetButton("Fire1"))
            {
                ChasePlayer();
                heardPlayerAI = true;
                canSeePlayer = true;
                radiusFOV = 20f;
                angleFOV = 160f;
            }
            else
            {
                heardPlayerAI = false;
                radiusFOV = 10f;
                angleFOV = 110f;
            }
        }
    }

    private void Patrol()
    {
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                int randomSpot = Random.Range(0, moveSpots.Length);
                navAgent.SetDestination(moveSpots[randomSpot].position);
                waitTime = startWaitTime;
            }
        }
    }

    public void ChasePlayer()
    {
        if (canSeePlayer)
        {
            navAgent.SetDestination(playerRef.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ModifyHealth(damage);
            }
        }
    }
}
