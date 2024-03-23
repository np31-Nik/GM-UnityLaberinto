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

    public AudioSource myAudioSource;
    public AudioClip gemSound;
    public AudioClip victorySound;
    public AudioClip defeatSound;
    public AudioClip movingSound1;

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

            if(!myAudioSource.isPlaying){
                //myAudioSource.PlayOneShot(movingSound1,0.5f);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("collectable"))
        {
            int gemValue = other.gameObject.GetComponent<Gem>().gemValue;
            //Debug.Log("gemValue: "+gemValue + ", name: "+other.gameObject.name);
            other.gameObject.SetActive(false);
            contador = contador + gemValue;
            SetCountText();
            myAudioSource.PlayOneShot(gemSound,1);
        }
    }

    void SetCountText()
    {
        countText.text = "Contador: " + contador.ToString();
        if (contador >= 4)
        {
            winText.text = "Ganaste!!";
            myAudioSource.PlayOneShot(victorySound,1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            winText.text = "Perdiste?!! :(";
            myAudioSource.PlayOneShot(defeatSound,1);
            Invoke("QuitGame", 3f);
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
