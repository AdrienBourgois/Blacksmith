using UnityEngine;
using UnityEngine.Assertions;

public class InputManager : MonoBehaviour
{
    public delegate void AxisCallback(float axe);
    public delegate void GamePadButtonEvent();

    private event AxisCallback _horizontalEvent;
    private event AxisCallback _verticalEvent;
    private event GamePadButtonEvent _jumpReviveEvent;
    private event GamePadButtonEvent _weakAttackEvent;
    private event GamePadButtonEvent _strongAttackEvent;
    private event GamePadButtonEvent _specialMoveEvent;
    private event GamePadButtonEvent _fusionEvent;
    private event GamePadButtonEvent _pauseEvent;

    private static InputManager instance;
    public static InputManager Instance
    {
        get
        {
            Assert.IsNotNull(instance, "Error : InputManager.instance = null.");
            return instance;
        }
    }

    #region SubscribeToEvents
    public void SubscribeToHorizontalEvent(AxisCallback functionToBind)
    {
        _horizontalEvent += functionToBind;
    }

    public void SubscribeToVerticalEvent(AxisCallback functionToBind)
    {
        _verticalEvent += functionToBind;
    }

    public void SubscribeToJumpReviveEvent(GamePadButtonEvent functionToBind)
    {
        _jumpReviveEvent += functionToBind;
    }

    public void SubscribeToWeakAttackEvent(GamePadButtonEvent functionToBind)
    {
        _weakAttackEvent += functionToBind;
    }
    public void SubscribeToStrongAttackEvent(GamePadButtonEvent functionToBind)
    {
        _strongAttackEvent += functionToBind;
    }
    public void SubscribeToSpecialMoveEvent(GamePadButtonEvent functionToBind)
    {
        _specialMoveEvent += functionToBind;
    }
    public void SubscribeToFusionEvent(GamePadButtonEvent functionToBind)
    {
        _fusionEvent += functionToBind;
    }
    public void SubscribeToPauseEvent(GamePadButtonEvent functionToBind)
    {
        _pauseEvent += functionToBind;
    }
    #endregion

    #region UnsubscribeFromEvents
    public void UnsubscribeFromHorizontalEvent(AxisCallback functionToUnbind)
    {
        _horizontalEvent -= functionToUnbind;
    }

    public void UnsubscribeFromVerticalEvent(AxisCallback functionToUnbind)
    {
        _verticalEvent -= functionToUnbind;
    }

    public void UnsubscribeFromJumpReviveEvent(GamePadButtonEvent functionToUnBind)
    {
        _jumpReviveEvent -= functionToUnBind;
    }

    public void UnsubscribeFromWeakAttackEvent(GamePadButtonEvent functionToUnBind)
    {
        _weakAttackEvent -= functionToUnBind;
    }

    public void UnsubscribeFromStrongAttackEvent(GamePadButtonEvent functionToUnBind)
    {
        _strongAttackEvent -= functionToUnBind;
    }

    public void UnsubscribeFromSpecialMoveEvent(GamePadButtonEvent functionToUnBind)
    {
        _specialMoveEvent -= functionToUnBind;
    }

    public void UnsubscribeFromFusionEvent(GamePadButtonEvent functionToUnBind)
    {
        _fusionEvent -= functionToUnBind;
    }

    public void UnsubscribeFromPauseEvent(GamePadButtonEvent functionToUnBind)
    {
        _pauseEvent -= functionToUnBind;
    }
    #endregion

    void Awake()
    {
        instance = this;
    }

    void Update ()
	{
	    float h_axe = Input.GetAxis("Horizontal");
	    float v_axe = Input.GetAxis("Vertical");

	    if (h_axe != 0f)
            if(_horizontalEvent != null)
                _horizontalEvent(h_axe);
        if (v_axe != 0f)
            if(_verticalEvent != null)
                _verticalEvent(v_axe);

        if (Input.GetButtonDown("Jump/Revive"))
            _jumpReviveEvent();
	    if (Input.GetButtonDown("WeakAttack"))
	        _weakAttackEvent();
	    if (Input.GetButtonDown("StrongAttack"))
	        _strongAttackEvent();
	    if (Input.GetButtonDown("SpecialMove"))
	        _specialMoveEvent();
	    if (Input.GetButtonDown("Fusion"))
	        _fusionEvent();
	    if (Input.GetButtonDown("Pause"))
	        _pauseEvent();
    }
}
