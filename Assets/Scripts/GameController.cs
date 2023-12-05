using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    private GameObject mainCamera;
    [SerializeField] private GameObject spawnPoint;
    public bool move;

    public GameObject banner;
    [Range(1, 4)] public int bannerTimer;
    private Coroutine bannerDeactivation;

    public TextMeshProUGUI tokenCounter;
    int totalTokens;
    int currentTokens;

    private void Start()
    {
        mainCamera = Camera.main.gameObject;
        move = true;
        totalTokens = GameObject.Find("Tokens").transform.childCount;
        currentTokens = 0;
        banner.SetActive(false);
        tokenCounter.text = 0 + " / " + totalTokens;
    }

    public void CapturedToken()
    {
        currentTokens++;
        tokenCounter.text = currentTokens + " / " + totalTokens;
        banner.SetActive(true);
        
        if(bannerDeactivation != null)
        {
            StopCoroutine(bannerDeactivation);
            banner.SetActive(true);
        }

        bannerDeactivation = StartCoroutine("DeactivateBanner");
    }

    IEnumerator DeactivateBanner()
    {
        yield return new WaitForSeconds(bannerTimer);
        banner.SetActive(false);
    }

    public void SetSpawnpoint(GameObject spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }

    public GameObject GetSpawnpoint()
    {
        return spawnPoint;
    }

    public void MoveCamera(Vector3 playerPos)
    {
        int camX = Mathf.FloorToInt(playerPos.x / 35);
        int camY = Mathf.FloorToInt(playerPos.y / 20);

        mainCamera.transform.position = new Vector3(17.5f + camX * 35, 10 + camY * 20, -10);
    }
}
