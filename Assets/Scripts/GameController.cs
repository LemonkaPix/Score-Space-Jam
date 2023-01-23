using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] float waveCooldown = 5;
    [SerializeField] float cycleColldown = 5;
    List<Transform> spawnPointsList = new List<Transform>();
    int wave = 1;
    int enemyCount = 1;
    int newEnemyIndex = 1;
    bool waveIsEnd = true;
    private void Start()
    {
        enemiesToSpawn.Add(enemies[0]);
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
        if (wave % 3 == 0) enemyCount++;
        if (wave % 10 == 0 && newEnemyIndex < enemies.Length)
        {
            enemiesToSpawn.Add(enemies[newEnemyIndex]);
            newEnemyIndex++;
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
        waveIsEnd = true;
    }
}
