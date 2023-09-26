using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject gameover;
    public GameObject damageText;//데미지 텍스트
    public GameObject endgame;
    public GameObject menu;
    public Canvas canvas;

    public GameObject fade;
    Image fadeImage;
    float fadevalue = 0;


    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
        fadeImage = fade.GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(endFadeout());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Menu();
        }
    }

    //현재 씬 재시작
    public void ReStartButton()
    {
        StartCoroutine(FadeOut(1));
    }

    //타이틀 씬으로
    public void TitleButton()
    {
        StartCoroutine(FadeOut(0));

    }

    //게임 종료
    public void ExitButton()
    {
        StartCoroutine(EixtFadeOut());
    }

    public void GameOver()
    {
        gameover.SetActive(true);
    }

    public void EndGame()
    {

        StartCoroutine(endFadeout());
    }

    public void Menu()
    {
        if (GameManager.Instance.es < 1 && !GameManager.Instance.lastboDie)
        {
            menu.SetActive(true);
            Time.timeScale = 0;
            GameManager.Instance.es++;
        }
        else
        {
            menu.SetActive(false);
            Time.timeScale = 1;
            GameManager.Instance.es = 0;
        }
    }

    public void TakeDamage(GameObject obj, int damage)
    {
        //데미지 텍스트 생성
        Vector3 position = Camera.main.WorldToScreenPoint(obj.transform.position);

        TMP_Text text = Instantiate(damageText, position, Quaternion.identity, canvas.transform).GetComponent<TMP_Text>();
        text.text = damage.ToString();

    }

    private void OnEnable()
    {
        TextEvent.Damaged += TakeDamage;
    }
    private void OnDisable()
    {
        TextEvent.Damaged -= TakeDamage;
    }





    IEnumerator endFadeout()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(5f);
        float offset = 1;
        fadevalue = 0;
        fadeImage.color = new Color(0, 0, 0, 0);
        fade.SetActive(true);
        while (offset > fadevalue)
        {
            yield return new WaitForSeconds(0.1f);
            fadevalue += 0.1f;
            fadeImage.color = new Color(0, 0, 0, fadevalue);
        }
        yield return new WaitForSeconds(0.1f);
        endgame.SetActive(true);
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(0);


    }

    IEnumerator FadeOut(int num)
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.1f);
        float offset = 1;
        fadevalue = 0;
        fadeImage.color = new Color(0, 0, 0, 0);
        fade.SetActive(true);
        while (offset > fadevalue)
        {
            yield return new WaitForSeconds(0.1f);
            fadevalue += 0.1f;
            fadeImage.color = new Color(0, 0, 0, fadevalue);
        }
        SceneManager.LoadScene(num);
    }


    IEnumerator EixtFadeOut()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.1f);
        float offset = 1;
        fadevalue = 0;
        fadeImage.color = new Color(0, 0, 0, 0);
        fade.SetActive(true);
        while (offset > fadevalue)
        {
            yield return new WaitForSeconds(0.1f);
            fadevalue += 0.1f;
            fadeImage.color = new Color(0, 0, 0, fadevalue);
        }
        Application.Quit();
    }


}
