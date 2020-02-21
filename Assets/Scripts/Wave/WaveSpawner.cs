using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class WaveSpawner : MonoBehaviour
{

	public static int EnemiesAlive = 0;

	public WavesSOHolder waveSOHolder;

	public Transform spawnPoint;
	public Transform enemiesHolder;

	[Header("UI elements")]
	public TextMeshProUGUI waveText;
	public Image waveFillImage;

	[Header("Managers references")]
	public GameManager gameManager;
	public UIManager uiManager;

	[Header("Variables")]
	public float startCoundown = 5f;

	private float countdown;
	private int waveIndex = 0;

	private void Start()
	{
		countdown = startCoundown;
		waveText.text = $"Wave: Preparing";
	}

	void Update()
	{
		if (GameManager.isGame)
			GamePlay();
	}

	private void GamePlay()
	{

		if (waveIndex == waveSOHolder.Objects.Count && EnemiesAlive == 0)
		{
			gameManager.WinLevel();
			this.enabled = false;
		}

		if (waveIndex < waveSOHolder.Objects.Count)
		{

			if (countdown <= 0f)
			{
				StartCoroutine(SpawnWave());
				return;
			}

			countdown -= Time.deltaTime;

			float progg = 0;

			if(waveIndex == 0)
				progg = Mathf.InverseLerp(startCoundown, 0, countdown);
			else
				progg = Mathf.InverseLerp(waveSOHolder.Objects[waveIndex - 1].duration, 0, countdown);

			waveFillImage.fillAmount = Mathf.Lerp(1, 0, progg);
		}
		else
		{
			waveFillImage.fillAmount = 0;
		}
	}

	private IEnumerator SpawnWave()
	{
		PlayerStats.Wave++;
		waveText.text = $"Wave: {PlayerStats.Wave}/{waveSOHolder.Objects.Count}";
		WaveSO wave = waveSOHolder.Objects[waveIndex];

		EnemiesAlive += wave.count;
		
		countdown = waveSOHolder.Objects[waveIndex].duration;
		waveIndex++;

		for (int i = 0; i < wave.count; i++)
		{
			SpawnEnemy(wave.enemySO);
			yield return new WaitForSeconds(1f / wave.spawnRate);
		}


	}

	private void SpawnEnemy(EnemySO enemy)
	{
		EnemyScript enemyScript = Instantiate(enemy.enemyPrefab, spawnPoint.position, spawnPoint.rotation,  enemiesHolder).GetComponent<EnemyScript>();
		enemyScript.uiManager = uiManager;
		enemyScript.mEnemySO = enemy;
	}
}