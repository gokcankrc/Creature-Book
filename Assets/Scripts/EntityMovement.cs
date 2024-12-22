using System.Collections;
using Ky;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityMovement : MonoBehaviour
{
    public Transform target;
    public bool active;
    public bool tired;
    private bool shaking;
    [ReadOnly] public bool attacking;

    [BoxGroup("Attack")] public AnimationCurve attackCurve;
    [BoxGroup("Attack")] public float attackDistance = 50;
    [BoxGroup("Attack")] public float attackDuration = 0.3f;
    [BoxGroup("Oscillation")] public float oscillationStart;
    [BoxGroup("Oscillation")] public float magnitude = 10f;
    [BoxGroup("Oscillation")] public float frequency = 1.4f;
    [BoxGroup("Oscillation")] public float tiredFrequency = 0.8f;
    [BoxGroup("Shake")] public float shakeDuration = 0.25f;
    [BoxGroup("Shake")] public float shakeMagnitude = 5f;
    private float shakeTimer;

    public void Activate()
    {
        oscillationStart = Random.value * 0.1f;
        active = true;
        tired = false;
        attacking = false;
    }

    public void Deactivate()
    {
        target.transform.localPosition = Vector3.zero;
        Halt();
    }

    public void Halt()
    {
        active = false;
        StopAllCoroutines();
    }

    private void Update()
    {
        if (!active) return;
        if (attacking) return;
        var freq = tired ? tiredFrequency : frequency;
        var yOffset = magnitude * Mathf.Sin((Time.time * freq + oscillationStart) * Mathf.PI);
        target.transform.localPosition = Vector3.up * yOffset;

        if (!shaking) return;
        shakeTimer -= Time.deltaTime;
        target.transform.localPosition += Random.insideUnitSphere * (shakeMagnitude * shakeTimer);

        if (shakeTimer > 0) return;
        shaking = false;
    }

    public void OnReadyToAct()
    {
        tired = false;
    }

    public void OnAttack()
    {
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        attacking = true;
        var offset = target.transform.localPosition;
        float timer = 0;
        while (timer < attackDuration)
        {
            timer += Time.deltaTime;
            var ratio = timer / attackDuration;
            var eval = attackCurve.Evaluate(ratio) * attackDistance;
            target.transform.localPosition = Vector3.right * eval + offset;
            yield return null;
        }

        target.transform.localPosition = Vector3.zero;
        attacking = false;
        tired = true;
    }

    public void OnDamageTaken()
    {
        shaking = true;
        shakeTimer = shakeDuration;
    }
}