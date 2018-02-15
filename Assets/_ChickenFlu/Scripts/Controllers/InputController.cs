using UnityEngine;
using CnControls;


public class InputController : MonoBehaviour 
{
    [SerializeField] private float joystickThreshold = 0.085f;
    [SerializeField] private float keyboardThreshold = 0.05f;
    [SerializeField] private float speedMultiplier = 4f;

    private enum InputType
    {
        WASD,
        Arrows,
        TouchJoystick,
        Joystick
    }
    private Player player;
    private Rigidbody rb;
    private float speed;

	void Start () 
    {
        this.player = GetComponent<Player>();
		this.rb = GetComponent<Rigidbody> ();
	}
	void FixedUpdate () 
    {
        this.speed = this.speedMultiplier * this.player.Speed;
        if (StateManager.Instance.State == GameState.Battle ||
            StateManager.Instance.State == GameState.StressBattle)
        {
            Vector3 movement = Vector3.zero;
            switch (this.name)
            {
                case "Player0":
                    movement = this.KeyboardMovement(InputType.WASD);
                    break;
                case "Player1":
                    movement = this.KeyboardMovement(InputType.Arrows);
                    break;
                case "Player2":
                    movement = this.JoyStickMovement(1);
                    break;
                case "Player3":
                    movement = this.JoyStickMovement(2);
                    break;
            }
            if (movement != Vector3.zero)
            {
                this.rb.velocity = (movement * this.speed);
                this.transform.rotation = Quaternion.LookRotation(movement);
            }
        }
	}
    Vector3 KeyboardMovement (InputType type) 
    {
        switch(type)
        {
            case InputType.Arrows:
                if (Mathf.Abs(Input.GetAxis("KHorizontal")) > keyboardThreshold || 
                    Mathf.Abs(Input.GetAxis("KVertical")) > keyboardThreshold)
                    return new Vector3(Input.GetAxis("KHorizontal"), 0f, Input.GetAxis("KVertical"));
                return Vector3.zero;
            case InputType.WASD:
                if (Mathf.Abs(Input.GetAxis("LKHorizontal")) > keyboardThreshold || 
                    Mathf.Abs(Input.GetAxis("LKVertical")) > keyboardThreshold)
                    return new Vector3(Input.GetAxis("LKHorizontal"), 0f, Input.GetAxis("LKVertical"));
                return Vector3.zero;
            default:
                return Vector3.zero;
        }
	}
    Vector3 JoyStickMovement(int player)
    {
        if (Mathf.Abs(Input.GetAxis("LeftStickX-Player" + player)) > joystickThreshold &&
            Mathf.Abs(Input.GetAxis("LeftStickY-Player" + player)) > joystickThreshold)
            return new Vector3(Input.GetAxis("LeftStickX-Player" + player), 0f, -Input.GetAxis("LeftStickY-Player" + player));
        return Vector3.zero;
    }
    //Vector3 TouchMovement()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        return new Vector3(CnInputManager.GetAxis("Horizontal"), 0f, CnInputManager.GetAxis("Vertical"));
    //    }
    //    return Vector3.zero;
    //}
}
