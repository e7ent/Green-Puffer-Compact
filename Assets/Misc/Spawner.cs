using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GreenPuffer.Misc
{
    public class Spawner : MonoBehaviour
    {
        public GameObject[] prefabs;
        public float spawnMinTime, spawnMaxTime;
        public Dictionary<GameObject, Vector3> sizes = new Dictionary<GameObject, Vector3>();


        private IEnumerator Start()
        {
            while (true)
            {
                // random select prefab
                var prefab = prefabs[Random.Range(0, prefabs.Length)];

                // create object
                var newObject = Instantiate(prefab);

                // get bounds size
                if (sizes.ContainsKey(prefab) == false)
                {
                    var renderers = newObject.GetComponentsInChildren<Renderer>();
                    if (renderers.Length == 0)
                    {
                        yield return null;
                        continue;
                    }

                    var bounds = renderers[0].bounds;
                    for (int i = 1; i < renderers.Length; i++)
                        bounds.Encapsulate(renderers[i].bounds);

                    sizes.Add(prefab, bounds.size);
                }

                // set position
                var position = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0, 2), Random.Range(0.2f, 0.8f), 0));
                position.x += Mathf.Sign(position.x) * sizes[prefab].x;
                position.z = 0;

                newObject.transform.position = position;

                // wait some times
                yield return new WaitForSeconds(Random.Range(spawnMinTime, spawnMaxTime));
            }
        }
    }
}