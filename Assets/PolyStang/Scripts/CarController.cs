using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

// SOURCE: https://assetstore.unity.com/packages/tools/physics/polystang-cc-269404?srsltid=AfmBOooN0-gV8ceI0IWfHWmN7OG8pHFUHlmj8BC5heNyGgHoPxXxMLi1

namespace PolyStang
{
    public class CarController : MonoBehaviour
    {
        public enum ControlMode 
        {
            Keyboard,
            Buttons
        };

        public enum Axel // used to identify front and rear wheels.
        {
            Front,
            Rear
        }

        [Serializable]
        public struct Wheel // wheel bits: all fields must be filled to make the wheel work properly.
        {
            public GameObject wheelModel;
            public WheelCollider wheelCollider;
            public GameObject wheelEffectObj;
            public ParticleSystem smokeParticle;
            public Axel axel;
            public GameObject skidSound;
            public int index;
        }

        public ControlMode control;

        [Header("Inputs")]
        public KeyCode brakeKey = KeyCode.Space;

        [Header("Accelerations and deaccelerations")]
        public float maxAcceleration = 30.0f;
        public float brakeAcceleration = 50.0f;
        public float noInputDeacceleration = 10.0f;

        [Header("Steering")]
        public float turnSensitivity = 1.0f;
        public float maxSteerAngle = 30.0f;

        [Header("Speed UI")]
        public TMP_Text speedText;
        public float UISpeedMultiplier = 4;

        [Header("Speed limit")]
        public float frontMaxSpeed = 200;
        public float rearMaxSpeed = 50;
        public float empiricalCoefficient = 0.41f;
        public enum TypeOfSpeedLimit
        {
            noSpeedLimit,
            simple,
            squareRoot
        };
        public TypeOfSpeedLimit typeOfSpeedLimit = TypeOfSpeedLimit.squareRoot;
        private float frontSpeedReducer = 1;
        private float rearSpeedReducer = 1;

        [Header("Skid")]
        public float brakeDriftingSkidLimit = 10f;
        public float lateralFrontDriftingSkidLimit = 0.6f;
        public float lateralRearDriftingSkidLimit = 0.3f;

        [Header("General")]
        public Vector3 _centerOfMass;

        public List<Wheel> wheels;

        float moveInput;
        float steerInput;

        private Rigidbody carRb;

        private CarLights carLights;
        private CarSounds carSounds;

        private bool finished = false;

        public void InitializeCarComponents()
        {
            carRb = GetComponent<Rigidbody>();
            carLights = GetComponent<CarLights>();
            carSounds = GetComponent<CarSounds>();
            carRb.centerOfMass = _centerOfMass;
        }

        public void ProcessPlayerInputs() // inputs.
        {
            if (control == ControlMode.Keyboard)
            {
                moveInput = Input.GetAxis("Vertical");
                steerInput = Input.GetAxis("Horizontal");
            }
        }

        public void ProcessMovement() // main vertical acceleration.
        {
            /*
            float forwardMovement = transform.position.z + 1 * moveInput;
            carRb.Move(new Vector3(transform.position.x, transform.position.y, forwardMovement), Quaternion.identity);
            return;
            */
            foreach (var wheel in wheels)
            {
                // rotational speed is proportional to radius * frequency: the empirical coefficient is around 0.41
                float currentWheelSpeed = empiricalCoefficient * wheel.wheelCollider.radius * wheel.wheelCollider.rpm;

                if (moveInput > 0 || currentWheelSpeed > 0) // when moving forwards
                {
                    if (currentWheelSpeed > frontMaxSpeed) // important check: it prevents the car from accelerating indefinetly
                    {
                        currentWheelSpeed = frontMaxSpeed;
                    }

                    // cases: different speed reducing technics
                    if (typeOfSpeedLimit == TypeOfSpeedLimit.noSpeedLimit)
                    {
                        frontSpeedReducer = 1;
                    }
                    else if (typeOfSpeedLimit == TypeOfSpeedLimit.simple)
                    {
                        frontSpeedReducer = (frontMaxSpeed - currentWheelSpeed) / frontMaxSpeed;
                    }
                    else if (typeOfSpeedLimit == TypeOfSpeedLimit.squareRoot)
                    {
                        frontSpeedReducer = Mathf.Sqrt(Mathf.Abs((frontMaxSpeed - currentWheelSpeed) / frontMaxSpeed));
                    }

                    // applying reduction
                    wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * frontSpeedReducer * Time.deltaTime;
                }
                else if (moveInput < 0 || currentWheelSpeed < 0) // when moving backwards
                {
                    if (currentWheelSpeed < -rearMaxSpeed) // important check: it prevents the car from accelerating indefinetly
                    {
                        currentWheelSpeed = -rearMaxSpeed;
                    }

                    // cases: different speed reducing technics
                    if (typeOfSpeedLimit == TypeOfSpeedLimit.noSpeedLimit)
                    {
                        rearSpeedReducer = 1;
                    }
                    else if (typeOfSpeedLimit == TypeOfSpeedLimit.simple)
                    {
                        rearSpeedReducer = (rearMaxSpeed + currentWheelSpeed) / rearMaxSpeed;
                    }
                    else if (typeOfSpeedLimit == TypeOfSpeedLimit.squareRoot)
                    {
                        rearSpeedReducer = Mathf.Sqrt(Mathf.Abs((rearMaxSpeed + currentWheelSpeed) / rearMaxSpeed));
                    }

                    // applying reduction
                    wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * rearSpeedReducer * Time.deltaTime;
                }
            }
        }

