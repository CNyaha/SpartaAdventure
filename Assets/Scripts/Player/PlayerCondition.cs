using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakePhysicalDamage(int damage);
    void GetHeal(int healAmont);
}

public class PlayerCondition : MonoBehaviour, IDamageable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public event Action onTakeDamage;
    public event Action onHealing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health.Add(health.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (health.curValue <= 0)
        {
            // 플레이어를 죽이거나 잠시 못움직이는 로직 만들기
        }

    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtrect(damage);
        onTakeDamage?.Invoke();
    }

    public void GetHeal(int healAmont)
    {
        health.Add(healAmont);
        onHealing?.Invoke();
    }

}
