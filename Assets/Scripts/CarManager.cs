using PolyStang;
using UnityEngine;

public class CarManager : MonoBehaviour
    {
        public CarController greenCar; // middle car
        public CarController blueCar; // left car
        public CarController redCar; // right car
        
        /* Where do these methods go?
            InitializeCarComponents();
            ProcessPlayerInputs();
            AnimateWheels();
            DisplayCarLights();
            MoveCarForward();
            UpdateSpeedUI();
         */
        
        // Awake is called when the script instance is being loaded
        private void Awake() // Initialization logic that should occur before the scene is active
        {
            // GREEN CAR
            greenCar.InitializeCarComponents();
            // BLUE CAR
            blueCar.InitializeCarComponents();
            // RED CAR
            redCar.InitializeCarComponents();
        }
        
        // Start is called before the first frame update if the script is enabled
        private void Start() // Initialization logic that depends on all game objects being initialized
        {
            // GREEN CAR

            // BLUE CAR
            
            // RED CAR
        }

        // OnEnable is called when the object becomes enabled and active
        private void OnEnable() // Logic to initialize when the object is activated
        {
            // GREEN CAR
            
            // BLUE CAR
            
            // RED CAR
        }

        // OnDisable is called when the behaviour becomes disabled or inactive
        private void OnDisable() // Logic for cleanup when the object is deactivated
        {
            // GREEN CAR
            
            // BLUE CAR
            
            // RED CAR
        }

        // OnDestroy is called when the MonoBehaviour will be destroyed
        private void OnDestroy() // Final cleanup logic before the object is completely destroyed
        {
            // GREEN CAR
            
            // BLUE CAR
            
            // RED CAR
        }

        // Update is called once per frame
        private void Update() // Regular game updates, such as input processing or non-physics related calculations
        {
            // GREEN CAR
            greenCar.ProcessPlayerInputs();
            greenCar.AnimateWheels();
            greenCar.DisplayCarLights();
            // BLUE CAR
            blueCar.ProcessPlayerInputs();
            blueCar.DisplayCarLights();
            // RED CAR
           redCar.ProcessPlayerInputs();
        }

        // FixedUpdate is called at a fixed interval and is independent of frame rate
        private void FixedUpdate() // Physics-related calculations
        {
            // GREEN CAR
            
            // BLUE CAR
            blueCar.AnimateWheels();
            blueCar.MoveCarForward();
            blueCar.UpdateSpeedUI();
            // RED CAR
            redCar.MoveCarForward();
        }

        // LateUpdate is called once per frame, after Update has finished
        private void LateUpdate() // Adjustments to the game scene based on updates (e.g., follow camera logic) 
        {
            // GREEN CAR
            greenCar.MoveCarForward();
            greenCar.UpdateSpeedUI();
            // BLUE CAR
            
            // RED CAR
            redCar.AnimateWheels();
            redCar.UpdateSpeedUI();

        }
    }