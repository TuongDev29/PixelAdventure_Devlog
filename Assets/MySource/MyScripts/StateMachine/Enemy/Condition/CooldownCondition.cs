using UnityEngine;

public class CooldownCondition : ICondition
{
    private readonly float cooldownTime;
    private float timer;

    public CooldownCondition(float cooldownTime)
    {
        this.cooldownTime = cooldownTime;
    }

    public void Enter()
    {
        this.timer = this.cooldownTime;
    }
    public bool Condition()
    {
        this.timer -= Time.deltaTime;

        return this.timer <= 0;
    }
}