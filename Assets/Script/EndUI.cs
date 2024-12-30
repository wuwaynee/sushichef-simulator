using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndUI : MonoBehaviour
{
    // Start is called before the first frame update
    GameManage gameManage;
    [SerializeField] TextMeshProUGUI statusText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI assemblyText;
    [SerializeField] TextMeshProUGUI sucessText;
    [SerializeField] GameObject PauseUI;
    void Start()
    {
        gameManage = GameObject.Find("GameManager").GetComponent<GameManage>();
        if (gameManage.scoreSystem.score >= gameManage.scoreSystem.maxScore)
        {
            statusText.SetText("YOU WIN!");
        }else
        {
            statusText.SetText("GAME END!");
            statusText.color = Color.red;
        }
        
        scoreText.SetText("Total Score :\n"+gameManage.scoreSystem.score.ToString());
        assemblyText.SetText("Sushi Made :\n" + gameManage.assemblyCount.ToString());
        sucessText.SetText("Food Delivered:\n" + gameManage.sucessCount.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void Pause(bool pause)
    {
        PauseUI.SetActive(pause);
        Time.timeScale = pause ? 0f : 1f;
    }
}
