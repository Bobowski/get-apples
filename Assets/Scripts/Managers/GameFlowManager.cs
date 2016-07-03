using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameFlowManager : MonoBehaviour
{
    // Singleton
    private static GameFlowManager instance;

    public static GameFlowManager Ins {
        get { return instance; }
    }

    public enum State
    {
        PreGame,
        Game,
        PostGame
    }

    [SerializeField]
    private State state = State.PreGame;
    [SerializeField]
    private Transform camera;
    [SerializeField]
    private Transform player;


    [SerializeField]
    private Text winMessage;
    [SerializeField]
    private Text loseMessage;

    void Awake ()
    {
        // Singleton assignment
        instance = this;
    }

    void Update ()
    {
        if (state == State.PreGame && Input.GetKey (KeyCode.Space)) {
            StartGame ();
        }

        if (state == State.PostGame && Input.GetKey (KeyCode.Space)) {
            ResetGame ();
        }
    }


    public void LoseGame ()
    {
        StartCoroutine ("DisplayMessage", loseMessage);
        player.GetComponent<Character> ().enabled = false;
        player.GetComponent<CharacterPhysics> ().enabled = false;
    }

    public void WinGame ()
    {
        StartCoroutine ("DisplayMessage", winMessage);
        player.GetComponent<Character> ().enabled = false;
        player.GetComponent<CharacterPhysics> ().enabled = false;
    }

    public void StartGame ()
    {
        state = State.Game;

        camera.GetComponent<CameraController> ().enabled = true;
        camera.GetComponent<ProtectCameraFromWallClip> ().enabled = true;

        player.GetComponent<Character> ().enabled = true;
    }

    private IEnumerator DisplayMessage (Text text)
    {
        text.gameObject.SetActive (true);

        for (float i = 0; i < 1.0f; i += 0.01f) {
            Color c = text.color;
            c.a = i;
            text.color = c;
            yield return new WaitForSeconds (0.01f);
        }

        state = State.PostGame;
    }

    private void ResetGame ()
    {
        Application.LoadLevel (Application.loadedLevel);
    }
}
