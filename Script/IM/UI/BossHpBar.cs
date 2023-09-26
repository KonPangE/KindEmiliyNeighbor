using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    public EnemyStatData date;
    [SerializeField]
    Slider hpBar;
    [SerializeField]
    float maxHp = 80;
    [SerializeField]
    float curHp = 80;
    [SerializeField]
    Text nameText;

    private void OnEnable()
    {
        Start();
    }

    // Start is called before the first frame update
    void Start()
    {
        date = GameManager.Instance.bossData;

        hpBar = GetComponent<Slider>();
        maxHp = date.maxHp;
        curHp = date.curHp;
        nameText = GetComponentInChildren<Text>();
        nameText.text = date.name;
        hpBar.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.bossLive > 0)
        {
            curHp = date.curHp;
            hpBar.value = (curHp / maxHp) % 100;
            if (curHp < 0)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    /*IEnumerator HpBar()
    {
        while (enemyDate != null)
        {

        }
    }*/

}
