using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    List<GameObject> enemiesToSpawn = new List<GameObject>();
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Transform enemiesParent;
    [SerializeField] Transform player;
    [SerializeField] TMP_Text waveText;
    [SerializeField] float waveCooldown = 5;
    [SerializeField] float cycleColldown = 5;
    List<Transform> spawnPointsList = new List<Transform>();
    int wave = 1;
    int enemyCount = 2;
    int newEnemyIndex = 1;
    public float waveDifficulty = 1f;
    bool waveIsEnd = true;
    private void Start()
    {
        Time.timeScale = 1;
        enemiesToSpawn.Add(enemies[0]);
        waveText.text = "wave: " + wave;
    }
    private void Update()
    {
        if (waveIsEnd) StartCoroutine(Waving());
    }

    IEnumerator Waving()
    {
        Debug.Log("Wave " + wave);
        waveIsEnd = false;
        spawnPointsList.Clear();
        foreach (Transform t in spawnPoints)
        {
            spawnPointsList.Add(t);
        }
        if (wave % 2 == 0)
        {
            enemyCount++;
        }
        if (wave % 7 == 0 && newEnemyIndex < enemies.Length)
        {
            enemiesToSpawn.Add(enemies[newEnemyIndex]);
            newEnemyIndex++;
        }

        if(wave >= 35)
        {
            if(wave % 5 == 0)
            {
                waveDifficulty += 0.2f;
            }
        }

        yield return new WaitForSeconds(waveCooldown);
        transform.position = player.position;
        //if (player.position.x <= 45 && player.position.x >= -45 &&
        //    player.position.y <= 45 && player.position.y >= -45)
        //{
        //}
        //else
        //{

        //}

        for (int i = 0; i < enemyCount; i++)
        {
            int spawnIndex = Random.Range(0, spawnPointsList.Count);
            GameObject enemy = Instantiate(enemies[Random.Range(0, enemiesToSpawn.Count)], enemiesParent);
            Enemy enemyStats = enemy.GetComponent<Enemy>();

            if(wave >= 50)
            {
            enemyStats.health *= waveDifficulty;
            enemyStats.damage *= waveDifficulty;
            enemyStats.fireRate = enemyStats.fireRate + waveDifficulty;
            }
            

            enemy.transform.position = spawnPointsList[spawnIndex].position;
            spawnPointsList.RemoveAt(spawnIndex);
            if (spawnPointsList.Count == 0)
            {
                foreach (Transform t in spawnPoints)
                {
                    spawnPointsList.Add(t);
                }
                yield return new WaitForSeconds(cycleColldown);
                transform.position = player.position;

            }
        }

        yield return new WaitUntil(() => enemiesParent.childCount == 0);
        Debug.Log("wave complete");
        wave++;
        waveText.text = "wave: " + wave;
        waveIsEnd = true;
    }
}
