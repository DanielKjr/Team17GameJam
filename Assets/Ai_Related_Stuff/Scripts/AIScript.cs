using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AIScript : MonoBehaviour
{

	[SerializeField]
	private GameObject target;
	[SerializeField]
	private List<GameObject> targets = new List<GameObject>();
	private NavMeshAgent agent;

	private int index = 0;
	[SerializeField]
	private float distance = 0;


	[SerializeField]
	private Vector3 randomDirection = new Vector3();

	private AudioSource audioSource;
	[SerializeField]
	private float minimumSoundDistance = 0;
	private Vector3 prevPos;


	[SerializeField]
	private float fovRadius = 0;
	[SerializeField]
	private LayerMask layermask = 0;
	[SerializeField]
	private float viewAngle = 0;
	[SerializeField]
	private LayerMask obstructionmask;
	[SerializeField]
	private float chaseSpeed = 0;
	[SerializeField]
	private float walkSpeed = 0;
	private bool playerIsSpotted = false;
	private GameObject playerRef;


	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		audioSource = GetComponent<AudioSource>();
		randomDirection = new Vector3(transform.position.x, transform.position.y);
		target = targets[index];
		playerRef = GameObject.FindGameObjectWithTag("Player");
		prevPos = transform.position;
		StartCoroutine(FieldOfViewRoutine());
	}

	// Update is called once per frame
	void Update()
	{
		if (!playerIsSpotted)
		{
			agent.SetDestination(target.transform.position);

			if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= distance)
			{
				NextPoint();
			}

		}
		else
		{
			agent.SetDestination(target.transform.position);
		}

		DrawRaycastToggle();
		WalkSoundEffect();

	}

	void NextPoint()
	{
			index++;
			if (index >= targets.Count)
			{
				index = 0;
			}
			target = targets[index];
	}

	private IEnumerator FieldOfViewRoutine()
	{
		float delay = 0.3f;
		WaitForSeconds wait = new WaitForSeconds(delay);


		while (true)
		{
			yield return wait;
			FieldOfViewCheck();
		}

	}

	private void FieldOfViewCheck()
	{
		Collider[] rangeChecks = Physics.OverlapSphere(transform.position, fovRadius, layermask);
		
		if (rangeChecks.Length != 0 )
		{
			Transform target = rangeChecks[0].transform;
			Vector3 targetDirection = (target.position - transform.position).normalized;
			if(Vector3.Angle(transform.forward, targetDirection) < viewAngle /2)
			{
				float distanceToTarget = Vector3.Distance(transform.position, target.position);
			
				
				if(!Physics.Raycast(transform.position, targetDirection, distanceToTarget, 6))
				{
					playerIsSpotted = true;
					this.target = rangeChecks[0].gameObject;
					agent.speed = chaseSpeed;
				}
				else
				{
					agent.speed = walkSpeed;
					playerIsSpotted = false;
				}
			}
			else
			{
				playerIsSpotted = false;
			}
		}
		else if(playerIsSpotted)
		{
			playerIsSpotted = false;
		}
	}

	private void DrawRaycastToggle()
	{
		Vector3 forward = transform.TransformDirection(Vector3.forward) * fovRadius;
		Debug.DrawRay(transform.position, forward, Color.red);
	}

	private void WalkSoundEffect()
	{
		float moveDist = Vector3.Distance(transform.position, playerRef.transform.position);

		if(moveDist >= minimumSoundDistance)
		{
			audioSource.Play();

		}
		else if(!transform.hasChanged)
		{
			audioSource.Stop();
		}
	}

	//These two are for more randomised patrol spots, not used yet
	Vector3 Patrol(Vector3 origin, float distance, int layermask)
	{
		randomDirection += origin;

		NavMeshHit navHit;
		NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

		return navHit.position;
	}


	void MoveToSpot(Vector3 destination)
	{

		float dist = Vector3.Distance(destination, transform.position);

		if (destination != Vector3.zero)
		{

			agent.SetDestination(destination);
		}
		if (dist < 10)
		{
			Patrol(gameObject.transform.position, 10, 1);
		}
	}
}
