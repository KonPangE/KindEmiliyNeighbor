using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBossBulletAttack : MonoBehaviour
{
    public GameObject bullet;
    LastBossAI lastBoss;
    public int bulletAttack = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        lastBoss = GetComponent<LastBossAI>();
        StartCoroutine(Attack2());
        
        

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Attack2()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (lastBoss.state == LastBossAI.State.ATTACK2 && lastBoss.phase > 1 && bulletAttack < 1)
            {
                bulletAttack = 1;
                yield return new WaitForSeconds(2f);
                for (int i = 0; i < 10; i++)
                {
                    Vector2 spawnPoint = SpawnPoint();
                    GameObject a = Instantiate(bullet, spawnPoint, Quaternion.identity);
                    yield return new WaitForSeconds(0.01f);
                    var _a = a.GetComponent<Bullet>();
                    _a.FirePos(lastBoss.playerTr.position);
                }
                //Points.Clear();

            }
        }
    }


    Vector2 SpawnPoint()
    {
        Vector2 originPoint = transform.position;
        Vector2 randomPoint;

        float posX = originPoint.x + Random.Range(-30, 30);
        float posY = originPoint.y + Random.Range(0, 10);

        randomPoint = new Vector2(posX, posY);
        return randomPoint;
        
    }

}
