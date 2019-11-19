using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jump;
    public float accel;
    private Rigidbody rb;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.drag = 2;
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector3.up * jump;
        }

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
}