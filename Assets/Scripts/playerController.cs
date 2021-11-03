using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class playerController : MonoBehaviour
{
    [HideInInspector]
    public int id;

    [Header("Info")]
    public float moveSpeed;
    public float jumpForce;
    public GameObject hatObject;

    [HideInInspector]
    public float currHatTime;

    [Header("Components")]
    public Rigidbody rb;
    public Player photonPlayer;

	private void Update()
	{
        Move();

        if(Input.GetKeyDown(KeyCode.Space))
		{
            TryJump();
		}
	}

    void Move()
	{
        float x = Input.GetAxis("Vertical") * moveSpeed;
        float z = Input.GetAxis("Horizontal") * moveSpeed;

        rb.velocity = new Vector3(x, rb.velocity.y, z);
	}

    void TryJump()
	{
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, 0.7f))
		{
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}
	}
}
