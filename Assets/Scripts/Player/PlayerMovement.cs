using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	//public varibables first
	public float speed = 6f;

	//then private
	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f;

	//sets up refrences
	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor"); //floor is the name of the layer
		anim = GetComponent <Animator> (); //FuncitonName<type>();
		playerRigidbody = GetComponent <Rigidbody> ();
	}

	//unity automatically calls all the physics updates with this
	void FixedUpdate()
	{
		float h = Input.GetAxisRaw ("Horizontal"); //Default axis that maps to A&D or <- & ->
		float v = Input.GetAxisRaw ("Vertical"); 

		Move (h, v);
		Turning ();
		Animating (h, v);
	}

	void Move (float h, float v)
	{
		movement.Set (h, 0f, v); //latteral movment, no need to go up and down

		movement = movement.normalized * speed * Time.deltaTime; //normalizes so it moves at the same speed in any direction you move. Per Second with Time.deltaTime

		playerRigidbody.MovePosition (transform.position + movement); //Actualy moves the character relative to its previous position.

	}

	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition); //finds the point under the mouse

		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) //floorMask makes use it only pulls info from that layer.
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse); //Quaternion is a type that stores rotation data, in this case the direction of the mouse from the player is forward
			playerRigidbody.MoveRotation (newRotation);
		}
	}

	void Animating(float h, float v)
	{
		bool walking = h != 0f || v != 0f; //did either of these get triggered
		anim.SetBool ("IsWalking", walking);
	}

}
