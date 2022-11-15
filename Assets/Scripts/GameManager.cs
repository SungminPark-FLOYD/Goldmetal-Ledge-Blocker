using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    static GameManager instance;

    [Header("Time")]
    public static float maxTime = 100f;
    public static float playTime;
    public static float maxComboTime = 3f;
    public static float playComboTime;
    public static bool isGameOver = true;

    [Header("Player")]
    public static float score;
    public static int hit;
    public static int combo;

    [Header("UI")]
    public GameObject uiInfo;
    public GameObject uiSelect;
    public GameObject uiGameOver;
    public GameObject uiGameStart;
    public GameObject uiNewRecord;

    [Header("Effect")]
    public ParticleSystem particle;
    public Animator animation;




    void Start()
    {
        Application.targetFrameRate = 60;

        Init();
    }

     void Init()
    {
        instance = this;
        playTime = 0;
        playComboTime = 0;
        isGameOver = true;
        score = 0;
        hit = 0;
        combo = 0;

        if(PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetFloat("Score", 0f);
        }
    }

    public static void Success()
    {
        hit++;
        combo++;
        score += 1 + (combo * 0.1f);
        playComboTime = 0;

        instance.particle.Play();
        instance.animation.SetTrigger("Hit");
        SoundManager.PlaySound("Hit");
    }

    public static void Fail()
    {
        playTime += 10f;
        combo = 0;
        SoundManager.PlaySound("Fail");
    }

    IEnumerator ComboTime()
    {
        while(!isGameOver)
        {
            yield return null;
            playComboTime += Time.deltaTime;

            if (playComboTime > maxComboTime)
            {
                combo = 0;
            }
        }
        
       
        
    }
    void Update()
    {
        if (isGameOver)
            return;

        GmaeTimer();
    }

    void GmaeTimer()
    {
        playTime += Time.deltaTime;

        if(playTime > maxTime)
        {
            GameOver();
        }
    }

    public void GameStart()
    {
        isGameOver = false;
        uiInfo.SetActive(true);
        uiSelect.SetActive(true);
        uiGameStart.SetActive(false);
        instance.StartCoroutine(instance.ComboTime());
        SoundManager.PlaySound("Start");
        SoundManager.BgmStart();
    }

    void GameOver()
    {
        isGameOver = true;
        uiInfo.SetActive(false);
        uiSelect.SetActive(false);
        uiGameOver.SetActive(true);

        float maxScore = PlayerPrefs.GetFloat("Score");
        if(score > maxScore)
        {
            PlayerPrefs.SetFloat("Score", score);
            uiNewRecord.SetActive(true);
        }
        SoundManager.PlaySound("Over");
        SoundManager.BgmStop();

    }

    public void Retry()
    {
        SceneManager.LoadScene("Main");
    }
}
