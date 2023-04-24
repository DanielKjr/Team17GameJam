using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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




	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		randomDirection = new Vector3(transform.position.x, transform.position.y);
		target = targets[index];
	}

	// Update is called once per frame
	void Update()
	{
		agent.SetDestination(target.transform.position);

		if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= distance)
		{
			NextPoint();
		}

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
