using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    // Start is called before the first frame update
    GameManage gameManage;
    [HeaderAttribute("Player setting")]
    [SerializeField] public const float maxScale = 1.6f;
    [SerializeField] public float maxScore = 200f;
    [SerializeField] const float baseScore=8;
    [SerializeField] float bonus_interval = 9f;
    [Space]
    //UI
    [HeaderAttribute("UI")]
    [SerializeField] Gradient gradient;
    [SerializeField] Slider timeSlider;
    [SerializeField] Slider scoreSlider;
    [SerializeField] Text BonusText;
    [SerializeField] TextMeshPro ScoreText;
    [SerializeField] SoundManage soundManage;
    [SerializeField] Transform player;
    [SerializeField] Image fill;
    [SerializeField] Animator scaleIcon;
    public float score = 0;
    float bonusTime = 0;    
    bool hasBonus = false;
    float scale=1;   
    void Start()
    {
        gameManage = GameObject.Find("GameManager").GetComponent<GameManage>();
        timeSlider.gameObject.SetActive(false);
        BonusText.gameObject.SetActive(false);
        scaleIcon.gameObject.SetActive(false);
        scaleIcon.SetFloat("Scale",scale);
        scoreSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBonus == true)
        {
            timeSlider.gameObject.SetActive(true);
            if (Time.time - bonusTime < bonus_interval)
            {
                timeSlider.value = 1f - ((Time.time - bonusTime) / bonus_interval);
                fill.color = gradient.Evaluate(timeSlider.value);
            }
            else
            {
                timeSlider.gameObject.SetActive(false) ;
                hasBonus = false;
            }
            
        }
    }
    public void setScore(int bonus)
    {
        float scoreChange;
        ScoreText.color = Color.white;
        soundManage.PlaySound("ScoreUp");
        scoreChange = (baseScore + bonus*bonus) * scale;//
        ScoreText.text = "+" + scoreChange.ToString();
        Instantiate(ScoreText, player);
        if(gameManage.gameData.isMultiple) Instantiate(ScoreText, gameManage.player2.transform);
        score += scoreChange;       
        scoreSlider.value += scoreChange / maxScore;
    }
    public void accumulate()//set  accumulate
    {   bonusTime = Time.time;
        hasBonus = true;
        StopCoroutine("Scoresystem");
        StartCoroutine("Scoresystem");
    }
    public void scoreDown()//
    {
        float scoreChange;
        soundManage.PlaySound("ScoreDown");
        scoreChange = 1.7f*baseScore * scale;
        ScoreText.text = "-" + scoreChange.ToString();
        ScoreText.color = Color.red;
        Instantiate(ScoreText, player);
        if (gameManage.gameData.isMultiple) Instantiate(ScoreText, gameManage.player2.transform);
        if (score - scoreChange > 0)
        {
            score -= scoreChange;
            scoreSlider.value -= scoreChange / maxScore;
        }
        else
        {
            score = 0;
            scoreSlider.value = 0;
        }
    }
    public void scoreUp(bool isCompletion)//
    {
        if (isCompletion)
        {
            setScore(4);
        }
        else setScore(0);
    }
    IEnumerator Scoresystem()//bonus accumulate
    {
            if (scale + 0.1f <= maxScale) scale +=0.2f;
            scaleIcon.gameObject.SetActive(true);
            scaleIcon.SetFloat("Scale", scale);
           //BonusText.gameObject.SetActive(true);
            //BonusText.text = "x"+scale.ToString();
            yield return new WaitForSeconds(bonus_interval);
            scaleIcon.gameObject.SetActive(false);
            BonusText.gameObject.SetActive(false);
            hasBonus = false;
            bonusTime = 0f;
            scale = 1f;
    }
}
