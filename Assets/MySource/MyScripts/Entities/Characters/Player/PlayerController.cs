using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    [Header("PlayerController")]
    public PlayerDataSO PlayerDataSO;
    public Rigidbody2D rb;

    public StateMachine PlayerState;
    public PlayerGroundedCheck PlayerGroundedCheck;
    public PlayerWallSlidingCheck PlayerWallSlidingCheck;
    public FacingHandler FacingHandler;

    protected virtual void OnEnable()
    {
        transform.position = CheckPointManager.Instance.CurrentPoint.PositionPoint;
    }

    protected virtual void Start()
    {
        this.PlayerGroundedCheck = new PlayerGroundedCheck(this);
        this.PlayerWallSlidingCheck = new PlayerWallSlidingCheck(this);
        this.FacingHandler = new FacingHandler(transform);

        this.PlayerState = new PlayerStateMachine(this);
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        UntilityHelper.AutoFetchComponent(ref rb, gameObject);
        this.LoadPlayerSO();
    }

    protected void LoadPlayerSO()
    {
        if (this.PlayerDataSO != null) return;
        string path = $"SO/Characters/Players/{transform.name}";
        this.PlayerDataSO = Resources.Load<PlayerDataSO>(path);
        Debug.LogWarning($"{transform.name}: LoadPlayerSO", gameObject);
    }

    protected virtual void Update()
    {
        this.PlayerState?.ExcuteState();

        if (Input.GetKeyDown(KeyCode.F)) PlayerState.ChangeState(EPlayerState.Hurt);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (this.PlayerState.currStateAction is ITrigger2DState stateTriggerHandle)
        {
            stateTriggerHandle.OnTriggerEnter2D(other);
        }
    }
}
