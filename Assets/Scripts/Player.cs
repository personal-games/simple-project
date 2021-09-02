using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool crouch = false;
    public float speed = 10f;
    public Rigidbody2D rigidbody2D;
    public bool standing;
    public float jetSpeed = 15f;
    public Vector2 maxVelocity = new Vector2(3, 5);
    public float airJetSpeedMultiplier = .3f;
    private PlayerController controller;
    private Animator animator;
	private float localScaleX;
	private float localScaleY;
	
    // Use this for initialization
    void Start () {
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
		localScaleX = gameObject.transform.localScale.x;
		localScaleY = gameObject.transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        var forceX = 0f;
        var forceY = 0f;

        var absVelx = Mathf.Abs(rigidbody2D.velocity.x);
        var absVely = Mathf.Abs(rigidbody2D.velocity.y);

        //detect character standing or not
        if (absVely < 0.2f)
        {
            standing = true;
        }else
        {
            standing = false;
        }

        if (controller.moving.x != 0)
        {
            if (absVelx < maxVelocity.x)
            {
                forceX = standing ? speed * controller.moving.x : (speed * controller.moving.x * airJetSpeedMultiplier);
                

				transform.localScale = new Vector3(forceX > 0 ? localScaleX : (localScaleX * -1), localScaleY, 1);

            }
            animator.SetInteger("Walk", 1);
        }else
        {
            animator.SetInteger("Walk", 0);
        }

        if (controller.moving.y > 0)
        {
            if (absVely < maxVelocity.y)
            {
                forceY = jetSpeed * controller.moving.y;
            }
        }

        rigidbody2D.AddForce(new Vector2(forceX, forceY));
	}
}
