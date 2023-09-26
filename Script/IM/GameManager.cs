using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool playerDie;
    public int bossLive= 0;
    public bool lastboDie = false;
    public EnemyStatData bossData;
    public int es = 0;


    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
        playerDie = false;

       
    }

   
    void Update()
    {
        
    }


}
