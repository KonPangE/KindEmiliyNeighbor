using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TextEvent
{
    //데미지와 공격당한 오브젝트 표시
    public static UnityAction<GameObject, int> Damaged;
}
