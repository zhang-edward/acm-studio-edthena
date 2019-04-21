﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int maxPlatforms = 3;
    public LevelController level;
    private bool won = false;
    private bool lost = false;
    private bool stage3 = false;
    private bool stage2 = false;
    public GameObject projectile;
    public int maxProjectiles = 21;
    public GameObject sideProjectile;
    public int maxSideProjectiles = 4;
    public GameObject target;
    public GameObject winText;
    public GameObject loseText;
    public AudioSource beginStage;
    public AudioSource winStage;
    public AudioSource loseStage;
    // Start is called before the first frame update
    void Start()
    {
        level.OnLevelLoaded += OnLoad;
        level.OnStageOneCompletion += OnStageTwo;
        level.OnStageTwoCompletion += OnStageThree;
        level.OnLevelCompleted += OnWin;
        level.OnLevelFailed += OnLose;
        beginStage.Play();
    }

    void OnLoad()
    {
        print("Hello");
    }

    void OnStageTwo()
    {
        stage2 = true;
        beginStage.Play();
        Transform transform = GameObject.FindGameObjectWithTag("Boss").transform;
        Vector3 projPos = new Vector3(transform.position.x, transform.position.y, 0);
        target.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
        Instantiate(target);
        projPos = new Vector3(transform.position.x - 10, transform.position.y, 0);
        target.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
        Instantiate(target);
        projPos = new Vector3(transform.position.x + 10, transform.position.y, 0);
        target.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
        Instantiate(target);
        projPos = new Vector3(transform.position.x, transform.position.y - 4, 0);
        target.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
        Instantiate(target);
        projPos = new Vector3(transform.position.x + 10, transform.position.y - 4, 0);
        target.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
        Instantiate(target);
        projPos = new Vector3(transform.position.x - 10, transform.position.y - 4, 0);
        target.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
        Instantiate(target);
    }

    void OnStageThree()
    {
        stage3 = true;
        beginStage.Play();
        Transform transform = GameObject.FindGameObjectWithTag("Boss").transform;
        Vector3 projPos = new Vector3(transform.position.x - 10, transform.position.y, 0);
        target.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
        Instantiate(target);
        projPos = new Vector3(transform.position.x + 10, transform.position.y, 0);
        target.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
        Instantiate(target);
        projPos = new Vector3(transform.position.x, transform.position.y - 5, 0);
        target.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
        Instantiate(target);
        projPos = new Vector3(transform.position.x, transform.position.y + 5, 0);
        target.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
        Instantiate(target);
    }

    void OnWin()
    {
        won = true;
        Destroy(GameObject.FindGameObjectWithTag("Boss"));
        Destroy(GameObject.FindGameObjectWithTag("Projectile"));
        Destroy(GameObject.FindGameObjectWithTag("Projectile"));
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Wall");
        for (int i = 0; i < objects.Length; i++)
        {
            Destroy(objects[i]);
        }
        objects = GameObject.FindGameObjectsWithTag("Platform");
        for (int i = 0; i < objects.Length; i++)
        {
            Destroy(objects[i]);
        }
        objects = GameObject.FindGameObjectsWithTag("DestructibleProjectile");
        for (int i = 0; i < objects.Length; i++)
        {
            Destroy(objects[i]);
        }
        objects = GameObject.FindGameObjectsWithTag("DestructibleProjectile2");
        for (int i = 0; i < objects.Length; i++)
        {
            Destroy(objects[i]);
        }
        objects = GameObject.FindGameObjectsWithTag("Target");
        for (int i = 0; i < objects.Length; i++)
        {
            Destroy(objects[i]);
        }
        winText.gameObject.SetActive(true);
        winStage.gameObject.SetActive(true);
    }

    void OnLose()
    {
        lost = true;
        Destroy(GameObject.FindGameObjectWithTag("Boss"));
        Destroy(GameObject.FindGameObjectWithTag("Projectile"));
        Destroy(GameObject.FindGameObjectWithTag("Projectile"));
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Wall");
        for (int i = 0; i < objects.Length; i++)
        {
            Destroy(objects[i]);
        }
        objects = GameObject.FindGameObjectsWithTag("Platform");
        for (int i = 0; i < objects.Length; i++)
        {
            Destroy(objects[i]);
        }
        objects = GameObject.FindGameObjectsWithTag("DestructibleProjectile");
        for (int i = 0; i < objects.Length; i++)
        {
            Destroy(objects[i]);
        }
        objects = GameObject.FindGameObjectsWithTag("DestructibleProjectile2");
        for (int i = 0; i < objects.Length; i++)
        {
            Destroy(objects[i]);
        }
        objects = GameObject.FindGameObjectsWithTag("Target");
        for (int i = 0; i < objects.Length; i++)
        {
            Destroy(objects[i]);
        }
        loseText.gameObject.SetActive(true);
        loseStage.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        int numPlatforms = platforms.Length;
        if (numPlatforms > maxPlatforms)
        {
            Destroy(platforms[0]);
        }
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("DestructibleProjectile");
        int numProjectiles = projectiles.Length;
        if (numProjectiles < maxProjectiles && stage2 && !won && !lost)
        {
            float position = Random.value * 40;
            Transform transform = GameObject.FindGameObjectWithTag("Boss").transform;
            Vector3 projPos = new Vector3(position - 20, transform.position.y + 9, 0);
            projectile.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, new Quaternion(0, 0, 0, 0));
            Instantiate(projectile);
        }
        GameObject[] projectiles2 = GameObject.FindGameObjectsWithTag("DestructibleProjectile2");
        numProjectiles = projectiles2.Length;
        if(numProjectiles < maxSideProjectiles && stage3 && !won && !lost)
        {
            float position = Random.value * 20;
            Transform transform = GameObject.FindGameObjectWithTag("Boss").transform;
            Vector3 projPos = new Vector3(transform.position.x-15, transform.position.y + position-10, 0);
            sideProjectile.GetComponent<Transform>().transform.SetPositionAndRotation(projPos, Quaternion.Euler(0,0,90));
            Instantiate(sideProjectile);
        }
    }
}