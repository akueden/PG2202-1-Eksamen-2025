using UnityEngine;
public class RayHitScript : MonoBehaviour
{
    private Material material;
    private Color originalColor;
    public float farDistance = 10;
    public Color rayHitSomethingNearColor = new Color(1, 0, 0);
    public Color rayHitSomethingFarColor = new Color(1, 1, 0);

    //Henter og lagrer objektets materiale for å kunne endre fargen senere
    //Lagrer den opprinnelige fargen til objektet.
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        originalColor = material.color;
    }

    //sender ut en stråle -> hvis strålen treffer endres fragen basert på avstand
    void Update()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo))
        {
            if (hitInfo.distance >= farDistance)
            {
                material.color = rayHitSomethingFarColor;
            }
            else
            {
                material.color = rayHitSomethingNearColor;
            }
        }
        else
        {
            material.color = originalColor;
        }
    }
}