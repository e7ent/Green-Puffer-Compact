using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GreenPuffer.Misc
{
    using URandom = UnityEngine.Random;
    class Spawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] prefabs;
        [SerializeField]
        private float spawnMinTime, spawnMaxTime;
        [SerializeField]
        private int initial, limit;

        private Dictionary<GameObject, Vector3> sizes = new Dictionary<GameObject, Vector3>();


        private IEnumerator Start()
        {
            for (int i = 0; i < initial; i++)
            {
                Spawn();
            }
            while (true)
            {
                Spawn();
                // wait some times
                yield return new WaitForSeconds(URandom.Range(spawnMinTime, spawnMaxTime));
            }
        }

        private void Spawn()
        {
            // random select prefab
            var prefab = prefabs[URandom.Range(0, prefabs.Length)];

            // create object
            var newObject = Instantiate(prefab);

            // get bounds size
            if (sizes.ContainsKey(prefab) == false)
            {
                var renderers = newObject.GetComponentsInChildren<Renderer>();
                if (renderers.Length == 0)
                {
                    throw new Exception();
                }

                var bounds = renderers[0].bounds;
                for (int i = 1; i < renderers.Length; i++)
                    bounds.Encapsulate(renderers[i].bounds);

                sizes.Add(prefab, bounds.size);
            }

            // set position
            var position = Camera.main.ViewportToWorldPoint(new Vector3(URandom.Range(0, 2), URandom.Range(0.2f, 0.8f), 0));
            position.x += Mathf.Sign(position.x) * sizes[prefab].x;
            position.z = 0;

            newObject.transform.position = position;
        }
    }
}