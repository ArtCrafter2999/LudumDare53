using System.Collections;
using System.Collections.Generic;
using LudumDare53.Leveling;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LudumDare53.Environment
{
    public class CloudsGenerator : MonoBehaviour
    {
        [SerializeField] private int _firstLayerRendererOrder = -100;
        [SerializeField] private List<CloudLayer> _layers;

        private bool _isPaused;

        protected void OnEnable()
        {
            _isPaused = PauseManager.IsPaused;
            PauseManager.Pause.AddListener(OnPause);
            PauseManager.Resume.AddListener(OnResume);
        }

        protected void OnDisable()
        {
            PauseManager.Pause.RemoveListener(OnPause);
            PauseManager.Resume.RemoveListener(OnResume);
        }

        protected void Update()
        {
            if (_isPaused)
            {
                return;
            }

            foreach (CloudLayer layer in _layers) 
            {
                layer.GenerationCooldown -= Time.deltaTime;

                if (layer.GenerationCooldown <= 0f)
                {
                    layer.GenerationCooldown = layer.GenerationFrequency;
                    SpriteRenderer cloud = CreateCloud(layer);
                    float wayXEdge = layer.DestroyEdge.position.x + cloud.bounds.size.x / 2;
                    StartCoroutine(MoveCloud(cloud, wayXEdge, layer.LayerSpeed));
                }
            }
        }

        private void OnResume()
        {
            _isPaused = false;
        }

        private void OnPause()
        {
            _isPaused = true;
        }

        private SpriteRenderer CreateCloud(CloudLayer layer)
        {
            int instanceId = 0;
            do
            {
                instanceId = Random.Range(0, layer.CloudsOnLayer.Count);
            }
            while (
                layer.CloudsOnLayer.Count > 1 &&
                layer.PreviousCloudId >= 0 &&
                layer.PreviousCloudId == instanceId);

            layer.PreviousCloudId = instanceId;

           SpriteRenderer instance = Instantiate(
                layer.CloudsOnLayer[instanceId], 
                layer.SpawnEdge);

            instance.transform.localPosition
                = Vector3.left * instance.bounds.size.x / 2;

            instance.transform.Translate(
                Vector3.up * Random.Range(-layer.YPositinonScatter, layer.YPositinonScatter));
            
            float minScale = Mathf.Max(0, 1 - layer.ScaleScatter);
            float maxScale = Mathf.Max(0, 1 + layer.ScaleScatter);
            instance.transform.localScale 
                = layer.InitialScale * Random.Range(minScale, maxScale);

            instance.sortingOrder = _firstLayerRendererOrder + _layers.IndexOf(layer);

            return instance;    
        }

        private IEnumerator MoveCloud(SpriteRenderer cloud, float wayXEdge, float moveSpeed)
        {
            while (cloud.transform.position.x < wayXEdge)
            {
                if (!_isPaused)
                {
                    cloud.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                }
                yield return new WaitForEndOfFrame();
            }

            Destroy(cloud.gameObject);
        }
    }
}
