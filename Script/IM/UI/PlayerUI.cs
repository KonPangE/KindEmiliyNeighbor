using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    PlayerData playerData;
    [SerializeField]
    public Slot[] slots;
    
    


    // Start is called before the first frame update
    void Start()
    {
        slots = GetComponentsInChildren<Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RemoveItem(int num)
    {
        slots[num].RemoveItem();
    }



}
