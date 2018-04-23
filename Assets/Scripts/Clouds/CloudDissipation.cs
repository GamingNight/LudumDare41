using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDissipation : MonoBehaviour {

    public ParticleSystem particleSystem1;
    public ParticleSystem particleSystem2;

    public void DeactivateParticleLoop() {
        ParticleSystem.MainModule main1 = particleSystem1.main;
        main1.loop = false;
        main1.simulationSpeed = 10;
        ParticleSystem.MainModule main2 = particleSystem2.main;
        main2.loop = false;
        main2.simulationSpeed = 10;
    }
}
