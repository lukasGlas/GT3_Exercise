using UnityEngine;
using PolyStang;

namespace DefaultNamespace
{
    public class CarLifecycle : CarController
    {
        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            // Initialization logic that should occur before the scene is active
            InitializeCarComponents();
        }
        
        // Start is called before the first frame update if the script is enabled
        private void Start()
        {
            // Initialization logic that depends on all game objects being initialized
        }

        // OnEnable is called when the object becomes enabled and active
        private void OnEnable()
        {
            // Logic to initialize when the object is activated
        }

        // OnDisable is called when the behaviour becomes disabled or inactive
        private void OnDisable()
        {
            // Logic for cleanup when the object is deactivated
        }

        // OnDestroy is called when the MonoBehaviour will be destroyed
        private void OnDestroy()
        {
            // Final cleanup logic before the object is completely destroyed
        }

        // Update is called once per frame
        private void Update()
        {
            // Regular game updates, such as input processing or non-physics related calculations
            GetInputs();
            AnimateWheels();
            WheelEffectsCheck();
            CarLightsControl();
        }

        // FixedUpdate is called at a fixed interval and is independent of frame rate
        private void FixedUpdate()
        {
            // Physics-related calculations
        }

        // LateUpdate is called once per frame, after Update has finished
        private void LateUpdate()
        {
            // Adjustments to the game scene based on updates (e.g., follow camera logic) 
            Move();
            Steer();
            BrakeAndDeacceleration();
            UpdateSpeedUI();
        }
    }
}