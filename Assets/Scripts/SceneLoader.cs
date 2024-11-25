using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /*  Obwohl Awaitable in dieser Unity-Version noch nicht verfügbar ist,
     *  kann Code asynchron ausgeführt werden. Ersetze LoadScene() mit
     *  LoadSceneAsync(), um das Laden der Szene auf einen Hintergrund-Thread
     *  zu verlagern.
     *  
     *  Gestalte vielleicht noch einen hübschen Ladebalken, der während dem
     *  Laden der Szene angezeigt und AsyncOperation.progress aktualisiert wird.
     */
    public void startLoading(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
