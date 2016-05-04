using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Control : MonoBehaviour 
{


	private float yaw = 0.0f;
	private float pitch = 0.0f;

	public GameObject hands;

	Quaternion originalRotation;


	void Start () 
	{


		originalRotation = transform.rotation;

	}
	
	// Update is called once per frame

	void Punch(Vector3 punch)
	{
		Debug.Log ("Punch x:" + punch.x + " y:" + punch.y);

	}
	void Update () 
	{


		Cursor.lockState = CursorLockMode.Locked;
		#if	UNITY_ANDROID
		
		yaw += 5 * Input.acceleration.x;
		pitch -= 5 * Input.acceleration.y;
		#else
		yaw += 5 * Input.GetAxis("Mouse X");
		pitch += 5 * Input.GetAxis("Mouse Y");
		#endif
		
		if (pitch > 60)
			pitch = 60;
		if (pitch < -60)
			pitch = -60;
		
		if (yaw > 360)
			yaw -= 360;
		if (yaw < -360)
			yaw += 360;
		
		
		Quaternion xQuaternion = Quaternion.AngleAxis (yaw, Vector3.up);
		Quaternion yQuaternion = Quaternion.AngleAxis (pitch, -Vector3.right);
		hands.transform.rotation = transform.rotation = originalRotation * xQuaternion * yQuaternion;

	
	}

}
