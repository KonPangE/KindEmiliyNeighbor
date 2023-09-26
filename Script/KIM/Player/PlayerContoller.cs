using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Collidertouch), typeof(Damageable))]
public class PlayerContoller : MonoBehaviour
{
    #region 변수
    bool ismove { get; set; }
    bool isright;
    bool isdash = false;
    float dashtime = 0.8f;
    public int direction { get; private set; }
    public bool canMove { get { return animator.GetBool("canMove"); } }
    public bool Ishit { get { return animator.GetBool("ishit"); } private set { animator.SetBool("ishit", value); } }
    Vector2 moveinput;
    Vector2 dashinput;
    #endregion 
    #region 컴포넌트
    Rigidbody2D rb;
    Animator animator;
    Collidertouch collidertouch;
    Damageable damageable;
    PlayerStateData playerstatedata;
    public PlayerUI playerUI;
    #endregion
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collidertouch = GetComponent<Collidertouch>();
        damageable = GetComponent<Damageable>();
        playerstatedata = GetComponent<PlayerStateData>();
        direction = 1;

    }
    private void Update()
    {
        if (ismove)
        {
            animator.SetBool("move", true);
            animator.SetBool("idle", false);
        }
        else
        {
            animator.SetBool("move", false);
            animator.SetBool("idle", true);

        }

    }
    private void FixedUpdate()
    {
        if (!damageable.Ishit)
        {
            rb.velocity = new Vector2(moveinput.x * playerstatedata.speed, rb.velocity.y);
            animator.SetFloat("yvelocity", rb.velocity.y);

        }
        if (isdash)
        {
            rb.velocity = new Vector2(dashinput.x * playerstatedata.dashforce, 0);
        }


    }

    public void Move(InputAction.CallbackContext context)
    {
        if (damageable.islive)
        {
            moveinput = context.ReadValue<Vector2>();
            ismove = moveinput != Vector2.zero;
            Checkflip();
        }
        else
        {
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            ismove = false;

        }

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && collidertouch.isgrounded)
        {
            animator.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, playerstatedata.jumpForce);
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger("attack");
        }

    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.started && !isdash && collidertouch.isgrounded)
        {
            Debug.Log("dash");
            isdash = true;
            animator.SetTrigger("dash");
            damageable.isinvincible = true;

            if (transform.rotation.y > -1)
            {
                dashinput = new Vector2(1, 0);
            }
            else
            {
                dashinput = new Vector2(-1, 0);
            }

            StartCoroutine(StopDash());
            return;
        }
    }

    public void UseItem1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ItemUse(0, playerUI.slots[0].item);
        }
    }
    public void UseItem2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ItemUse(1, playerUI.slots[1].item);
        }
    }



    IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashtime);
        isdash = false;
        damageable.isinvincible = false;
    }

    public void OnDamage(int damage, Vector2 knockback)
    {
        Ishit = true;
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    void Checkflip()
    {
        if (isright && moveinput.x > 0)
        {
            Flip();
        }
        else if (!isright && moveinput.x < 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        isright = !isright;
        rb.transform.Rotate(0, 180, 0);
    }

    public void ItemUse(int num, Item _item)
    {
        if (playerUI.slots[num].item == null)
            return;
        if (_item.itemType == Item.ItemType.UseItem)
        {
            if (playerUI.slots[num].item != null)
            {
                playerstatedata.StatUp(_item);
                playerUI.RemoveItem(num);
            }
            if (playerUI.slots[num].item != null)
            {
                playerstatedata.StatUp(_item);
                playerUI.RemoveItem(num);
            }
        }

    }



}
