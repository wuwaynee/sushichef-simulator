using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Commission : MonoBehaviour
{
    GameManage gameManage;
    [HeaderAttribute("Player setting")]
    [SerializeField] float interval = 13.5f;
    [Space]   
    [SerializeField] Animator animator;
    [SerializeField] Slider slider;
    [SerializeField] BoundCreate boundCreate;
    ScoreSystem scoreSystem; 
    static Vector3[] bound;
    ItemClass.Type commission;
    bool isFinish=false;
    bool enable;
    static int length;
    void Start()
    {
        gameManage = GameObject.Find("GameManager").GetComponent<GameManage>();
        scoreSystem = gameManage.scoreSystem;
        enable =GetComponent<SpriteRenderer>().enabled;
        bound = boundCreate.bound;
        length = bound.Length;
        animator.gameObject.SetActive(false);
        StartCoroutine("commisionStart");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void typeChanged()
    {
        animator.gameObject.SetActive(true);
        switch (commission)
        {
            case ItemClass.Type.Box: animator.SetTrigger("Box"); break;
            case ItemClass.Type.Bonus: animator.SetTrigger("Bonus"); break;
            case ItemClass.Type.Cover: animator.SetTrigger("Cover"); break;
            case ItemClass.Type.Combination: animator.SetTrigger("Combination"); break;
            case ItemClass.Type.Completion:animator.SetTrigger("Completion");break;
        }
    }
    public bool Submit(ItemClass.Type type)
    {
        if (isFinish==false&&type == commission)
        {
            gameManage.sucessCount++;
            isFinish = true;
            enabled = false;
            scoreSystem.scoreUp((type ==ItemClass.Type.Completion));
            animator.gameObject.SetActive(false);
            slider.value = 0;
            return true;
        }
        return false;
    }
    IEnumerator commisionStart()
    {
        while (true)
        {
            transform.position = bound[Random.Range(0, length)];
            commission =(ItemClass.Type)Random.Range(0, 5);
            typeChanged();
            print(commission);
            for (float i = 0; i < interval; i+=0.1f)
            {
                if(isFinish==false) slider.value = i / interval;
                else slider.value = 0;
                yield return new WaitForSeconds(0.1f);
            }
            slider.value = 0;           
            enable = true;
            if (isFinish == false) scoreSystem.scoreDown();
            isFinish = false;
        }
    }
}
