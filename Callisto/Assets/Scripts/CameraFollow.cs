using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // obiekt, za którym kamera ma podążać
    //public float cameraRotation = 50f; // Nachylenie kamery względem gracza
    //public float cameraYOffset = 5f; // odległość kamery w osi Y względem gracza
    //public float cameraZOffset = -2f; // odległość kamery w osi Z względem gracza

    private Vector3 initialPosition;

    void Start()
    {
        //initialPosition = transform.position;
    }

    void LateUpdate()
    {
        //Vector3 desiredPosition = target.position + new Vector3(0, cameraYOffset, cameraZOffset);
        Vector3 desiredPosition = target.position + new Vector3(0, 5, 0);
        transform.position = desiredPosition;

        //transform.rotation = Quaternion.Euler(cameraRotation, 0, 0);
    }
}
