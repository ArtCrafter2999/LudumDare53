using System;
using DanPie.Framework.AudioManagement;
using UnityEngine;

namespace LudumDare53.Boxes
{
    public class Shredder : MonoBehaviour
    {
        [SerializeField] private float power;
        [SerializeField] private float rotationPower;
        [SerializeField] private float damage;
        [SerializeField] private AudioClipDataProvider workSound;
        [SerializeField] private AudioClipDataProvider bladeSound;
        private AudioSourcesManager manager;

        private void Start()
        {
            manager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSourcesManager>();
            manager.GetAudioSourceController().Play(workSound.GetClipData(), true);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out BoxDamage boxDamage)) return;
            collision.rigidbody.velocity = Vector2.up*power;
            collision.rigidbody.angularVelocity = rotationPower;
            boxDamage.Damage(damage);
            manager.GetAudioSourceController().Play(bladeSound.GetClipData());
        }
    }
}