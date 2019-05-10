using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public Camera playerCamera = null;

    protected CharacterController playerController = null;
    protected CapsuleCollider playerCollider = null;
    
    private Vector3 zeroVector = Vector3.zero;
    private Vector3 rotation = new Vector3(0f, 0f, 0f);

    public float speed = 0f;
    public float turnRate = 0.2f;

    //Custom function
    float trimFloat(float input)
    {
        return (input == 0f)? 0f:((input > 0)? 1f:-1f);
    }
    Vector3 trimVector(Vector3 input)
    {
        return new Vector3(trimFloat(input.x), trimFloat(input.y), trimFloat(input.z));
    }
    float circleFloat(float input)
    {
        return (input > 1f) ? -1f : ((input < -1f) ? 1f : input);
    }
    Vector3 circleVector(Vector3 input)
    {
        return new Vector3(circleFloat(input.x), circleFloat(input.y), circleFloat(input.z));
    }
    //  Handler
    void handleInput()
    {
        Vector3 velocity = new Vector3(0f, 0f, 0f);

        if (Input.GetAxis("horizontal") > 0)
        {
            velocity += new Vector3(-1f, 0f, 1f);
        }
        else if (Input.GetAxis("horizontal") < 0)
        {
            velocity += new Vector3(1f, 0f, -1f);
        }
        if (Input.GetAxis("vertical") > 0)
        {
            velocity += new Vector3(-1f, 0f, -1f);
        }
        else if (Input.GetAxis("vertical") < 0)
        {
            velocity += new Vector3(1f, 0f, 1f);
        }
        velocity += new Vector3(0f, -1f, 0f);

        playerController.Move(trimVector(velocity) * speed * Time.deltaTime);

        rotation = trimVector(velocity);
        if(rotation.x == 0f && rotation.z == 0f)
        {
            rotation.x = 1f;
            rotation.z = 1f;
        }
        rotation.y = 0f;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(rotation), .1f);
    }
    void handleCamera()
    {
        if (playerCamera == null)
            return;

        Vector3 targetPosition = new Vector3(transform.position.x + 7.5f, 10f, transform.position.z + 7.5f);

        playerCamera.transform.position = Vector3.SmoothDamp(playerCamera.transform.position, targetPosition, ref zeroVector, .1f);
    }

    void Start() {
        //Get stuff on start
        playerController = GetComponent<CharacterController>();
        //playerCollider = GetComponent<CapsuleCollider>();
    }

    public void Update() {
        handleInput();
        handleCamera();
	}

    //Debugging stuff
    void OnDrawGizmos()
    {
        Color gizmoColor = Color.red;
        gizmoColor.a = .25f;
        Gizmos.color = gizmoColor;

        Vector3 gizmoPosition = transform.localPosition;
        gizmoPosition.y += 2f;

        Gizmos.DrawCube(gizmoPosition, new Vector3(2f, 3.5f, 2f));
    }
}
