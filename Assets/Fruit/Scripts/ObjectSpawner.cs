using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public delegate void ObjectSpawnedHandler(CuttableObject obj);
    public event ObjectSpawnedHandler OnObjectSpawned;

    private GameObject sceneController;
    private SceneController sceneCtrl;

    [Header("Target")]
    public GameObject prefab;

    [Header("Gameplay")]
    public float interval;
    public float minimumX;
    public float maximumX;
    public float minY;
    public float maxY;

    [Header("Visuals")]
    public Sprite[] sprites;

    void Start()
    {
        sceneController = GameObject.Find("SceneController");
        sceneCtrl = sceneController.GetComponent<SceneController>();

        InvokeRepeating("Spawn", interval, interval);
    }

    void Update()
    {
        if (sceneCtrl.timeRemaining <= 0 || sceneCtrl.remainLives <= 0)
        {
            CancelInvoke();
        }
    }

    private void Spawn()
    {
        GameObject instance = Instantiate(prefab);
        instance.transform.position = new Vector2(Random.Range(minimumX, maximumX), Random.Range(minY, maxY));
        instance.transform.SetParent(transform);

        if (OnObjectSpawned != null)
        {
            OnObjectSpawned(instance.GetComponent<CuttableObject>());
        }

        int fruitIndex = Random.Range(0, sprites.Length);
        Sprite randomSprite = sprites[fruitIndex];
        instance.GetComponent<SpriteRenderer>().sprite = randomSprite;
        if (sprites.Length > 1 && instance.TryGetComponent(out CuttableObject co))
        {
            co.fruitIndex = fruitIndex;
        }
    }
}
