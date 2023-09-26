using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject boss;
    [SerializeField]
    GameObject bossHpBar;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.bossData = boss.GetComponent<EnemyStatData>();
            GameManager.Instance.bossLive = 1;
            bossHpBar.SetActive(true);
            Destroy(this.gameObject);
        }
    }

}
