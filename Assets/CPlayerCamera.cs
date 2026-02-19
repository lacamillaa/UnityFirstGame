using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotazione;
    float yRotazione;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotazione += mouseX;
        xRotazione -= mouseY;
        xRotazione = Mathf.Clamp(xRotazione, -45f, 45f);

        transform.rotation = Quaternion.Euler(xRotazione, yRotazione, 0);
        orientation.rotation = Quaternion.Euler(0, yRotazione, 0);
    }
}
