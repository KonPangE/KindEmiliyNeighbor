using UnityEngine;

public class Collidertouch : MonoBehaviour
{
    //ĳ���Ͱ� ���̳� �� � ����ִ��� Ȯ����

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
        //������ Ȯ��, cast�� �ٸ������浹 �Լ��� ���� ���ÿ� ����Ǿ���ϱ⶧���� fixedupdate ���
        isgrounded = capsuleCollider.Cast(Vector2.down, castfilter, groundhit, grounddistance) > 0;
    }


}
