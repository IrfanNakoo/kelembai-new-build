/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace VSX.UniversalVehicleCombat
{
    public class spawnOnInput2 : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] protected List<WaveController> waveControllers = new List<WaveController>();
        public List<WaveController> WaveControllers { get { return waveControllers; } }

        [SerializeField] protected bool loopWaves = false;
        [SerializeField] protected bool autoSpawnAllWaves = false;
        [SerializeField] protected bool spawnOnInput = false;

        protected int lastSpawnedWaveIndex = -1;
        protected bool wavesDestroyed = false;

        [Header("Events")]
        public UnityEvent onWavesDestroyed;

        private bool itemSpawned = false; // Initialize itemSpawned here

        // Code for accessing the skill input action
        public InputActionReference skill;

        private void OnEnable()
        {
            skill.action.started += SkillStarted;
        }

        private void OnDisable()
        {
            skill.action.started -= SkillStarted;
        }

        private void SkillStarted(InputAction.CallbackContext context)
        {
            Debug.Log("Skill activated");
            // Call the method for handling input
            OnInput();

        }

        public virtual void OnInput()
        {
            if (!itemSpawned)
            {
                // Check if the spawnOnInput flag is set and if it is, spawn waves
                if (spawnOnInput)
                {
                    //SpawnAllWaves();
                    itemSpawned = true;
                    Debug.LogWarning("have spawn on this time");
                    SpawnNextWave();
                }
                else
                {
                    Debug.LogWarning("Spawn on input is not enabled.");
                }
            }
            else
            {
                Debug.LogWarning("An item has already been spawned.");
            }
        }
        
        public virtual void SpawnNextWave()
        {
            int nextWaveSpawnIndex = lastSpawnedWaveIndex + 1;
            if (nextWaveSpawnIndex >= waveControllers.Count)
            {
                if (loopWaves)
                {
                    //ResetWaves();
                    nextWaveSpawnIndex = 0;
                }
                else
                {
                    return;
                }
            }

            SpawnWave(nextWaveSpawnIndex);
        }

        public virtual void SpawnWave(int index)
        {
            if (index < 0 || index >= waveControllers.Count) return;

            waveControllers[index].Spawn();
            lastSpawnedWaveIndex = index;
        }

        public virtual void SpawnAllWaves()
        {
            if (autoSpawnAllWaves)
            {
                for (int i = 0; i < waveControllers.Count; i++)
                {
                    SpawnWave(i);
                }

                lastSpawnedWaveIndex = waveControllers.Count - 1;
            }
        }
    }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VSX.UniversalVehicleCombat
{
    /// <summary>
    /// Manage a set of waves.
    /// </summary>
    public class spawnOnInput2 : MonoBehaviour
    {
        [Header("General")]
        [SerializeField]
        private List<WaveController> waveControllers = new List<WaveController>();
        public List<WaveController> WaveControllers { get { return waveControllers; } }

        private int lastSpawnedWaveIndex = -1;
        public int LastSpawnedWaveIndex { get { return lastSpawnedWaveIndex; } }

        [Header("Item Spawning")]
        [SerializeField]
        private GameObject itemPrefab; // The prefab of the item to spawn

        [Header("Events")]
        public UnityEvent onWavesDestroyed;
        public UnityEvent onItemSpawned; // Event triggered when an item is spawned

        private bool itemSpawned = false;

        protected virtual void Awake()
        {
            foreach (WaveController waveController in waveControllers)
            {
                waveController.onWaveDestroyed.AddListener(OnWaveDestroyed);
            }
        }

        public virtual void SpawnWave(int index)
        {
            if (index < 0 || index >= waveControllers.Count) return;

            waveControllers[index].Spawn();
            lastSpawnedWaveIndex = index;
        }

        public virtual void SpawnNextWave()
        {
            int nextWaveSpawnIndex = lastSpawnedWaveIndex + 1;
            if (nextWaveSpawnIndex >= waveControllers.Count)
            {
                // Handle looping logic here if needed
                return;
            }

            SpawnWave(nextWaveSpawnIndex);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                SpawnItem();
            }
        }

        void SpawnItem()
        {
            if (!itemSpawned)
            {
                if (itemPrefab != null)
                {
                    GameObject newItem = Instantiate(itemPrefab, transform.position, Quaternion.identity);
                    itemSpawned = true;
                    onItemSpawned.Invoke();
                }
                else
                {
                    Debug.LogWarning("Item prefab is not assigned.");
                }
            }
            else
            {
                Debug.LogWarning("An item has already been spawned.");
            }
        }

        /*
        public virtual void SpawnAllWaves()
        {
            // Implement spawning all waves if needed
        }

        public virtual void SpawnRandomWave()
        {
            // Implement spawning a random wave if needed
        }

        public virtual void ResetWaves()
        {
            // Implement resetting waves if needed
        }
        */

        protected virtual void OnWaveDestroyed()
        {
            // Implement wave destruction logic here
            // This method will be called when a wave is destroyed
        }
    }
}