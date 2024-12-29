using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    [Header("PlayerController")]
    public EEnemyCode enemyCode;

    public Rigidbody2D rb;
    public EnemyDataSO EnemyData;
    public EnemyDamageable EnemyDamageable;
    public FacingHandler FacingHandler;


    protected virtual void Start()
    {
        this.FacingHandler = new FacingHandler(transform, -1);
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadRigidbody2D();
        this.LoadEnemyData();
        this.LoadEnemyType();
        UntilityHelper.AutoFetchComponent<EnemyDamageable>(ref this.EnemyDamageable, gameObject);
    }

    private void LoadEnemyType()
    {
        if (this.enemyCode != EEnemyCode.None) return;
        this.enemyCode = UntilityHelper.TryParseEnum<EEnemyCode>(transform.name);
        Debug.LogWarning($"{transform.name}: LoadEnemyType", gameObject);
    }

    private void LoadRigidbody2D()
    {
        if (this.rb != null) return;
        UntilityHelper.AutoFetchComponent(ref this.rb, gameObject);
        this.rb.angularDrag = 10;
    }

    private void LoadEnemyData()
    {
        if (this.EnemyData != null) return;
        string path = $"SO/Characters/Enemies/{transform.name}";
        this.EnemyData = Resources.Load<EnemyDataSO>(path);
        Debug.LogWarning($"{transform.name}: LoadEnemyData", gameObject);
    }
}
