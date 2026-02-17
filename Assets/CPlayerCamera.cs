using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensX;  //senisibiltià della asse X
    public float sensY;  //senisibiltià della asse Y

    public Transform orientation; //Orientamento 

    float xRotazione; //Movimento di rotazione di X
    float yRotazione; //Movimento di rotazione di Y


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Otteniamo la posizione dela X del cursore
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        //Otteniamo la posizione dela Y del cursore
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        //
        yRotazione += mouseX;
        xRotazione -= mouseY;
        xRotazione = Mathf.Clamp(xRotazione, -90f, 90f);

        // Ruotiamo la camera
        transform.rotation = Quaternion.Euler(xRotazione, yRotazione, 0);
        orientation.rotation = Quaternion.Euler(0, yRotazione, 0);

    }
}
