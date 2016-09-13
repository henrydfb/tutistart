using UnityEngine;
using System.Collections;

public class SandBackController : MonoBehaviour {

    private ParticleSystem parSys;

	// Use this for initialization
	void Start () {
        
	}

    public void Emit(Vector3 pos)
    {
        ParticleSystem.EmitParams emitOverride = new ParticleSystem.EmitParams();
        parSys = GetComponent<ParticleSystem>();

        emitOverride.position = pos - transform.position;

        for (int i = 0; i < 10; i++)
        {
            emitOverride.velocity = new Vector3(1 - 2 * Random.Range(0.0f, 1.0f), 1 - 2 * Random.Range(0.0f, 1.0f));
            emitOverride.startLifetime = Random.Range(0.1f, 0.3f);
            parSys.Emit(emitOverride, 1);
        }
    }
}
