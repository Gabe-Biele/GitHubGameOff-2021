using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BieleStudios.GitHubGameOff.Controllers
{
    public class LevelController : MonoBehaviour
    {
        public GameObject Spawnpoint;
        public List<GameObject> Waypoints;

        private int spawnLimit = 20;
        private int totalSpawned = 0;
        private int spawnRate = 150;
        private int spawnCooldown = 0;

        // Start is called before the first frame update
        void Start()
        {

        }

        void FixedUpdate()
        {
            if(spawnCooldown > spawnRate && totalSpawned <= spawnLimit)
            {
                GameObject monsterObj = Instantiate(Resources.Load<GameObject>("Monsters/Larva"));
                monsterObj.transform.position = Spawnpoint.transform.position;

                spawnCooldown = 0;
                totalSpawned++;
                Debug.Log(spawnCooldown);
            }
            else spawnCooldown++;
        }
    }
}
