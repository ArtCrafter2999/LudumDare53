using System;
using DanPie.Framework.AudioManagement;
using LudumDare53.Leveling;
using UnityEngine;

namespace LudumDare53.Boxes
{
    [RequireComponent(typeof(Collider2D))]
    public class Shredder : MonoBehaviour
    {
        [SerializeField] private float power;
        [SerializeField] private float rotationPower;
        [SerializeField] private float damage;
        [SerializeField] private AudioClipDataProvider workSound;
        [SerializeField] private AudioClipDataProvider bladeSound;
        [SerializeField] private Animator animator;
        private Collider2D _collider;
        private AudioSourcesManager manager;

        private void Start()
        {
            manager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSourcesManager>();
            _collider = GetComponent<Collider2D>();
            DifficultyManager.DifficultyChanged.AddListener(OnDifficultyChanged);
        }

        private void OnDifficultyChanged()
        {
            animator.SetBool("IsEnabled", DifficultyManager.Difficulty > 0);
            _collider.enabled = DifficultyManager.Difficulty > 0;
            if(DifficultyManager.Difficulty > 0) manager.GetAudioSourceController().Play(workSound.GetClipData(), true);
            else manager.GetAudioSourceController().Stop();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (DifficultyManager.Difficulty <= 0) return;
            if (!collision.gameObject.TryGetComponent(out BoxDamage boxDamage)) return;
            
            collision.rigidbody.velocity = Vector2.up*power;
            collision.rigidbody.angularVelocity = rotationPower;
            boxDamage.Damage(damage);
            manager.GetAudioSourceController().Play(bladeSound.GetClipData());
        }
    }
}