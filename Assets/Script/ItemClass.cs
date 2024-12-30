using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClass : MonoBehaviour
{
    // Start is called before the first frame update
    [ColorUsageAttribute(true,true)] [SerializeField] Color color;  
    [SerializeField] Material material;
    [SerializeField] Texture texture;
    public Type type;
    public int bonus = 0;   
    public bool isComplete = false; 
    public enum Type
    {
        Box, Bonus, Cover,Combination, Completion
    };  
    Animator animator;
    const int maxBonus = 3;
    //for item create
    const int setN= 9;
    static int index = 0;
    static Type[] typeSet = {Type.Box,Type.Bonus,Type.Cover, Type.Box, Type.Bonus, Type.Cover,Type.Box, Type.Bonus, Type.Cover };
    void Awake()
    {
        animator = GetComponent<Animator>();     
        type = Create();
        if (type == Type.Bonus) bonus = 1;
        typeChanged();
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void typeChanged()
    {     
        switch (type)
        {
            case Type.Box:animator.SetTrigger("Box");break;
            case Type.Bonus: animator.SetTrigger("Bonus"); break;
            case Type.Cover: animator.SetTrigger("Cover"); break;
            case Type.Combination: animator.SetTrigger("Combination"); break;
            case Type.Completion: {
                    animator.SetTrigger("Completion");
                    /*GetComponent<Renderer>().material.SetTexture("_Emission1", texture);
                    GetComponent<Renderer>().material.SetColor("_Color",color);*/
                } break;
        }
        animator.SetInteger("bonus", bonus);
    }
    private void bonusUp(int item)
    {
        if (bonus + item > maxBonus) bonus = maxBonus;
        else bonus += item;
    }
    public bool Combine(ItemClass item)// type of catch item
    {       
        if ((item.type == Type.Bonus && bonus == maxBonus) || (type == Type.Bonus && item.bonus == maxBonus)) return false;
        //Box and Bonus
        if (
            (item.type == Type.Box && type == Type.Bonus) ||
            (type == Type.Box && item.type == Type.Bonus)
            )
        {
            bonusUp(item.bonus);
            type = Type.Combination;
        }
        // Combination and Bonus      
        else if (
          (item.type == Type.Combination && type == Type.Bonus) ||
          (item.type == Type.Bonus && type == Type.Combination)
          )
        {
            bonusUp(item.bonus);
            type = Type.Combination;
        }
        // Combination and Cover
        else if (
          (item.type == Type.Cover && type == Type.Combination)||
          (item.type == Type.Combination && type == Type.Cover)
          )
        {
            type = Type.Completion;
            isComplete = true; 
        }
        // Bonus Combine
        else if (item.type == Type.Bonus && type == Type.Bonus)
        {
            bonusUp(item.bonus);
        }
        else {
            return false;
        }
        typeChanged();
        return true;
    }
    static Type Create()
    {
        if (index == 0)
        {
            Type temp;
            for(int i = setN-1,j; i >= 0; i--)
            {               
                j = Random.Range(0, setN);
                temp = typeSet[i];
                typeSet[i] = typeSet[j];
                typeSet[j] = temp;
            }
        }       
        return typeSet[index = (index + 1) % setN];      
    }
}
