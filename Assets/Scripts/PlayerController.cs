using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float velocidad;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int contador;
    private bool isAndroid;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        contador = 0;
        SetCountText();
        winText.text = "";

#if UNITY_ANDROID
        isAndroid = true;
#else
        isAndroid = false;
#endif
    }

    void FixedUpdate()
    {
        if (isAndroid)
        {
            // Get input from accelerometer
            float posH = Input.acceleration.x;
            float posV = Input.acceleration.y;

            Vector3 movimiento = new Vector3(posH, 0.0f, posV);

            rb.AddForce(movimiento * velocidad);
        }
        else
        {
            // Get input from arrow keys
            float posH = Input.GetAxis("Horizontal");
            float posV = Input.GetAxis("Vertical");

            Vector3 movimiento = new Vector3(posH, 0.0f, posV);

            rb.AddForce(movimiento * velocidad);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("mono"))
        {
            other.gameObject.SetActive(false);
            contador = contador + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Contador: " + contador.ToString();
        if (contador >= 4)
        {
            winText.text = "Ganaste!!";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("tablero"))
        {
            winText.text = "Perdiste?!! :(";
            Invoke("QuitGame", 1f);
        }
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
