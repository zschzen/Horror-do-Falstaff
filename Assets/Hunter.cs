using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class Hunter : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;
        public Player ply;

        public Maestria maestro;

        public string cState = "Strolling";

        public Transform[] moveSpots;
        public int randomSpot;

        public Vector3 distance => target.position - transform.position;

        public AudioSource[] aud = new AudioSource[2];

        void Awake()
        {
            //Create the AudioSource components that we will be using
            aud[0] = gameObject.AddComponent<AudioSource>();
            aud[1] = gameObject.AddComponent<AudioSource>();

            aud[0].loop = true;
            aud[1].loop = true;
        }

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            ply = FindObjectOfType<Player>();
            target = ply.transform;
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            aud[0].clip = Resources.Load<AudioClip>("Audio/chorus");
            aud[0].loop = true;
            aud[0].volume = 0.2256f;

            aud[1].clip = Resources.Load<AudioClip>("Audio/gelado");
            aud[1].loop = false;
            aud[1].playOnAwake = false;

            maestro = FindObjectOfType<Maestria>();

            agent.updateRotation = false;
            agent.updatePosition = true;

            randomSpot = 0; //Random.Range(0, moveSpots.Length);
        }


        private void Update()
        {
            if (cState == "Strolling")
            {
                Vector3 _target = moveSpots[randomSpot].position;

                Vector3 targetDir = _target - transform.position;

                // The step size is equal to speed times frame time.
                float step = 2f * Time.deltaTime;

                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
                Debug.DrawRay(transform.position, newDir, Color.red);

                // Move our position a step closer to the target.
                transform.rotation = Quaternion.LookRotation(newDir);

                agent.SetDestination(_target);

                if (ply.isLost && distance.z < 4.5f && distance.x < 4.5f)
                {
                    cState = "Hunting";
                    aud[0].Stop();
                    aud[1].Play();
                    maestro.NewSoundtrack("Audio/run");
                }
                else if (ply.isSafe)
                {
                    if (cState != "Strolling")
                    {
                        cState = "Strolling";
                    }
                }
            }
            else if (cState == "Hunting")
            {

                agent.SetDestination(target.position);

                if (agent.remainingDistance > agent.stoppingDistance)
                    character.Move(agent.desiredVelocity, false, false);
                else
                    character.Move(Vector3.zero, false, false);

                if (distance.z < 1.5f && distance.x < 1.5f)
                {
                    // attack
                }
            }

            print(distance);
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
