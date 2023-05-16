using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
	PhotonView PV;

	//Code derived from https://stackoverflow.com/users/7111561/derhugo

	[Header("Constants")]

	//unity controls and constants input
	public float AccelerationMod;
	public float XAxisSensitivity;
	public float YAxisSensitivity;
	public float DecelerationMod;

	[Space]

	[Range(0, 89)] public float MaxXAngle = 60f;

	[Space]

	public float OgMaximumMovementSpeed = 1f;
	public float MaximumMovementSpeed = 1f;
	[Header("Controls")]

	public KeyCode Forwards = KeyCode.W;
	public KeyCode Backwards = KeyCode.S;
	public KeyCode Left = KeyCode.A;
	public KeyCode Right = KeyCode.D;
	public KeyCode Up = KeyCode.Space;
	public KeyCode Down = KeyCode.LeftControl;
	public KeyCode Sprint = KeyCode.LeftShift;
	public GameObject[] selfGraphicDestruction; 

	private Vector3 _moveSpeed;

	//Hold breath button. Player shows as static for enemy if they are breathing. 

	//Anouncement stations where everyone can hear

	private void Awake()
	{
		PV = GetComponent<PhotonView>();
	}
	private void Start()
	{
		if (!PV.IsMine)
		{
			Destroy(GetComponentInChildren<Camera>().gameObject);
		}
        else
        {
			foreach(GameObject graphic in selfGraphicDestruction)
            {
				Destroy(graphic);
            }
        }


		_moveSpeed = Vector3.zero;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update()
	{

		if (!PV.IsMine)
			return;

		freeCamUpdate();

	}

	void freeCamUpdate()
    {
		if (Input.GetKey(Sprint))
		{
			MaximumMovementSpeed = OgMaximumMovementSpeed * 2f;
			Debug.Log(MaximumMovementSpeed);
		}
		else
		{
			MaximumMovementSpeed = OgMaximumMovementSpeed;
		}

		transform.Translate(_moveSpeed);


		if (!PV.IsMine)
			return;

		HandleMouseRotation();

		var acceleration = HandleKeyInput();

		_moveSpeed += acceleration;

		HandleDeceleration(acceleration);

		// clamp the move speed
		if (_moveSpeed.magnitude > MaximumMovementSpeed)
		{
			_moveSpeed = _moveSpeed.normalized * MaximumMovementSpeed;
		}
	}

	private void FixedUpdate()
	{
		
	}

	private Vector3 HandleKeyInput()
	{
		var acceleration = Vector3.zero;

		//key input detection
		if (Input.GetKey(Forwards))
		{
			acceleration.z += 1;
		}

		if (Input.GetKey(Backwards))
		{
			acceleration.z -= 1;
		}

		if (Input.GetKey(Left))
		{
			acceleration.x -= 1;
		}

		if (Input.GetKey(Right))
		{
			acceleration.x += 1;
		}

		if (Input.GetKey(Up))
		{
			acceleration.y += 1;
		}

		if (Input.GetKey(Down))
		{
			acceleration.y -= 1;
		}

		return acceleration.normalized * AccelerationMod;
	}

	private float _rotationX;

	private void HandleMouseRotation()
	{
		//mouse input
		var rotationHorizontal = XAxisSensitivity * Input.GetAxis("Mouse X");
		var rotationVertical = YAxisSensitivity * Input.GetAxis("Mouse Y");

		//applying mouse rotation
		// always rotate Y in global world space to avoid gimbal lock
		transform.Rotate(Vector3.up * rotationHorizontal, Space.World);

		var rotationY = transform.localEulerAngles.y;

		_rotationX += rotationVertical;
		_rotationX = Mathf.Clamp(_rotationX, -MaxXAngle, MaxXAngle);

		transform.localEulerAngles = new Vector3(-_rotationX, rotationY, 0);
	}

	private void HandleDeceleration(Vector3 acceleration)
	{
		//deceleration functionality
		if (Mathf.Approximately(Mathf.Abs(acceleration.x), 0))
		{
			if (Mathf.Abs(_moveSpeed.x) < DecelerationMod)
			{
				_moveSpeed.x = 0;
			}
			else
			{
				_moveSpeed.x -= DecelerationMod * Mathf.Sign(_moveSpeed.x);
			}
		}

		if (Mathf.Approximately(Mathf.Abs(acceleration.y), 0))
		{
			if (Mathf.Abs(_moveSpeed.y) < DecelerationMod)
			{
				_moveSpeed.y = 0;
			}
			else
			{
				_moveSpeed.y -= DecelerationMod * Mathf.Sign(_moveSpeed.y);
			}
		}

		if (Mathf.Approximately(Mathf.Abs(acceleration.z), 0))
		{
			if (Mathf.Abs(_moveSpeed.z) < DecelerationMod)
			{
				_moveSpeed.z = 0;
			}
			else
			{
				_moveSpeed.z -= DecelerationMod * Mathf.Sign(_moveSpeed.z);
			}
		}
	}
}
