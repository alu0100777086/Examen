using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]

public class ControllerSphere : MonoBehaviour {

	public float walkSpeed = 8f;
	public float speed = 6.0F;
	public float gravity = 20.0F;
	private Quaternion targetRotation;
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	public float sensitivityX = 5F;
	public float sensitivityY = 5F;
	
	public float minimumX = -360F;
	public float maximumX = 360F;
	
	public float minimumY = -60F;
	public float maximumY = 60F;
	
	float rotationY = 0F;

    public Material mTransform;

    public GameObject[] cubes;
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
        controller.detectCollisions = true;
        positionSpheres();
	}
	
	// Update is called once per frame
	void Update () {
		updateMovement();
	}
	void positionSpheres() {
        System.Random rnd = new System.Random();
		for (int i = 0; i < cubes.Length; i++) {
			cubes[i].transform.position = new Vector3((float)rnd.NextDouble() * 20 - 10, 0.7F, (float)rnd.NextDouble() * 20 - 10);
        }
	}
	void updateMovement()
	{
        
		Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		
		CharacterController controller = GetComponent<CharacterController>();
		//if (controller.isGrounded)
		//{
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
		//}

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
		
		float rotationX = transform.eulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
		float aux = transform.eulerAngles.x;
		
		rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
		
		
		rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
		
		transform.eulerAngles = new Vector3(-rotationY, rotationX, 0);

        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            GetComponent<Animator>().Play("flight");
        }
        else
        {
            GetComponent<Animator>().Play("idle");
        }
	}
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name != "Cube")
            hit.gameObject.GetComponent<Renderer>().material = mTransform;
    }

}
