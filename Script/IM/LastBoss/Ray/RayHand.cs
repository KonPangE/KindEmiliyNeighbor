using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayHand : MonoBehaviour
{
    Animator anim;
    //EdgeCollider2D coll;
    GameObject ray;
    [SerializeField]
    EnemyStatData data;
    //readonly int hashLHandSpwan = Animator.StringToHash("Spwan");
    readonly int hashLHandAttack = Animator.StringToHash("Attack");

    private void Awake()
    {
        data = GetComponentInParent<EnemyStatData>();
        anim = GetComponent<Animator>();
        ray = transform.GetChild(0).gameObject;
        //ray.SetActive(false);
        this.gameObject.SetActive(false);

    }
    private void OnEnable()
    {
        ray.SetActive(false);
        if (data.pahse > -1)
        StartCoroutine(HandON());

    }

    // Start is called before the first frame update
    void Start()
    {
        /*anim = GetComponent<Animator>();
        ray = transform.GetChild(0).gameObject;
        ray.SetActive(false);*/
        //coll = transform.GetChild(0).GetComponent<EdgeCollider2D>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator HandON()
    {
        yield return null;
        //anim.SetTrigger(hashLHandSpwan);
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger(hashLHandAttack);
        yield return new WaitForSeconds(0.6f);
        ray.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        ray.SetActive(false);
        yield return new WaitForSeconds(0.4f);
        this.gameObject.SetActive(false);

    }


}
