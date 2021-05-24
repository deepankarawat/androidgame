using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Transform initialPos;
    public Rigidbody2D Character;
    public float moveForce;
    public Animator animation;
    private Touch theTouch;
    private Vector3 destination;
    public int coin = 0;
   

    // Start is called before the first frame update
    void Start()
    {
        Character.position = initialPos.position;
        animation = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        //============================ INPUT FOR TOUCH =========================

        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);
            destination = Camera.main.ScreenToWorldPoint(theTouch.position);
            destination.z = 0.0f;

            if(theTouch.phase == TouchPhase.Began)
            {
               Character.AddForce((new Vector2(destination.x - Character.position.x, destination.y - Character.position.y)).normalized * moveForce);
            }
        }

        //============================ INPUT FOR KEYBOARD =========================

        if (Input.GetKeyDown(KeyCode.D))
        {
            Character.AddForce(new Vector2(moveForce, 0.0f));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Character.AddForce(new Vector2(-moveForce, 0.0f));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Character.AddForce(new Vector2(0.0f, moveForce));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Character.AddForce(new Vector2(0.0f , - moveForce));
        }

        if (Character.velocity == Vector2.zero)
        {
            animation.SetBool("isMoving", false);
        }
        else
        {
            animation.SetBool("isMoving", true);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "coin")
        {
            Destroy(collision.gameObject);
            coin++;
        }
        else
        {
            Character.position = initialPos.position;

            Character.velocity = Vector2.zero;
            coin = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(1);
    }
}

  
        

