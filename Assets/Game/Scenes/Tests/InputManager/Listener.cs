using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{
	void Start ()
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
