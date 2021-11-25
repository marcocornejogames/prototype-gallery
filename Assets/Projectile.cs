using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
	//Custom Methods _______________________________________________
	public void PredictTrajectory(Vector3 force)
	{
		TrajectoryPrediction.Instance.Predict(force, gameObject);
	}

	public void TogglePredictionRendering(bool isOn)
    {
		TrajectoryPrediction.Instance.ToggleLineRenderer(isOn);
    }
}
