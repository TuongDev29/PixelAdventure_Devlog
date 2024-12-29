using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] protected CinemachineVirtualCamera virtualCamera;
    [SerializeField] protected List<PlayerController> playersCtrl = new List<PlayerController>();
    [SerializeField] protected PlayerController currentPlayer;
    public PlayerController CurrentPlayer => currentPlayer;
    public Vector2 CurrentPosition
    {
        get
        {
            if (currentPlayer != null)
            {
                return currentPlayer.transform.position;
            }

            Debug.LogWarning("CurrentPlayer is not exist");
            return Vector2.zero;
        }

    }

    protected override void Awake()
    {
        base.Awake();
        if (virtualCamera == null) Debug.LogWarning("Cinemachine Virtual Camera is not assigned or cannot be found!");

        foreach (PlayerController playerPrefab in this.playersCtrl)
        {
            playerPrefab.gameObject.SetActive(false);
        }

        if (this.currentPlayer != null) this.ChangeToOtherPlayer(this.currentPlayer);
        else ChosePlayer("NinjaFrog");
    }

    public Transform GetCurrentPlayer()
    {
        if (this.currentPlayer == null)
        {
            Debug.LogWarning("Current player is null");
            return null;
        }
        return this.currentPlayer.transform;
    }

    public void AddPlayer(PlayerController playerController)
    {
        if (playersCtrl.Contains(playerController)) return;
        this.playersCtrl.Add(playerController);
    }

    public void ChosePlayer(int index)
    {
        if (index > playersCtrl.Count) return;

        this.ChangeToOtherPlayer(playersCtrl[index]);
    }

    public void ChosePlayer(string playerName)
    {
        foreach (PlayerController player in playersCtrl)
        {
            if (!player.name.Contains(playerName)) continue;
            this.ChangeToOtherPlayer(player);
            return;
        }

        Debug.LogWarning("Cannot found " + playerName);
    }

    private void ChangeToOtherPlayer(PlayerController player)
    {
        if (this.currentPlayer != null) this.currentPlayer.gameObject.SetActive(false);
        if (this.virtualCamera != null) this.virtualCamera.Follow = player.transform;

        this.currentPlayer = player;
        this.currentPlayer.gameObject.SetActive(true);
    }
}
