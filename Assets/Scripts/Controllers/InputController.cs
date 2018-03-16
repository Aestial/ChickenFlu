using UnityEngine;
using CnControls;
using System.Collections;

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
    public Vector3 movement = Vector3.zero;
    public bool movementDisabled;

	void Start () 
    {
        this.player = GetComponent<Player>();
		this.rb = GetComponent<Rigidbody> ();
	}
	void FixedUpdate () 
    {
        movement = this.KeyboardMovement(InputType.WASD);
        this.speed = this.speedMultiplier * this.player.Speed;

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

    public IEnumerator ReturnMovement () {

        movementDisabled = true;
        yield return new WaitForSeconds(2);
        movementDisabled = false;

    }

}
