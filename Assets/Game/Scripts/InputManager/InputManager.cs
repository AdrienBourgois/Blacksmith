using UnityEngine;
using UnityEngine.Assertions;

public class InputManager : MonoBehaviour
{
    public delegate void AxisCallback(float axe);
    public delegate void GamePadButtonEvent();

    private event AxisCallback _horizontalP1Event;
    private event AxisCallback _verticalP1Event;
    private event GamePadButtonEvent _jumpReviveP1Event;
    private event GamePadButtonEvent _weakAttackP1Event;
    private event GamePadButtonEvent _strongAttackP1Event;
    private event GamePadButtonEvent _specialMoveP1Event;
    private event GamePadButtonEvent _fusionP1Event;
    private event GamePadButtonEvent _pauseP1Event;

    private event AxisCallback _horizontalP2Event;
    private event AxisCallback _verticalP2Event;
    private event GamePadButtonEvent _jumpReviveP2Event;
    private event GamePadButtonEvent _weakAttackP2Event;
    private event GamePadButtonEvent _strongAttackP2Event;
    private event GamePadButtonEvent _specialMoveP2Event;
    private event GamePadButtonEvent _fusionP2Event;
    private event GamePadButtonEvent _pauseP2Event;

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
    public void SubscribeToHorizontalP1Event(AxisCallback functionToBind)
    {
        _horizontalP1Event += functionToBind;
    }

    public void SubscribeToVerticalP1Event(AxisCallback functionToBind)
    {
        _verticalP1Event += functionToBind;
    }

    public void SubscribeToJumpReviveP1Event(GamePadButtonEvent functionToBind)
    {
        _jumpReviveP1Event += functionToBind;
    }

    public void SubscribeToWeakAttackP1Event(GamePadButtonEvent functionToBind)
    {
        _weakAttackP1Event += functionToBind;
    }

    public void SubscribeToStrongAttackP1Event(GamePadButtonEvent functionToBind)
    {
        _strongAttackP1Event += functionToBind;
    }

    public void SubscribeToSpecialMoveP1Event(GamePadButtonEvent functionToBind)
    {
        _specialMoveP1Event += functionToBind;
    }

    public void SubscribeToFusionP1Event(GamePadButtonEvent functionToBind)
    {
        _fusionP1Event += functionToBind;
    }

    public void SubscribeToPauseP1Event(GamePadButtonEvent functionToBind)
    {
        _pauseP1Event += functionToBind;
    }

    public void SubscribeToHorizontalP2Event(AxisCallback functionToBind)
    {
        _horizontalP2Event += functionToBind;
    }

    public void SubscribeToVerticalP2Event(AxisCallback functionToBind)
    {
        _verticalP2Event += functionToBind;
    }

    public void SubscribeToJumpReviveP2Event(GamePadButtonEvent functionToBind)
    {
        _jumpReviveP2Event += functionToBind;
    }

    public void SubscribeToWeakAttackP2Event(GamePadButtonEvent functionToBind)
    {
        _weakAttackP2Event += functionToBind;
    }

    public void SubscribeToStrongAttackP2Event(GamePadButtonEvent functionToBind)
    {
        _strongAttackP2Event += functionToBind;
    }

    public void SubscribeToSpecialMoveP2Event(GamePadButtonEvent functionToBind)
    {
        _specialMoveP2Event += functionToBind;
    }

    public void SubscribeToFusionP2Event(GamePadButtonEvent functionToBind)
    {
        _fusionP2Event += functionToBind;
    }

    public void SubscribeToPauseP2Event(GamePadButtonEvent functionToBind)
    {
        _pauseP2Event += functionToBind;
    }
    #endregion

    #region UnsubscribeFromEvents
    public void UnsubscribeFromHorizontalP1Event(AxisCallback functionToUnbind)
    {
        _horizontalP1Event -= functionToUnbind;
    }

    public void UnsubscribeFromVerticalP1Event(AxisCallback functionToUnbind)
    {
        _verticalP1Event -= functionToUnbind;
    }

    public void UnsubscribeFromJumpReviveP1Event(GamePadButtonEvent functionToUnBind)
    {
        _jumpReviveP1Event -= functionToUnBind;
    }

    public void UnsubscribeFromWeakAttackP1Event(GamePadButtonEvent functionToUnBind)
    {
        _weakAttackP1Event -= functionToUnBind;
    }

    public void UnsubscribeFromStrongAttackP1Event(GamePadButtonEvent functionToUnBind)
    {
        _strongAttackP1Event -= functionToUnBind;
    }

