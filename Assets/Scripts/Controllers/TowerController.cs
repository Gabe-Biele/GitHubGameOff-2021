using BieleStudios.GitHubGameOff.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BieleStudios.GitHubGameOff.Controllers
{
    public class TowerController : MonoBehaviour
    {
        [Header("Projectile Settings")]
        public bool HasProjectileAttack;
        public Projectile Projectile;
        public float SpawnRate;

        [Tooltip("Set to greater than 0 to override the default speed")]
        public float OverrideDefaultSpeed = -1;
        private float speed;

        private CircleCollider2D towerRange;
        private List<GameObject> enemiesInRange;
        private bool hasTarget { get; set; }
        private GameObject target;
        private int spawnCooldown = 0;

        // Start is called before the first frame update
        void Start()
        {
            towerRange = GetComponent<CircleCollider2D>();
            enemiesInRange = new List<GameObject>();

            if (OverrideDefaultSpeed == -1) speed = Projectiles.Get(Projectile).DefaultSpeed;
            else speed = OverrideDefaultSpeed;
        }


        private void FixedUpdate()
        {
            if (enemiesInRange.Count > 0)
            {
                FindTarget();
            }
            else hasTarget = false;

            if (HasProjectileAttack && hasTarget && spawnCooldown > SpawnRate * 50)
            {
                Vector3 position = transform.position;

                GameObject projectileObj = Instantiate(Resources.Load<GameObject>(Projectiles.Get(Projectile).ResourceLocation), transform, true);
                projectileObj.transform.position = position;


                Vector2 targetLocation = CalculateTargetPosition(target.GetComponent<EnemyController>(), target.transform.position);
                float rotRadians = Mathf.Atan2(position.y - targetLocation.y, position.x - targetLocation.x);

                projectileObj.transform.rotation = Quaternion.Euler(0, 0, (Mathf.Rad2Deg*rotRadians) - 180f);
                projectileObj.GetComponent<ProjectileController>().TargetDestination = targetLocation;

                projectileObj.GetComponent<ProjectileController>().TravelSpeed = speed;

                spawnCooldown = 0;
            }
            else spawnCooldown++;
        }


        // Update is called once per frame
        void Update()
        {

        }

        public Vector2 CalculateTargetPosition(EnemyController enemyController, Vector2 predictedLocation)
        {
            float time = Vector2.Distance(predictedLocation, transform.position) / speed;
            float distance = enemyController.GetPredictedDistance(time);

            Debug.Log(string.Format("Calculating target... Time To Hit: {0}   Distance: {1}", time, distance));
            
            Vector2 newPredictedLocation = enemyController.GetPredictedLocation(distance);
            if (Vector2.Distance(newPredictedLocation, predictedLocation) < 0.01f)
            {
                Debug.Log("Target Acquired! {" + newPredictedLocation.x + ", " + newPredictedLocation.y + "}");
                return newPredictedLocation;
            }
            else return CalculateTargetPosition(enemyController, newPredictedLocation);
        }

        public void FindTarget()
        {
            if (enemiesInRange.Count.Equals(1))
            {
                target = enemiesInRange[0];
                hasTarget = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            enemiesInRange.Add(collision.gameObject);
            Debug.Log(string.Format("Something exited trigger. There are now {0} enemies in range.", enemiesInRange.Count));
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            enemiesInRange.Remove(collision.gameObject);
            Debug.Log(string.Format("Something exited trigger. There are now {0} enemies in range.", enemiesInRange.Count));
        }
    }
}
