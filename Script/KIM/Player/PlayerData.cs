using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData",menuName ="Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    
    public float speed = 6f;
    public float jumpForce = 18f;
    public int jumpamount = 1;
    public float jumptime = 0.2f;
    public float jumpheight = 1;
    public int damage = 5;
    public float dashForce = 10;
    public float attackSpped = 1f;
    public Vector2 knockback = Vector2.zero;
    [Header("Health")]
    public int Maxhealth = 16;

}
