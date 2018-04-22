using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleClouds : MonoBehaviour
{
    public CircleCollider2D safeZone;
    ParticleSystem ps;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> outside = new List<ParticleSystem.Particle>();

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleTrigger()
    {
        // get the particles which matched the trigger conditions this frame
        int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
        int numOutside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Outside, outside);

        // iterate through the particles which entered the trigger and make them red
        for (int i = 0; i < numInside; i++)
        {
            ParticleSystem.Particle p = inside[i];
            float distance = (p.position - transform.position).magnitude;
            if (distance > 0.8f)
                p.startColor = new Color32(255, 255, 255, 200);
            else if (distance > 0.6f)
                p.startColor = new Color32(255, 255, 255, 160);
            else if (distance > 0.4f)
                p.startColor = new Color32(255, 255, 255, 80);
            else if (distance > 0.4f)
                p.startColor = new Color32(255, 255, 255, 40);
            else
                p.startColor = new Color32(255, 255, 255, 0);

            //float toconvert = distance / safeZone.radius;
            //byte converted = (byte)(toconvert * 255);
            //Debug.Log("converted value : " + converted);
            //p.startColor = new Color32(255, 255, 255, (byte)(255 * (distance / safeZone.radius)));
            //p.startColor = new Color32(255, 255, 255, converted);
            inside[i] = p;
        }

        // iterate through the particles which exited the trigger and make them green
        for (int i = 0; i < numOutside; i++)
        {
            ParticleSystem.Particle p = outside[i];
            p.startColor = new Color32(255, 255, 255, 255);
            outside[i] = p;
        }

        // re-assign the modified particles back into the particle system
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Outside, outside);
    }
}
