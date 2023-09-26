using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateData : MonoBehaviour
{
    public PlayerData playerData;
    Damageable damageable;
    UIManager uiManager;

    public float speed;
    public float jumpForce;
    public int jumpamount;
    public float jumptime;
    public float jumpheight;
    public int damage;
    public float dashforce;
    public float attackspeed;
    public int Maxhealth;
    public int curHp;

    public Vector2 knockback = Vector2.zero;
    public Item _item;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        damageable  = GetComponent<Damageable>();
        uiManager = FindObjectOfType<UIManager>();
        animator = GetComponent<Animator>();
        speed = playerData.speed;
        jumpForce = playerData.jumpForce;
        jumpamount = playerData.jumpamount;
        jumpheight = playerData.jumpheight;
        damage = playerData.damage;
        dashforce = playerData.dashForce;
        Maxhealth = playerData.Maxhealth;
        curHp = Maxhealth;
        knockback = playerData.knockback;
        attackspeed = playerData.attackSpped;
        animator.SetFloat("attackspeed", attackspeed);
    }
  
    public void StatUp(Item stat)
    {
        if(stat.itemIndex == 8)
        {
            WhitePotion();
            return;
        }

        if(stat.itemIndex == 9)
        {
            GlassCanon();
            return;
        }
        speed += stat.moveSpeed;
        damage += stat.damage;
        jumpForce += stat.jumpForce;
        if (curHp + stat.maxHealth <= 0)
        {
            curHp = 0;
            damageable.islive = false;
            animator.SetBool("islive", false);
            uiManager.GameOver();
        }
        else if(curHp + stat.maxHealth < 16)
        {
            curHp += stat.maxHealth;        
        }
        else
        {
            curHp = 16;
        }

        dashforce += stat.dashforce;
        if(attackspeed + stat.attackSpeed < 0.1f)
        {
            attackspeed = 0.1f;
        }
        else
        {
            attackspeed += stat.attackSpeed; 
        }
        animator.SetFloat("attackspeed", attackspeed);

    }

    void WhitePotion()
    {
        speed = playerData.speed;
        jumpForce = playerData.jumpForce;
        damage = playerData.damage;
        dashforce = playerData.dashForce;
        curHp = Maxhealth;
        attackspeed = playerData.attackSpped;
        animator.SetFloat("attackspeed", attackspeed);
    }

    void GlassCanon()
    {
        damage = 20;
        curHp = 2;
    }

}
