using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {
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
    public float y;

    [Header("Visuals")]
    public Sprite[] sprites;

    void Start() {
        sceneController = GameObject.Find("SceneController");
        sceneCtrl = sceneController.GetComponent<SceneController>();

        InvokeRepeating("Spawn", interval, interval);
    }

    void Update() {
        if (sceneCtrl.timeRemaining <= 0 || sceneCtrl.remainLives <= 0) {
            CancelInvoke();
        }
    }

    private void Spawn() {
        GameObject instance = Instantiate(prefab);
        instance.transform.position = new Vector2(Random.Range(minimumX, maximumX), y);
        instance.transform.SetParent(transform);

        if (OnObjectSpawned != null) {
            OnObjectSpawned(instance.GetComponent<CuttableObject>());
        }

        Sprite randomSprite = sprites[Random.Range(0, sprites.Length)];
        instance.GetComponent<SpriteRenderer>().sprite = randomSprite;
    }
}
