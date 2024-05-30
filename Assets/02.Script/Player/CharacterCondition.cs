using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterCondition : MonoBehaviour
{
    public UICondition uiCondition;

    public Condition health { get { return uiCondition.health; } }
    public Condition hunger { get { return uiCondition.hunger; } }
    public Condition stamina { get { return uiCondition.stamina; } }
    public Condition mana { get { return uiCondition.mana; } }

    public float noHungerHealthDecay;
    public event Action onTakeDamage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);
        mana.Add(mana.passiveValue * Time.deltaTime);

        if (hunger.curValue <= 0.0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (health.curValue <= 0.0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        StartCoroutine(IncreseStat(hunger, amount));
    }

    public void Die()
    {
        Debug.Log("플레이어가 죽었다.");
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke();
    }

    IEnumerator IncreseStat(Condition condition,float amount)
    {
        while (amount > 0)
        {
            condition.Add(amount / 5f);
            amount -= amount / 5f;
            yield return new WaitForSeconds(1.0f);
        }
    }
}
