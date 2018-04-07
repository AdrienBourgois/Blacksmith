using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{
	void Start ()
	{
	    InputManager.Instance.SubscribeToHorizontalEvent(Horizontal);
	    InputManager.Instance.SubscribeToVerticalEvent(Vertical);
	    InputManager.Instance.SubscribeToJumpReviveEvent(JumpRevive);
	    InputManager.Instance.SubscribeToWeakAttackEvent(WeakAttack);
	    InputManager.Instance.SubscribeToStrongAttackEvent(StrongAttack);
	    InputManager.Instance.SubscribeToSpecialMoveEvent(SpecialMove);
	    InputManager.Instance.SubscribeToFusionEvent(Fusion);
	    InputManager.Instance.SubscribeToPauseEvent(Pause);
    }

    public void Horizontal(float axe)
    {
        print("Horizontal : " + axe);
    }

    public void Vertical(float axe)
    {
        print("Vertical : " + axe);
    }

    public void JumpRevive()
    {
        print("JumpRevive");
    }

    public void WeakAttack()
    {
        print("WeakAttack");
    }

    public void StrongAttack()
    {
        print("StrongAttack");
    }

    public void SpecialMove()
    {
        print("SpecialMove");
    }
    public void Fusion()
    {
        print("Fusion");
    }
    public void Pause()
    {
        print("Pause");
    }
}
