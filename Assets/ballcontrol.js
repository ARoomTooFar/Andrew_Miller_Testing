#pragma strict
//these are now properties of the object  in game and can be edited in unity
//thses are public variables that show up in unity
var rotationSpeed = 1000;
var jumpHeight = 8;


//privates are only accessable within the script.
private var isFalling = false;

function Update () {
	//this is the code for horizontal movement.
	var rotation : float = Input.GetAxis ("Horizontal") * rotationSpeed;
	rotation *= Time.deltaTime;
	rigidbody.AddRelativeTorque(Vector3.back * rotation);
	if (Input.GetKeyDown(KeyCode.W) && isFalling == false)
	{
		rigidbody.velocity.y = jumpHeight;
		
	}
	isFalling = true;
}

function OnCollisionStay ()
{
	isFalling = false;


}