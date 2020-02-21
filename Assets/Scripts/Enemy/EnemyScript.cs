using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public EnemySO mEnemySO;

	[Header("Attributes")]
	public float speed;
	public int worth;

	[Header("UI references")]
	public Image healthBarFillImage;

	[Header("Manager references")]
	public UIManager uiManager;

	private float health;
	private bool isDead = false;

	private void Start()
	{
		// Setup start values
		speed = mEnemySO.speed;
		health = mEnemySO.startHealth;
		worth = Random.Range(mEnemySO.minCoinReward, mEnemySO.maxCoinReward);
	}


	public void TakeDamage(float amount)
	{
		health -= amount;

		float progress = Mathf.InverseLerp(0, mEnemySO.startHealth, health);
		healthBarFillImage.fillAmount = Mathf.Lerp(0, 1, progress);

		if (health <= 0 && !isDead)
		{
			Die();
		}
	}


	void Die()
	{
		isDead = true;

		PlayerStats.Coins += worth;
		WaveSpawner.EnemiesAlive--;
		uiManager.UpdateUI();

		Destroy(gameObject);
	}
}
