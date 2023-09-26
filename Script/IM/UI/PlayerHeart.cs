using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHeart : MonoBehaviour
{
    [SerializeField]
    List<Heart> hearts;
    [SerializeField]
    int playerCurhp;
    int updatenum;
    PlayerStateData playerState;

    // Start is called before the first frame update
    void Start()
    {
        playerState = GameObject.Find("Player").GetComponent<PlayerStateData>();
        
        for (int i = 0; i < 8; i++)
        {
            var _heart = transform.GetChild(i).GetComponent<Heart>();
            if (_heart != null)
            {
                hearts.Add(_heart);
            }
            else
            {
                continue;
            }
        }
        StartCoroutine(PlayerHeartUpdate());
    }

    IEnumerator PlayerHeartUpdate()
    {
        while (true)
        {
            yield return null;
            int a = 0;
            int b = 0;
            playerCurhp = playerState.curHp;
            if (playerCurhp != updatenum)
            {


                for (int i = 0; i < hearts.Count; i++)
                {
                    hearts[i].heartUpdate(0);
                }

                //짝수라면
                if (playerCurhp % 2 == 0 && playerCurhp != 0)
                {
                    a = 2;
                    b = (playerCurhp / 2) - 1;
                    for (int i = 0; i < (playerCurhp / 2); i++)
                    {
                        hearts[i].heartUpdate(2);
                    }

                }
                //홀수라면
                else if (playerCurhp % 2 != 0 && playerCurhp != 0)
                {
                    a = 1;
                    b = (playerCurhp / 2);
                    for (int i = 0; i < (playerCurhp / 2) + 1; i++)
                    {
                        hearts[i].heartUpdate(2);
                    }
                }
                //0이라면
                else if (playerCurhp < 1)
                {
                    for (int i = 0; i < hearts.Count; i++)
                    {
                        hearts[i].heartUpdate(0);
                    }
                }


                //마지막 하트를 절반 또는 꽉찬 하트로
                if (playerCurhp > 0)
                {
                    hearts[b].heartUpdate(a);
                    updatenum = playerCurhp;
                }
                updatenum = playerCurhp;
            }


        }
    }

}