    public void UnsubscribeFromSpecialMoveP1Event(GamePadButtonEvent functionToUnBind)
    {
        _specialMoveP1Event -= functionToUnBind;
    }

    public void UnsubscribeFromFusionP1Event(GamePadButtonEvent functionToUnBind)
    {
        _fusionP1Event -= functionToUnBind;
    }

    public void UnsubscribeFromPauseP1Event(GamePadButtonEvent functionToUnBind)
    {
        _pauseP1Event -= functionToUnBind;
    }

    public void UnsubscribeFromHorizontalP2Event(AxisCallback functionToUnbind)
    {
        _horizontalP2Event -= functionToUnbind;
    }

    public void UnsubscribeFromVerticalP2Event(AxisCallback functionToUnbind)
    {
        _verticalP2Event -= functionToUnbind;
    }

    public void UnsubscribeFromJumpReviveP2Event(GamePadButtonEvent functionToUnBind)
    {
        _jumpReviveP2Event -= functionToUnBind;
    }

    public void UnsubscribeFromWeakAttackP2Event(GamePadButtonEvent functionToUnBind)
    {
        _weakAttackP2Event -= functionToUnBind;
    }

    public void UnsubscribeFromStrongAttackP2Event(GamePadButtonEvent functionToUnBind)
    {
        _strongAttackP2Event -= functionToUnBind;
    }

    public void UnsubscribeFromSpecialMoveP2Event(GamePadButtonEvent functionToUnBind)
    {
        _specialMoveP2Event -= functionToUnBind;
    }

    public void UnsubscribeFromFusionP2Event(GamePadButtonEvent functionToUnBind)
    {
        _fusionP2Event -= functionToUnBind;
    }

    public void UnsubscribeFromPauseP2Event(GamePadButtonEvent functionToUnBind)
    {
        _pauseP2Event -= functionToUnBind;
    }
    #endregion

    void Awake()
    {
        instance = this;
    }

    void Update ()
	{
	    float horizontalP1Axe = Input.GetAxis("Horizontal_P1");
	    float verticalP1Axe = Input.GetAxis("Vertical_P1");
	    float horizontalP2Axe = Input.GetAxis("Horizontal_P2");
	    float verticalP2Axe = Input.GetAxis("Vertical_P2");

        if (horizontalP1Axe != 0f)
            if(_horizontalP1Event != null)
                _horizontalP1Event(horizontalP1Axe);
        if (verticalP1Axe != 0f)
            if(_verticalP1Event != null)
                _verticalP1Event(verticalP1Axe);
	    if (horizontalP2Axe != 0f)
	        if (_horizontalP2Event != null)
	            _horizontalP2Event(horizontalP2Axe);
	    if (verticalP2Axe != 0f)
	        if (_verticalP2Event != null)
	            _verticalP2Event(verticalP2Axe);

        if (Input.GetButtonDown("Jump/Revive_P1"))
            if (_jumpReviveP1Event != null)
                _jumpReviveP1Event();
	    if (Input.GetButtonDown("Jump/Revive_P2"))
	        if (_jumpReviveP2Event != null)
	            _jumpReviveP2Event();
        if (Input.GetButtonDown("WeakAttack_P1"))
	        if (_weakAttackP1Event != null)
	            _weakAttackP1Event();
	    if (Input.GetButtonDown("WeakAttack_P2"))
	        if (_weakAttackP2Event != null)
	            _weakAttackP2Event();
        if (Input.GetButtonDown("StrongAttack_P1"))
	        if (_strongAttackP1Event != null)
	            _strongAttackP1Event();
	    if (Input.GetButtonDown("StrongAttack_P2"))
	        if (_strongAttackP2Event != null)
	            _strongAttackP2Event();
        if (Input.GetButtonDown("SpecialMove_P1"))
	        if (_specialMoveP1Event != null)
	            _specialMoveP1Event();
	    if (Input.GetButtonDown("SpecialMove_P2"))
	        if (_specialMoveP2Event != null)
	            _specialMoveP2Event();
        if (Input.GetButtonDown("Fusion_P1"))
	        if (_fusionP1Event != null)
	            _fusionP1Event();
	    if (Input.GetButtonDown("Fusion_P2"))
	        if (_fusionP2Event != null)
	            _fusionP2Event();
        if (Input.GetButtonDown("Pause_P1"))
	        if (_pauseP1Event != null)
	            _pauseP1Event();
	    if (Input.GetButtonDown("Pause_P2"))
	        if (_pauseP2Event != null)
	            _pauseP2Event();
    }
}
