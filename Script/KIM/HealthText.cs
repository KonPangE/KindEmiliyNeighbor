using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    //텍스트 속도
    public Vector3 movespeed = new Vector3(0,75,0);
    public float Fadetime = 1f;

    RectTransform rectTransform;
    TextMeshProUGUI textMeshProUGUI;

    float fadingtime = 0f;
    Color fadestartcolor;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        fadestartcolor = textMeshProUGUI.color;
    }

    private void Update()
    {
        rectTransform.position += movespeed * Time.deltaTime;

        fadingtime += Time.deltaTime;

        if( fadingtime < Fadetime)
        {
            float alpha = fadestartcolor.a * (1-(fadingtime/Fadetime));
            textMeshProUGUI.color = new Color(fadestartcolor.r,fadestartcolor.g,fadestartcolor.b,alpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
