using System.Collections;
using UnityEngine;

public class LoadingRotationAnimation : MonoBehaviour
{
    Vector3 rotationEuler;

    Coroutine myRoutine;

    [SerializeField] bool counterClockWise = false;

    private void OnEnable()
    {
        myRoutine = StartCoroutine(RotateMe());
        transform.rotation = Quaternion.identity;
    }

    IEnumerator RotateMe()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            if (!counterClockWise)
            {
                rotationEuler -= Vector3.forward * 45; //increment 30 degrees every second
            }
            else
            {
                rotationEuler += Vector3.forward * 45; //increment 30 degrees every second
            }
            transform.rotation = Quaternion.Euler(rotationEuler);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(myRoutine);
    }
}
