using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public float spawnTime = 0.1f;
    public GameObject[] enemy;
    public GameObject[] Chefes;
    public Transform[] spawnPoints;
    public int MaximumEnemyInScene = 50;

    GameObject player;
    VidaPlayer VidaPlayer;

    int SpawnPrev = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        VidaPlayer = player.GetComponent<VidaPlayer>();
    }

    void Update()
    {

    }

    public IEnumerator SpawnEnemies(int QtdeInimigo, bool RoundBoss)
    {
        for (int i = 0; i < QtdeInimigo; i++)
        {
            if (VidaPlayer.CurrentHealth > 0)
            {
                int spawnPointIndex = Random.Range(0, (spawnPoints.Length));
                if (spawnPoints.Length > 1)
                {
                    do
                    {
                        spawnPointIndex = Random.Range(0, (spawnPoints.Length));
                    } while (spawnPointIndex == SpawnPrev);
                }
                SpawnPrev = spawnPointIndex;

                if (RoundBoss == true && i == (QtdeInimigo-1))
                {
                    int EnemyIndex = Random.Range(0, (Chefes.Length));
                    Instantiate(Chefes[EnemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                }
                else
                {
                    Debug.Log("aqui");
                    int EnemyIndex = Random.Range(0, (enemy.Length));
                    Instantiate(enemy[EnemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                }

                yield return new WaitForSeconds(spawnTime);
            }
        }
    }
}
