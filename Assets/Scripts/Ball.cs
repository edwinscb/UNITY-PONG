using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float velocidad = 30.0f;

    [SerializeField]
    private int golesIzquierda = 0;

    [SerializeField]
    private int golesDerecha = 0;

    [SerializeField]
    private Text contadorIzquierda;

    [SerializeField]
    private Text contadorDerecha;

    [SerializeField]
    private Text resultado;

    [SerializeField]
    private GameObject center;

    [SerializeField]
    private Text temporizador;

    AudioSource fuenteDeAudio;

    [SerializeField]
    private AudioClip

            audioGol,
            audioRaqueta,
            audioRebote;

    [SerializeField]
    private float tiempo = 180;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;
        contadorIzquierda.text = golesIzquierda.ToString();
        contadorDerecha.text = golesDerecha.ToString();
        fuenteDeAudio = GetComponent<AudioSource>();
        resultado.enabled = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        velocidad = velocidad + 2 * Time.deltaTime;

        if (tiempo >= 0)
        {
            tiempo -= Time.deltaTime;
            temporizador.text = formatearTiempo(tiempo);
        }
        else
        {
            temporizador.text = "00:00";
            center.SetActive(false);
            if (golesIzquierda > golesDerecha)
            {
                resultado.text =
                    "¡Jugador Izquierda GANA!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
            }
            else if (golesDerecha > golesIzquierda)
            {
                resultado.text =
                    "¡Jugador Derecha GANA!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
            }
            else
            {
                resultado.text =
                    "¡EMPATE!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
            }
            resultado.enabled = true;
            Time.timeScale = 0;
        }
    }

    public void reiniciarBola(string direccion)
    {
        transform.position = Vector2.zero;
        velocidad = 30;

        if (direccion == "Derecha")
        {
            golesDerecha++;
            contadorDerecha.text = golesDerecha.ToString();
            if (!comprobarFinal())
            {
                GetComponent<Rigidbody2D>().velocity =
                    Vector2.right * velocidad;
            }
        }
        else if (direccion == "Izquierda")
        {
            golesIzquierda++;
            contadorIzquierda.text = golesIzquierda.ToString();
            if (!comprobarFinal())
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.left * velocidad;
            }
        }
        fuenteDeAudio.clip = audioGol;
        fuenteDeAudio.Play();
    }

    void OnCollisionEnter2D(Collision2D micolision)
    {
        int x = 0;
        if (micolision.gameObject.name == "Raqueta Izquierda")
        {
            x = 1;
            int y =
                direccionY(transform.position, micolision.transform.position);
            Vector2 direccion = new Vector2(x, y);
            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;
            fuenteDeAudio.clip = audioRaqueta;
            fuenteDeAudio.Play();
        }
        if (micolision.gameObject.name == "Raqueta Derecha")
        {
            x = -1;
            int y =
                direccionY(transform.position, micolision.transform.position);
            Vector2 direccion = new Vector2(x, y);
            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;
            fuenteDeAudio.clip = audioRaqueta;
            fuenteDeAudio.Play();
        }
        if (
            micolision.gameObject.name == "Arriba" ||
            micolision.gameObject.name == "Abajo"
        )
        {
            fuenteDeAudio.clip = audioRebote;
            fuenteDeAudio.Play();
        }
    }

    int direccionY(Vector2 posicionBola, Vector2 posicionRaqueta)
    {
        if (posicionBola.y > posicionRaqueta.y)
        {
            return 1;
        }
        else if (posicionBola.y < posicionRaqueta.y)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    bool comprobarFinal()
    {
        if (golesIzquierda == 5)
        {
            resultado.text =
                "¡Jugador Izquierda GANA!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
            resultado.enabled = true;
            Time.timeScale = 0;
            center.SetActive(false);
            return true;
        }
        else if (golesDerecha == 5)
        {
            resultado.text =
                "¡Jugador Derecha GANA!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
            resultado.enabled = true;
            Time.timeScale = 0;
            center.SetActive(false);
            return true;
        }
        else
        {
            return false;
        }
    }

    string formatearTiempo(float tiempo)
    {
        string minutos = Mathf.Floor(tiempo / 60).ToString("00");
        string segundos = Mathf.Floor(tiempo % 60).ToString("00");
        return minutos + ":" + segundos;
    }
}
