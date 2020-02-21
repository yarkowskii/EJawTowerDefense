using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyScript))]
public class EnemyMovement : MonoBehaviour
{

	private Transform target;
	private int waypointIndex = 0;

	private EnemyScript enemy;

	void Start()
	{
		enemy = GetComponent<EnemyScript>();

		target = Waypoints.points[0];
	}

	void Update()
	{
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, target.position) <= 0.2f)
		{
			GetNextWaypoint();
		}

	}

	void GetNextWaypoint()
	{
		if (waypointIndex >= Waypoints.points.Count - 1)
		{
			EndPath();
			return;
		}

		waypointIndex++;
		target = Waypoints.points[waypointIndex];
	}

	void EndPath()
	{
		if(GameManager.isGame)
			PlayerStats.Hp -= enemy.mEnemySO.damage;
		
		WaveSpawner.EnemiesAlive--;
		enemy.uiManager.UpdateUI();
		Destroy(gameObject);
	}

}
