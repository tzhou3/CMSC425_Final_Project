using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jump;
    public float accel;
    private Rigidbody rb;
    public GameObject inventory;
    private bool hasSled;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inventory.SetActive(false);
    }

    private void Update()
    {
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
        if (!hasSled)
        {
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
        }
    }
    void FixedUpdate()
    {
        rb.drag = 2;
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        Vector3 translation = new Vector3(0,0,Input.GetAxis("Vertical")) * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        if (hasSled) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = Vector3.up * jump;
            }
            // Rotate around our y-axis
            if (Input.GetMouseButton(0))
            {
                rb.drag = 0.1f;
                rb.AddForce(transform.forward * accel);
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                // Move translation along the object's z-axis
                //transform.Translate(0, 0, translation);
                rb.drag = 4;
                rb.AddForce(transform.forward * speed);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                // Move translation along the object's z-axis
                //transform.Translate(0, 0, translation);
                rb.drag = 4;
                rb.AddForce(transform.forward * speed * -1);
            }
        }

        transform.Rotate(0, rotation, 0);

        
    }
}