using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrajectoryPrediction : MonoBehaviour
{
    private Scene predicitonScene;
    private PhysicsScene predictionPhysicsScene;

    private Scene currentScene;
    private PhysicsScene currentPhysicsScene;

    void Start()
    {
        Physics.autoSimulation = false;

        currentScene = SceneManager.GetActiveScene();
        currentPhysicsScene = currentScene.GetPhysicsScene();

        CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        predicitonScene = SceneManager.CreateScene("Prediction", parameters);
        predictionPhysicsScene = predicitonScene.GetPhysicsScene();

    }

    private void FixedUpdate()
    {
        if(currentPhysicsScene.IsValid())
        {
            currentPhysicsScene.Simulate(Time.fixedDeltaTime);
        }
    }

}
