using UnityEngine;
public class ColliderScript : MonoBehaviour
{
    //lagrer de opprinnelige fargene
    private Color originalOwnColor;
    private Color originalOtherColor;

    //objektet kolliderer med et annet -> lagrer de orginale fargene og endrer de
    void OnCollisionEnter(Collision other)
    {
        originalOwnColor = gameObject.GetComponent<MeshRenderer>().material.color;
        originalOtherColor = other.gameObject.GetComponent<MeshRenderer>().material.color;
        gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0);
        other.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0);
    }

    //kollisjonen avsluttes -> fargene settes tilbake
    void OnCollisionExit(Collision other)
    {
        gameObject.GetComponent<MeshRenderer>().material.color = originalOwnColor;
        other.gameObject.GetComponent<MeshRenderer>().material.color = originalOtherColor;
    }

    //et objekt går inn i trigger-sonen -> lagrer de orginale fargene og endrer de
    void OnTriggerEnter(Collider other)
    {
        originalOwnColor = gameObject.GetComponent<MeshRenderer>().material.color;
        originalOtherColor = other.gameObject.GetComponent<MeshRenderer>().material.color;
        gameObject.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 1);
        other.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 1);
    }

    // forlater trigger-sonen -> fargene settes til deres orginale farge
    void OnTriggerExit(Collider other)
    {
        gameObject.GetComponent<MeshRenderer>().material.color = originalOwnColor;
        other.gameObject.GetComponent<MeshRenderer>().material.color = originalOtherColor;
    }
}