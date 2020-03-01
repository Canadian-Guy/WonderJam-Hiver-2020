using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Photon.Pun;

/*
 * Handles the loading screen transition.
 */
public class LoadingSceneHandler : MonoBehaviour
{
    [Tooltip("How long the handler should take to load the actual game")]
    [Range(1f, 10f)] public float WaitTime = 2.5f;

    void Awake()
    {
        StartCoroutine(DelayedLoading());
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }

    private IEnumerator DelayedLoading()
    {
        yield return new WaitForSecondsRealtime(WaitTime);

        if(PhotonNetwork.CurrentRoom.PlayerCount == 1) Leave();
        else
        {
            SceneManager.LoadScene(2);
        }
    }
}
