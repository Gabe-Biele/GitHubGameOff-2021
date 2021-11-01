using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BieleStudios.GitHubGameOff.Controllers
{
    public class ProjectileController : MonoBehaviour
    {
        public Vector2 TargetDestination;
        public TravelMode TravelMode;

        public float TravelSpeed = 1;
        public bool HasAfterEffect;
        public GameObject AfterEffect;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector2 velocity = Vector2.right * TravelSpeed;
            transform.Translate(velocity * Time.fixedDeltaTime);
        }

        public float GetPredictedTimeToHit(float distance)
        {
            return distance / TravelSpeed;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (HasAfterEffect)
            {
                GameObject.Instantiate(AfterEffect, transform.position, Quaternion.identity);
            }
            Destroy(transform.gameObject);
        }
    }

    public enum TravelMode { Simple, Tracking, Predict };
}
