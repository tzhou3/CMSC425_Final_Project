using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jump;
    private float doubleJump;
    private float GroundDistance;
    public float accel;
    private Rigidbody rb;
    private CharacterController controller;
    public GameObject inventory;
    public Animator anim;
    private bool hasSled;
    private bool isFinishedAnimation;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inventory.SetActive(false);
        anim = GetComponentInChildren<Animator>();
        isFinishedAnimation = true;
        hasSled = true;
        doubleJump = 0;
        transform.Find("sleddingModel").gameObject.SetActive(false);
        controller = GetComponent<CharacterController>();
    }

    IEnumerator JumpAnim()
    {
        isFinishedAnimation = false;
        anim.SetTrigger("Jump");

        //Wait until Animator is done playing
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") &&
        anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            //Wait every frame until animation has finished
            yield return null;
        }
        
    }
    void Update()
    {

        if (IsGrounded())
        {
            doubleJump = 0;
        }
 
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(inventory.activeSelf == true)
            {
                inventory.SetActive(false);
            }
            else
            {
                inventory.SetActive(true);
            }
        }
        if (!hasSled | !Input.GetMouseButton(0))
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {

                doubleJump += 1;
                if (doubleJump < 2)
                {
                    //StartCoroutine(JumpAnim());
                    rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
                }

          
            }

            if ((Input.GetAxisRaw("Vertical") != 0))
            {
                //character is jumping
                if (!IsGrounded())
                {
                    anim.SetBool("isWalking", false);
                    transform.position += transform.forward * speed/2 * Time.deltaTime;
                }
                //character is walking
                else
                {
                    anim.SetBool("isWalking", true);
                    transform.position += transform.forward * speed * Time.deltaTime;
                }
               
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
        }
    }
    void FixedUpdate()
    {
        rb.drag = 2;
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        Vector3 translation = new Vector3(0, 0, Input.GetAxis("Vertical")) * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Rotate(0, rotation, 0);

        if (hasSled) {

            if (Input.GetMouseButton(0))
            {
                transform.Find("riggedModel").gameObject.SetActive(false);
                transform.Find("sleddingModel").gameObject.SetActive(true);
                rb.freezeRotation = false;

                rb.drag = 0.1f;
                rb.AddForce(transform.forward * accel);
            }

            else if (Input.GetMouseButtonUp(0))
            {
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                transform.Find("riggedModel").gameObject.SetActive(true);
                transform.Find("sleddingModel").gameObject.SetActive(false);

        


            }
            else
            {

                //if (Input.GetKey(KeyCode.UpArrow))
                //{
                //    // Move translation along the object's z-axis
                //    //transform.Translate(0, 0, translation);
                //    rb.drag = 4;
                //    rb.AddForce(transform.forward * speed);
                //}
                //if (Input.GetKey(KeyCode.DownArrow))
                //{
                //    // Move translation along the object's z-axis
                //    //transform.Translate(0, 0, translation);
                //    rb.drag = 4;
                //    rb.AddForce(transform.forward * speed * -1);
                //}
            }

        }


        
    }

    bool IsGrounded()
    {
        RaycastHit hit;
        float distance = 1f;
        Vector3 dir = new Vector3(0, -1);

        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}

