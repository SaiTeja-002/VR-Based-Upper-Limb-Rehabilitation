// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class AirplaneController : MonoBehaviour
// {

//     // The bend factors determine how quickly the plane bends towards its threshold
//     public float verticalBendFactor;
//     public float horizontalBendFactor;

//     // Thrusts determine the translation of the plane 
//     public float horizontalThrust;
//     public float verticalThrust;
//     public float constantThrust;

//     // Threshold determines the maximum angle the plane can bend
//     public float horizontalLowerThreshold;
//     public float horizontalUpperThreshold;
//     public float verticalLowerThreshold;
//     public float verticalUpperThreshold;

//     // Movement factors determine the distance travelled when the trigger key is held pressed
//     public float horizontalMovementFactor;
//     public float verticalMovementFactor;

//     // Determine the direction of motion
//     private int alt;
//     private int dir;

//     // Angle made by the airplane with the axes
//     private float xAngle;
//     private float yAngle;
//     private float zAngle;

//     // Converting 0-360 scale into -180-180 scale
//     private float xConverted;
//     private float yConverted;
//     private float zConverted;

//     // Vector that is added to the current position of the object
//     private Vector3 step;
    
//     // Start is called before the first frame update
//     void Start()
//     {
//         verticalBendFactor = 8.0f;
//         horizontalBendFactor = 15.0f;

//         horizontalLowerThreshold = 33.0f;
//         horizontalUpperThreshold = 327.0f;
//         verticalLowerThreshold = 27.0f;
//         verticalUpperThreshold = 333.0f;

//         horizontalThrust = 20.0f;
//         verticalThrust = 10.0f;
//         constantThrust = 10f;

//         step.x = horizontalThrust;
//         step.y = verticalThrust;
//         step.z = constantThrust;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         xAngle = gameObject.transform.rotation.eulerAngles.x;
//         yAngle = gameObject.transform.rotation.eulerAngles.y;
//         zAngle = gameObject.transform.rotation.eulerAngles.z;

//         // xConverted = scaleConvert(xAngle);
//         // yConverted = scaleConvert(yAngle);
//         // zConverted = scaleConvert(zAngle);

//         step.z = constantThrust;

//         // step.x = xConverted*constantThrust;
//         // step.y = yConverted*constantThrust;
//         // step.z = zConverted*constantThrust;

//         gameObject.transform.position += step*Time.deltaTime;

//         // Debug.Log("Around x - " + xAngle);
//         // Debug.Log("Around z - " + zAngle);
//         // Debug.Log("x - "+ transform.position.x);
//         // Debug.Log("y - "+ transform.position.y);
//         // Debug.Log("z - "+ transform.position.z);

//         if (Input.GetKey(KeyCode.UpArrow))
//         {
//             step.y = verticalThrust;
//             alt = 1;
//         }
//         else if (Input.GetKey(KeyCode.DownArrow))
//         {
//             step.y = -1*verticalThrust;
//             alt = -1;
//         }
//         else
//         {
//             step.y = 0.0f;
//             alt = 0;
//         }

//         if (Input.GetKey(KeyCode.LeftArrow))
//         {
//             step.x = -1*horizontalThrust;
//             dir = -1;
//         }
//         else if (Input.GetKey(KeyCode.RightArrow))
//         {
//             step.x = horizontalThrust;
//             dir = 1;
//         }
//         else
//         {
//             step.x = 0.0f;
//             dir = 0;
//         }

//         UpDown(alt, xAngle);
//         LeftRight(dir, zAngle);
//     }

//     void UpDown(int alt, float xAngle)
//     {
//         if(((xAngle <= 360.0f && xAngle >= verticalUpperThreshold) || (xAngle >= 0.0f && xAngle <= verticalLowerThreshold)))
//             transform.Rotate(Time.deltaTime*verticalBendFactor*alt*-1, 0, 0);
//         else if(xAngle > verticalLowerThreshold && xAngle < 180.0f)
//             transform.rotation = Quaternion.Euler(verticalLowerThreshold, yAngle, zAngle);
//         else if(xAngle < verticalUpperThreshold && xAngle > 180.0f)
//             transform.rotation = Quaternion.Euler(verticalUpperThreshold, yAngle, zAngle);
//     }

//     void LeftRight(int dir, float zAngle)
//     {
//         if(((zAngle <= 360.0f && zAngle >= horizontalUpperThreshold) || (zAngle >= 0.0f && zAngle <= horizontalLowerThreshold)))
//             transform.Rotate(0, 0, Time.deltaTime*horizontalBendFactor*dir*-1);
//         else if(zAngle > horizontalLowerThreshold && zAngle < 180.0f)
//             transform.rotation = Quaternion.Euler(xAngle, yAngle, horizontalLowerThreshold);
//         else if(zAngle < horizontalUpperThreshold && zAngle > 180.0f)
//             transform.rotation = Quaternion.Euler(xAngle, yAngle, horizontalUpperThreshold);
//     }

//     float scaleConvert(float angle)
//     {
//         return (angle-180.0f);
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneController : MonoBehaviour
{

    public float forwardSpeed, strafeSpeed, hoverSpeed;
    private float activeForwardSpeed, activeStrifeSpeed, activeHoverSpeed;
    private float forwardAcceleration, strafeAcceleration, hoverAcceleration;
    
    public float lookRotateSpeed;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    public float rollSpeed, rollAcceleration, rollSensitivity;

    void Start()
    {
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;

        forwardSpeed = 25f;
        strafeSpeed = 14f;
        hoverSpeed = 5f;

        forwardAcceleration = 2.5f;
        strafeAcceleration = 2f; 
        hoverAcceleration = 2f;

        lookRotateSpeed = 90f;
        
        rollSpeed = 4f;
        rollAcceleration = 3.5f;
        rollSensitivity = 0.2f;

        // Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        // xAngle = transform.rotation.eulerAngles.x;
        // yAngle = transform.rotation.eulerAngles.y;
        // zAngle = transform.rotation.eulerAngles.z;

        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);

        // transform.Rotate(-mouseDistance.y * lookRotateSpeed * Time.deltaTime, mouseDistance.x * lookRotateSpeed * Time.deltaTime, rollInput * rollSpeed * mouseDistance.x, Space.Self);
        
        transform.Rotate(-mouseDistance.y * lookRotateSpeed * Time.deltaTime, mouseDistance.x * lookRotateSpeed * Time.deltaTime, -rollSensitivity * mouseDistance.x, Space.Self);

        // activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration);
        
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, forwardSpeed, forwardAcceleration);
        // activeStrifeSpeed = Mathf.Lerp(activeStrifeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration);
        // activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration);

        // transform.Rotate()
        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        // transform.position += transform.right * activeStrifeSpeed * Time.deltaTime;
        // transform.position += transform.up * activeHoverSpeed * Time.deltaTime;
    }

    void FixedUpdate()
    {

    }
}