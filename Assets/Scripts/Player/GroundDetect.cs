using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect : MonoBehaviour {
	private PlayerController player;
	public float groundDistance;
	public LayerMask groundLayer;

	public float width;
	public float height;

	private Vector3 ray2;
	private Vector3 ray3;


	// Use this for initialization
	void Start () {

		player = GetComponent<PlayerController>();

		width = GetComponent<BoxCollider2D>().bounds.extents.x - 0.01f;
		height = GetComponent<BoxCollider2D>().bounds.extents.y + 0.02f;




	}

	// Update is called once per frame
	void Update () {

		GroundDetection();
	}

	void GroundDetection()
	{

		ray2 = new Vector3(transform.position.x + width, transform.position.y, transform.position.z);
		ray3 = new Vector3(transform.position.x - width, transform.position.y, transform.position.z);


		RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, groundDistance, groundLayer);
		RaycastHit2D hit2 = Physics2D.Raycast(ray2, -Vector2.up, groundDistance, groundLayer);
		RaycastHit2D hit3 = Physics2D.Raycast(ray3, -Vector2.up, groundDistance, groundLayer);

		Ray2D landingRay = new Ray2D(transform.position, -Vector2.up);

		Debug.DrawRay(transform.position, -Vector2.up * groundDistance);
		Debug.DrawRay(ray2, -Vector2.up * groundDistance);
		Debug.DrawRay(ray3, -Vector2.up * groundDistance);

		if(hit.collider != null)
		{

			if(hit.collider.gameObject.tag == "Ground" && (player.GetRigidbody().velocity.y > 5))
			{
				player.grounded = false;
			}

			else
			{
				player.grounded = true;
			}
		}

		else if(hit2.collider != null)
		{
			if(hit2.collider.gameObject.tag == "Ground" && (player.GetRigidbody().velocity.y > 5))
			{
				player.grounded = false;
			}
			else
			{
				player.grounded = true;
			}
		}

		else if(hit3.collider != null)
		{
			if(hit3.collider.gameObject.tag == "Ground" && (player.GetRigidbody().velocity.y > 5))
			{
				player.grounded = false;
			}
			else
			{
				player.grounded = true;
			}
		}

		else
		{
			player.grounded = false;
		}
	}
	}
