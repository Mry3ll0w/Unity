using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerMovimiento : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocidad;
    private Rigidbody rb;
    public Camera camara;
    private Vector3 offset;
    private int estrellas = 0;
    private int vidas = 2;
    public Text textoEstrella;
    public Text textoVida;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        offset = camara.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float movHorizontal = Input.GetAxis("Horizontal");
        float movVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movHorizontal, 0.0f, movVertical);

        rb.AddForce(movimiento * velocidad);
        camara.transform.position = this.transform.position + offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EstrellaPremio"))
        {
            //other.gameObject.SetActive(false);
            Debug.Log("Ha tocado una estrella");
            Destroy(other.gameObject);
            estrellas++;
            textoEstrella.text = "Estrellas: " + estrellas;

            if (estrellas == 6)
                SceneManager.LoadScene("dos", LoadSceneMode.Single);            
        }

        if (other.gameObject.CompareTag("malicia"))
        {
            Debug.Log("Ha tocado un obstáculo");
            Destroy(other.gameObject);
            vidas--;
            textoVida.text = "Vidas: " + vidas;

            if (vidas == 0)
                SceneManager.LoadScene("muerte", LoadSceneMode.Single);
        }

        if (other.gameObject.CompareTag("victoria"))
            SceneManager.LoadScene("victoria", LoadSceneMode.Single);
    }
}
