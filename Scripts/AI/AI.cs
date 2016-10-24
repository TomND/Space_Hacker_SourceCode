using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {
   NavMeshAgent      agent;
   public float      radius;
   public float      idleTime;
   private float     currentTime;
   private Animator  animator;
   public GameObject aiArea;
   public bool       dead;
   private Rigidbody rigid;
   public GameObject coin;


   void Awake()
   {
      foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
      {
         rb.isKinematic = true;
      }
   }

   // Use this for initialization
   void Start()
   {
      rigid    = GetComponent<Rigidbody>();
      agent    = GetComponent<NavMeshAgent>();
      animator = GetComponent<Animator>();
   }

// Update is called once per frame
   void Update()
   {
      if (dead)
      {
         //Die();
      }
      else
      {
         ManageMovement();
      }
   }

   void Die(PlayerControllerThirdPersonVR player)
   {
      animator.enabled = false;
      agent.enabled    = false;
      foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
      {
         rb.isKinematic = false;
         rb.AddForce(player.moveDirection * 100);
      }
   }

   void ManageMovement()
   {
      if (Vector3.Distance(transform.position, agent.destination) < 0.5f)
      {
         if (Time.time > currentTime + idleTime)
         {
            agent.destination = RandomNavPoint();
            animator.SetBool("Walk", true);
         }
         else
         {
            animator.SetBool("Walk", false);
         }
      }
   }

   Vector3 RandomNavPoint()
   {
      currentTime = Time.time;
      Vector3 randomDirection = Random.insideUnitSphere * radius;
      randomDirection += aiArea.transform.position;
      NavMeshHit hit;
      if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
      {
         return hit.position;
      }
      return transform.position;
   }

   void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.tag == "Player")
      {
         if ((other.gameObject.GetComponent<Animator>().GetBool("Roll") == true) && !dead)
         {
            dead = true;
            Instantiate(coin, transform.position, Quaternion.identity);
            Instantiate(coin, transform.position, Quaternion.identity);
                PlayerControllerThirdPersonVR player = other.gameObject.GetComponent<PlayerControllerThirdPersonVR>();
            GetComponent<SphereCollider>().isTrigger = true;
            Die(player);
            rigid.AddForce(player.moveDirection);
            }
				 else if ((other.gameObject.GetComponent<Animator>().GetBool("Roll") == false) && !dead){
                PlayerControllerThirdPersonVR player = other.gameObject.GetComponent<PlayerControllerThirdPersonVR>();
                player.animator.SetBool("Hit", true);
            }
      }
   }
}
