using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Boss_Cutscene : MonoBehaviour
{
    public VideoPlayer cutscenePlayer;
    public VideoClip cutscene;
    public RawImage rawImg;
    public RenderTexture renderTex;
    public GameObject cutsceneCam;
    public GameObject player;
    public GameObject bossRestarter;
    public Animator pitchBlack;

    public Victim[] bosses;
    public HealthBar[] healthbars;

    private Player _playerScript;
    private Movement _movementScript;
    private Inventory _inventoryScript;
    private bool _isPaused;

    // Start is called before the first frame update
    void Start()
    {
        _playerScript = player.GetComponent<Player>();
        _movementScript = player.GetComponent<Movement>();
        _inventoryScript = player.GetComponent<Inventory>();
        cutscenePlayer.loopPointReached += EndReached;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPaused)
            {
                cutscenePlayer.Pause();

                _isPaused = true;
            }
            else
            {
                cutscenePlayer.Play();
                _isPaused = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _playerScript.enabled = false;
            _movementScript.enabled = false;
            _inventoryScript.enabled = false;

            rawImg.texture = renderTex;
            cutscenePlayer.clip = cutscene;
            cutscenePlayer.targetTexture = renderTex;
            cutsceneCam.SetActive(true);
            cutscenePlayer.Play();
        }
    }

    void EndReached(VideoPlayer video)
    {
        cutsceneCam.SetActive(false);
        StartCoroutine(UnfadeBlack(1.5f));

        for (int i =0; i< bosses.Length; i++)
        {
            bosses[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < healthbars.Length; i++)
        {
            healthbars[i].gameObject.SetActive(true);
        }
    }

    private IEnumerator UnfadeBlack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        pitchBlack.GetComponent<Animator>().SetBool("faded", false);
        _playerScript.enabled = true;
        _movementScript.enabled = true;
        _inventoryScript.enabled = true;
        bossRestarter.SetActive(true);
        Destroy(gameObject);
    }
}
