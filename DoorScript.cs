using System.Collections;
using UnityEngine;

// Komponent som lar spilleren åpne og lukke dører med "E"‑tasten.
public class DoorScript : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f;

    private bool _isOpen;
    private Quaternion _closedRotation;
    private Quaternion _openRotation;
    private Coroutine _rotationRoutine;

    private void Awake()
    {
        _closedRotation = transform.rotation;

        Vector3 euler = transform.eulerAngles;
        _openRotation = Quaternion.Euler(euler.x, euler.y + openAngle, euler.z);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleDoor();
        }
    }

    // starter coroutine som roterer døren
    private void ToggleDoor()
    {
        if (_rotationRoutine != null)
            StopCoroutine(_rotationRoutine);

        _rotationRoutine = StartCoroutine(RotateDoor());
    }

    // roterer døren fra dens nåværende vinkel til åpen/lukket vinkel, avhengig av tilstanden 
    private IEnumerator RotateDoor()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = _isOpen ? _closedRotation : _openRotation;
        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime * openSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, progress);
            yield return null;
        }

        transform.rotation = targetRotation;
        _isOpen = !_isOpen;

        _rotationRoutine = null;
    }
}
