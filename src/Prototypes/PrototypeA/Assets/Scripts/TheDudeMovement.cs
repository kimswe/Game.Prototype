using UnityEngine;
using System.Collections;

public class TheDudeMovement : MonoBehaviour
{
    public float maxSpeed = 6.0f;
    public bool facingRight = true;
    public float moveDirection;
	public float jumpSpeed = 0.1f;

	private bool moving;
	private float weight = 30f;
	private float liftSpeed = 2f;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private float lerpStartTime;
	private bool front;
	private float timeTakenDuringLerp = 0.5f;


    void FixedUpdate()
    {
		rigidbody.velocity = new Vector3 (moveDirection * maxSpeed, rigidbody.velocity.y, 0);

		if (moving) 
		{ 
			float timeSinceStarted = Time.time - lerpStartTime;
			float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

			transform.position = Vector3.Lerp (startPosition, endPosition, percentageComplete);
			if(percentageComplete >= 1.0f)
			{
				moving = false;
			}
		}
    }

    void Update()
    {
		moveDirection = Input.GetAxis("Horizontal");

		if (Input.GetButtonDown ("Jump")) 
		{
			rigidbody.velocity += new Vector3(0,5,0);

		}

		if (Input.GetButtonDown("Fire1")) 
		{
			moving = true;
			front = !front;
			lerpStartTime = Time.time;
			startPosition = rigidbody.position;
			endPosition = rigidbody.position + Vector3.back *  (front ? -10f : 10f);
		}

	}
}
