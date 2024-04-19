using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public static StartPoint Instance { get; private set; }
    public Animator Anim { get; private set; }
    [SerializeField] private Transform spawnPos;
    private bool isAnimationFinished;
    private GameObject playerPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void OnEnable()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }
    private void Start()
    {
        Anim = GetComponent<Animator>();
        isAnimationFinished = true;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == (int)LayerIndex.Player && isAnimationFinished)
        {
            isAnimationFinished = false;
            Anim.SetTrigger("hit");
        }
    }
    private void OnGameStateChanged(GameState gameState)
    {
        if (gameState == GameState.Start)
        {
            playerPrefab = LevelManager.Instance.PlayerPrefab;
            if (playerPrefab != null)
            {
                Anim.SetTrigger("spawn");
                GameManager.Instance.SwitchGameState(GameState.Progess);
                GameManager.OnGameStateChanged -= OnGameStateChanged;
            }
        }
    }
    public void TriggerAnimationFinished() => isAnimationFinished = true;
    public void SpawnPlayer()
    {
        GameObject player = Instantiate(playerPrefab, spawnPos.position, Quaternion.identity);
        player.SetActive(true);
    }
}