        public void AnimateWheels() // to animate wheels accordingly to the car speed.
        {
            foreach (var wheel in wheels)
            {
                Quaternion rot;
                Vector3 pos;
                wheel.wheelCollider.GetWorldPose(out pos, out rot);
                wheel.wheelModel.transform.position = pos;
                wheel.wheelModel.transform.rotation = rot;
            }

            AnimateWheelDrift();
        }

        public void DisplayCarLights() // controlling lights, through the specific script "CarSounds".
        {
            if (Input.GetKey(brakeKey)) // the red lights are activated when the brake is pressed
            {
                carLights.RearRedLightsOn();
            }
            else
            {
                carLights.RearRedLightsOff();
            }

            if (moveInput < 0f) // the rear white lights are activated when the player is pressing "S" or down arrow.
            {
                carLights.RearWhiteLightsOn();
            }
            else
            {
                carLights.RearWhiteLightsOff();
            }
        }

        public void UpdateSpeedUI(int roundedSpeed) // UI: speed update.
        {
            speedText.text = roundedSpeed.ToString();
        }
        
        public void ActivateSelf()
        {
            gameObject.SetActive(true);
        }

        public void DestroySelf()
        {
            Destroy(this.gameObject);
        }

        public void DeactivateOnFinishLineCrossing()
        {
            if (finished)
            {
                this.gameObject.SetActive(false);
            } 
        }

        public int calculateRoundedSpeed()
        {
            return (int)Mathf.Round(carRb.velocity.magnitude * UISpeedMultiplier);
        }

        /*  Angenommen, bei calculateRoundedSpeed() handelte es sich um eine etwas komplexere
         *  Berechnung, die viele Male pro Frame ausgef�hrt wird. Unter diesen Umst�nden k�nnte
         *  es aus Performance-Gr�nden sinnvoll sein, die Berechnung auf mehreren Threads parallel
         *  auszuf�hren. Welcher Multithreading Ansatz w�re hierf�r geeignet und warum?
         *
         *  Berechne roundedSpeed auf mehreren Threads parallel und zeige die berechneten Werte mit
         *  UpdateSpeedUI() im gleichen Frame an.
         *  Welche Methoden im Game-Loop sind f�r das Starten / Beenden der Berechnung sinnvoll?
         */
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("FinishLine"))
            {
                finished = true;
            }
        }

        private void MoveInput(float input) // used for touch controls.
        {
            moveInput = input;
        }

        private void SteerInput(float input) // used for touch controls.
        {
            steerInput = input;
        }


        private void Steer() // to rotate the front wheels, when steering.
        {
            foreach (var wheel in wheels)
            {
                if (wheel.axel == Axel.Front)
                {
                    var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                    wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
                }
            }
        }

        private void BrakeAndDeacceleration()
        {
            if (Input.GetKey(brakeKey)) // when pressing space, the brake is used.
            {
                foreach (var wheel in wheels)
                {
                    wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration;// * Time.deltaTime;
                }

            }
            else if (moveInput == 0) // with no vertical input, a slight deacceleration is used to slightly slow down the speed of the car.
            {
                foreach (var wheel in wheels)
                {
                    wheel.wheelCollider.brakeTorque = 300 * noInputDeacceleration;// * Time.deltaTime;
                }
            }
            else // with vertical input, no brake or deacceleration is applied.
            {
                foreach (var wheel in wheels)
                {
                    wheel.wheelCollider.brakeTorque = 0;
                }
            }
        }

        private void EffectCreate(Wheel wheel) // actually creating the effects: 1) trail renderer for the skid, 2) smoke particles, 3) skid sound.
        {
            wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = true;
            wheel.smokeParticle.Emit(1);
            carSounds.PlaySkidSound(wheel.skidSound); // actually setting the volume of the skid to 1
        }
        
        private void AnimateWheelDrift() // checking for every wheel if it's slipping: if yes, the "EffectCreate()" function is called.
        {
            foreach (var wheel in wheels)
            {
                // slipping ---> skid
                WheelHit GroundHit; // variable to store hit data
                wheel.wheelCollider.GetGroundHit(out GroundHit); // store hit data into GroundHit
                float lateralDrift = Mathf.Abs(GroundHit.sidewaysSlip);

                if (Input.GetKey(brakeKey) && wheel.axel == Axel.Rear && wheel.wheelCollider.isGrounded == true && carRb.velocity.magnitude >= brakeDriftingSkidLimit)
                {
                    EffectCreate(wheel);
                }
                else if (wheel.wheelCollider.isGrounded == true && wheel.axel == Axel.Front && (lateralDrift > lateralFrontDriftingSkidLimit)) // drifting: front wheels
                {
                    EffectCreate(wheel);
                }
                else if (wheel.wheelCollider.isGrounded == true && wheel.axel == Axel.Rear && (lateralDrift > lateralRearDriftingSkidLimit)) // drifting: rear wheels
                {
                    EffectCreate(wheel);
                }
                else
                {
                    wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = false;
                    carSounds.StopSkidSound(wheel.skidSound, wheel.index); // actually decreasing the volume of the skid to 0: see the "CarSound" script.
                }
            }
        }
    }
}