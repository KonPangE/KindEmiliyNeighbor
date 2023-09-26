using UnityEngine;

public class Collidertouch : MonoBehaviour
{
    //캐릭터가 벽이나 땅 등에 닿고있는지 확인함

    CapsuleCollider2D capsuleCollider;
    RaycastHit2D[] groundhit = new RaycastHit2D[3];
    Animator animator;

    public float grounddistance = 0.05f;
    public ContactFilter2D castfilter;
    public bool isgrounded { get { return isGround; } set { isGround = value; animator.SetBool("isground", value); } }

    [SerializeField]
    private bool isGround;
    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();

    }

    void FixedUpdate()
    {
        //땅인지 확인, cast가 다른물리충돌 함수와 같이 동시에 실행되어야하기때문에 fixedupdate 사용
        isgrounded = capsuleCollider.Cast(Vector2.down, castfilter, groundhit, grounddistance) > 0;
    }


}
