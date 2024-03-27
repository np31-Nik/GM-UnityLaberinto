using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float velocidad;
    public TextMeshProUGUI countText;

    private Rigidbody rb;
    private int contador;
    private bool isAndroid;

    public AudioSource myAudioSource;
    public AudioClip gemSound;
    public AudioClip victorySound;
    public AudioClip defeatSound;
    public AudioClip movingSound1;
    public GameObject victoryScreen;
    public GameObject defeatScreen;
    public GameObject sueloController;
    public GameObject mazeGenerator;

    private bool gameEnded;

    public void SetSuelo(GameObject suelo){
        sueloController = suelo;
        Debug.Log("sueloController: "+sueloController);
    }
    void Start()
    {
        gameEnded = false;
        rb = GetComponent<Rigidbody>();
        contador = 0;
        SetCountText();

#if UNITY_ANDROID
        isAndroid = true;
#else
        isAndroid = false;
#endif
    }

    void FixedUpdate()
    {
        if(!gameEnded){
            if (isAndroid){
                // Get input from accelerometer
                float posH = Input.acceleration.x;
                float posV = Input.acceleration.y;

                Vector3 movimiento = new Vector3(posV, 0.0f, posH);

                rb.AddForce(movimiento * velocidad);
            }
            else
            {
                // Get input from arrow keys
                float posH = Input.GetAxis("Horizontal");
                float posV = Input.GetAxis("Vertical");

                Vector3 movimiento = new Vector3(posH, 0.0f, posV);
                //rb.AddForce(movimiento);
                rb.AddForce(movimiento * velocidad);

                if(!myAudioSource.isPlaying){
                    //myAudioSource.PlayOneShot(movingSound1,0.5f);
                }
            }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(!gameEnded){
            if (other.gameObject.CompareTag("collectable")){
                int gemValue = other.gameObject.GetComponent<Gem>().gemValue;
                //Debug.Log("gemValue: "+gemValue + ", name: "+other.gameObject.name);
                other.gameObject.SetActive(false);
                contador = contador + gemValue;
                SetCountText();
                myAudioSource.PlayOneShot(gemSound,1);
                                mazeGenerator.GetComponent<MazeGenerator>().gemsPicked += 1;

            }else if (other.gameObject.CompareTag("portal")){
                gameEnded = true;
                sueloController.GetComponent<SueloController>().gameEnded=true;
                myAudioSource.PlayOneShot(victorySound,1);
                Time.timeScale = 0.01f;
                Time.fixedDeltaTime = 0.02F * Time.timeScale;
                victoryScreen.SetActive(true);
                victoryScreen.GetComponent<EndGamePanel>().score.text += contador;
            }
        }
        
    }

    void SetCountText()
    {
        countText.text = "Score: " + contador.ToString();
    }

    private void OnTriggerExit(Collider other)
    {
        if(!gameEnded){
            if (other.gameObject.CompareTag("DeadZone"))
            {
                gameEnded = true;
                sueloController.GetComponent<SueloController>().gameEnded=true;
                myAudioSource.PlayOneShot(defeatSound,1);
                Time.timeScale = 0.1f;
                Time.fixedDeltaTime = 0.02F * Time.timeScale;
                defeatScreen.SetActive(true);
                defeatScreen.GetComponent<EndGamePanel>().score.text += contador;
                //Invoke("QuitGame", 3f);
            }
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
