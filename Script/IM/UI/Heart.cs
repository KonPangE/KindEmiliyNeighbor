using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    [SerializeField]
    Sprite[] heartSprites;
    Image heartImage;


    public void heartUpdate(int num)
    {
        heartImage.sprite = heartSprites[num];
    }

    // Start is called before the first frame update
    void Start()
    {
        heartImage = GetComponent<Image>();
        //heartImage.sprite = heartSprites[0];
    }

    
}
