using UnityEngine;

public class BulletScript : MonoBehaviour
{

	[Header("Attributes")]
	public float speed;
	public int damage;

	private Transform target;

	public void Seek(Transform _target)
	{
		target = _target;
	}

	void Update()
	{

		if (target == null)
		{
			gameObject.SetActive(false);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);


	}

	void HitTarget()
	{
		Damage(target);
		gameObject.SetActive(false);
	}

	void Damage(Transform enemy)
	{
		EnemyScript e = enemy.GetComponent<EnemyScript>();

		if (e != null)
		{
			e.TakeDamage(damage);
		}
	}

}
