using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FotoFinish : MonoBehaviour
{
    /*
     *  Erstelle eine coole Kamera-Einstellung für das Ende des Rennens,
     *  indem du mit der switchCamera()-Methode die Kamera wechselst und
     *  das Rennen in Slow-Motion abspielen lässt, sobald ein Auto den
     *  Collider an diesem GameObject berührt!
     *
     *  Experimentiere auch mit verschiedenen Varianten von Slow-Motion.
     *  Zum Beispiel, indem du die Zeitskala über einen bestimmten Zeitraum
     *  kontinuierlich veränderst oder das Spiel für einige Momente im
     *  Standbild lässt, kurz bevor die Autos die Ziellinie überqueren.
     *  Programmiere diese zeitlichen Verläufe mit geeigneten Mitteln!
     */

    [SerializeField] private Camera finishLineCamera;
    [SerializeField] private Camera carCamera;

    //Musterlösung, löschen!
    private void OnTriggerEnter(Collider other)
    {
        switchCamera();
        Time.timeScale = 0.1f;
        //StartCoroutine("slowMotionCoroutine");
    }

    //Musterlösung, löschen!
    private IEnumerator slowMotionCoroutine()
    {
        float timer = 0;
        Time.timeScale = 0;
        while (timer < 3)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1;
    }


    private void switchCamera()
    {
        finishLineCamera.enabled = true;
        carCamera.enabled = false;
    }
}
