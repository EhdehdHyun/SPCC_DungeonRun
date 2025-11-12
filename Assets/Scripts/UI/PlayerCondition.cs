using System;
using UnityEngine;
public interface IDamagable
{
    void TakePhysicalDamage(int damage);
}
public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;

    private void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.curValue == 0)
        {
            health.Subtract(health.passiveValue * Time.deltaTime);
        }
        if (health.curValue == 0)
        {
            Die();
        }

    }

    public void Die()
    {
        Debug.Log("플레이어가 죽음");
    }

    public void Heal(float amout)
    {
        health.Add(amout);
    }

    public void Eat(float amout)
    {
        hunger.Add(amout);
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        //인보크?
    }
}
