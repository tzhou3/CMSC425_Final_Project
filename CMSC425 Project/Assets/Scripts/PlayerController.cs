using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jump;
    private int doubleJump;
    private float groundDistance;
    public float accel;
    private Rigidbody rb;
    private CharacterController controller;
    public GameObject inventory;
    public Animator anim;
    public bool hasSled;
    private bool isRunning;
    private int numItems;
    public Sprite plankSprite;
    public Sprite railsSprite;
    public Sprite sledSprite;
    public Sprite wedgeSprite;
    public Vector3 startPos;
    public Vector3 topOfMountain;
    private bool isSledding;

    InventorySlot[] slots;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inventory.SetActive(false);
        anim = GetComponentInChildren<Animator>();
        hasSled = false;
        doubleJump = 0;
        transform.Find("sleddingModel").gameObject.SetActive(false);
        controller = GetComponent<CharacterController>();
        numItems = 0;
        slots = inventory.GetComponentsInChildren<InventorySlot>();
        startPos = transform.position;
        topOfMountain = new Vector3(157,125,-71);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isRunning = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isRunning = true;
        }

        if (IsGrounded())
        {
            if (doubleJump == 1 && isRunning)
            {
                anim.SetBool("continueRunning", true);
            } else
            {
                anim.SetBool("continueRunning", false);
            }
            
            anim.SetBool("secondJump", false);
            anim.SetBool("firstJump", false);
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
                if (doubleJump <= 2)
                {
                    if (doubleJump == 2)
                    {
                        anim.SetBool("secondJump", true);
                        rb.AddForce(Vector3.up * 1.25f * jump, ForceMode.Impulse);
                    } else
                    {
                        anim.SetBool("firstJump", true);
                        rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
                    }
                    //StartCoroutine(JumpAnim());
                }

            }

            if ((Input.GetAxisRaw("Vertical") != 0))
            {
                //character is jumping
                if (!IsGrounded())
                {
                    
                    transform.position += transform.forward * speed/2 * Time.deltaTime;
                    //rb.AddForce(transform.forward * speed/2, ForceMode.Impulse);
                }
                //character is walking
                else
                {
                   transform.position += transform.forward * speed * Time.deltaTime;
                   //rb.AddForce(transform.forward * speed, ForceMode.Impulse);
                }
                anim.SetBool("isWalking", true);
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
        rb.mass = 2;
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
                rb.mass = 0.3f;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wedges")
        {
            GameObject[] wedges = GameObject.FindGameObjectsWithTag("Wedges");
            for(var i = 0; i < wedges.Length; i++)
            {
                Destroy(wedges[i]);
            }
            slots[numItems].addItem(wedgeSprite);
            FindObjectOfType<AudioManager>().Play("Item_Pickup");
            numItems++;
        }
        else if (other.tag == "SleddyParts")
        {
            GameObject[] sledParts = GameObject.FindGameObjectsWithTag("SleddyParts");
            
            for (var i = 0; i < sledParts.Length; i++)
            {
                Destroy(sledParts[i]);
            }
            slots[numItems].addItem(sledSprite);
            FindObjectOfType<AudioManager>().Play("Item_Pickup");
            numItems++;
        }
        else if (other.tag == "Rails")
        {
            GameObject[] rails = GameObject.FindGameObjectsWithTag("Rails");
            
            for (var i = 0; i < rails.Length; i++)
            {
                Destroy(rails[i]);
            }
            slots[numItems].addItem(railsSprite);
            FindObjectOfType<AudioManager>().Play("Item_Pickup");
            numItems++;
        }
        else if(other.tag == "Planks")
        {
            GameObject[] planks = GameObject.FindGameObjectsWithTag("Planks");
            
            for (var i = 0; i < planks.Length; i++)
            {
                Destroy(planks[i]);
            }
            slots[numItems].addItem(plankSprite);
            FindObjectOfType<AudioManager>().Play("Item_Pickup");
            numItems++;
        }else if(other.tag == "Death")
        {
            RestartScene();
        }
        else if (other.tag == "music_collider" && numItems >= 2)
        {
            other.gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().FadeOut("Glacial_Ruins");
            FindObjectOfType<AudioManager>().FadeIn("Glacial_Theme");
        }
        if (numItems >= 4)
        {
            hasSled = true;
        }
        
    }

    void RestartScene()
    {
        if (hasSled)
        {
            transform.position = topOfMountain;
        }
        else
        {
            transform.position = startPos;
        }
    }

    bool IsGrounded()
    {
        RaycastHit hit;
        float distance = .25f;
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

