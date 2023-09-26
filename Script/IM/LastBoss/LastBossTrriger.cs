using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBossTrriger : MonoBehaviour
{
    EnemyStatData enemyDate;
    [SerializeField]
    LastBossAI lastBoss;

    [SerializeField]
    GameObject bossCam;
    [SerializeField]
    GameObject sub1;
    [SerializeField]
    GameObject sub2;

    void Start()
    {
        enemyDate = GameObject.Find("LastBossModel").GetComponent<EnemyStatData >();
        lastBoss = enemyDate.gameObject.GetComponent<LastBossAI>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyDate != null)
        {
            if (collision.CompareTag("Player"))
            {
                bossCam.SetActive(true);
                enemyDate.pahse = 0;
                lastBoss.playerIn = 1;
                lastBoss.state = LastBossAI.State.PATUNSTART;
                sub1.SetActive(true);
                sub2.SetActive(true);

            }
        }
    }

}
