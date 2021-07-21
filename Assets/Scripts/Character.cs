using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    CharacterController controller;

    [Header("PlayerSettings")]
    [Space(2)]
    [Tooltip("Speed value must be between 1 and 6.")]
    [Range(1.0f, 6.0f)]
    public float speed;
    public float jumpSpeed;
    public float rotationSpeed;
    public float gravity;
    public int collectible;

    public int health;
    public int score;
    public int powerUp;

    private float timeBtwShots;
    public float startTimeBtwShots;
    public Rigidbody projectile2;


    Vector3 moveDirection;

    enum ControllerType { SimpleMove, Move };
    [SerializeField] ControllerType type;

    [Header("Weapon Settings")]
    public float projectileForce;
    public Rigidbody projectilePrefab;
    public Transform projectileSpawnPoint;

    PickUps collectibles;


    // Start is called before the first frame update
    void Start()
    {
        collectibles = GetComponent<PickUps>();
        try
        {
            controller = GetComponent<CharacterController>();
            
            controller.minMoveDistance = 0.0f;

            if (speed <= 0)
            {
                speed = 6.0f;

                Debug.LogWarning(name + ": Speed not set. Defaulting to " + speed);
            }

            if (jumpSpeed <= 0)
            {
                jumpSpeed = 6.0f;
               // throw new UnassignedReferenceException("Jump Problem");

                Debug.LogWarning(name + ": jumpSpeed not set. Defaulting to " + jumpSpeed);
            }

            if (rotationSpeed <= 0)
            {
                rotationSpeed = 10.0f;

                Debug.LogWarning(name + ": rotationSpeed not set. Defaulting to " + rotationSpeed);
            }

            if (gravity <= 0)
            {
                gravity = 6.0f;

                Debug.LogWarning(name + ": gravity not set. Defaulting to " + gravity);
            }

            if (projectileForce <= 0)
            {
                projectileForce = 10.0f;

                Debug.LogWarning(name + ": projectileForce not set. Defaulting to " + projectileForce);
            }

            if (!projectileSpawnPoint)
                Debug.LogWarning(name + ": Missing projectilePrefab.");

            if (!projectilePrefab)
                Debug.LogWarning(name + ": Missing projectileSpawnPoint.");

            moveDirection = Vector3.zero;

        }
        catch (NullReferenceException e)
        {
            Debug.LogWarning(e.Message);
        }
        catch(UnassignedReferenceException e)
        {
            Debug.LogWarning(e.Message);
        }
        catch(UnityException e)
        {
            Debug.LogWarning(e.Message);
        }
        catch(InvalidOperationException e)
        {
            Debug.LogWarning(e.Message);
        }
        //finally
        //{
        //    Debug.LogWarning("Always get called");
        //}
    }

    void Update()
    {

        switch (type)
        {

            case ControllerType.SimpleMove:

                //transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0);

                controller.SimpleMove(transform.forward * Input.GetAxis("Vertical") * speed);

                break;

            case ControllerType.Move:

                if (controller.isGrounded)
                {
                    moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

                    moveDirection *= speed;

                    moveDirection = transform.TransformDirection(moveDirection);

                    if (Input.GetButtonDown("Jump"))
                        moveDirection.y = jumpSpeed;
                }

                moveDirection.y -= gravity * Time.deltaTime;

                controller.Move(moveDirection * Time.deltaTime);

                break;
        }

        LinkedList<string> collectibles = new LinkedList<string>();
        collectibles.AddFirst("PowerUp");
        collectibles.AddFirst("Health");
        collectibles.AddFirst("Score");

        
      
        if (Input.GetButtonDown("Fire1") && (collectibles.Contains("PowerUp") == true))
        {        
                fire();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && (collectibles.Contains("Score") == true))
        {           
                KeepScore.Score += 100;       
        }

        if (Time.timeScale != 0)
        {
            if (timeBtwShots <= 0)
            {
                if (Input.GetKey(KeyCode.Alpha2) && (collectibles.Contains("Health") == true))
                {
                    Rigidbody temp = Instantiate(projectile2, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                    temp.AddForce(projectileSpawnPoint.forward * projectileForce, ForceMode.Impulse);
                    timeBtwShots = startTimeBtwShots;
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
        
    }

    public void fire()
    {

       // Debug.Log("Fired");

        if(projectileSpawnPoint && projectilePrefab)
        {
           // if(LinkedList.collectibles.contains("PowerUp"))
           // {
                // Make the projectile
                Rigidbody temp = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

                //Shoot Projectile
                temp.AddForce(projectileSpawnPoint.forward * projectileForce, ForceMode.Impulse);

                // Destroy Projectile after 2.0s
                Destroy(temp.gameObject, 2.0f);
           // }
        }
        
    }

    [ContextMenu("Reset Stats")]
    void ResetStats()
    {
        speed = 6.0f;
    }
}
