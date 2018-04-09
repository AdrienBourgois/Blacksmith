using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{
    public int player_id;

	void Start ()
	{
        if (player_id == 1)
            ListenToP1();
        else
            ListenToP2();
    }

    private void ListenToP1()
    {
        InputManager.Instance.SubscribeToHorizontalP1Event(Horizontal);
        InputManager.Instance.SubscribeToVerticalP1Event(Vertical);
        InputManager.Instance.SubscribeToJumpReviveP1Event(JumpRevive);
        InputManager.Instance.SubscribeToWeakAttackP1Event(WeakAttack);
        InputManager.Instance.SubscribeToStrongAttackP1Event(StrongAttack);
        InputManager.Instance.SubscribeToSpecialMoveP1Event(SpecialMove);
        InputManager.Instance.SubscribeToFusionP1Event(Fusion);
        InputManager.Instance.SubscribeToPauseP1Event(Pause);
    }

    private void ListenToP2()
    {
        InputManager.Instance.SubscribeToHorizontalP2Event(Horizontal);
        InputManager.Instance.SubscribeToVerticalP2Event(Vertical);
        InputManager.Instance.SubscribeToJumpReviveP2Event(JumpRevive);
        InputManager.Instance.SubscribeToWeakAttackP2Event(WeakAttack);
        InputManager.Instance.SubscribeToStrongAttackP2Event(StrongAttack);
        InputManager.Instance.SubscribeToSpecialMoveP2Event(SpecialMove);
        InputManager.Instance.SubscribeToFusionP2Event(Fusion);
        InputManager.Instance.SubscribeToPauseP2Event(Pause);
    }

    public void Horizontal(float axe)
    {
        print(player_id +" Horizontal : " + axe);
    }

    public void Vertical(float axe)
    {
        print(player_id + " Vertical : " + axe);
    }

    public void JumpRevive()
    {
        print(player_id + " JumpRevive");
    }

    public void WeakAttack()
    {
        print(player_id + " WeakAttack");
    }

    public void StrongAttack()
    {
        print(player_id + " StrongAttack");
    }

    public void SpecialMove()
    {
        print(player_id + " SpecialMove");
    }
    public void Fusion()
    {
        print(player_id + " Fusion");
    }
    public void Pause()
    {
        print(player_id + " Pause");
    }
}
