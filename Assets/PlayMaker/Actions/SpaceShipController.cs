using System;
using UnityEngine;


namespace HutongGames.PlayMaker.Actions
{

    [ActionCategory(ActionCategory.Input)]
    public class SpaceShipController : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(Rigidbody))]
        public FsmOwnerDefault gameObject;

        public enum RotationAxis { Pitch = 0, Yaw = 1, Roll = 2 }

        public RotationAxis axis = RotationAxis.Pitch;

        // Multiplier to use for Pitch(x or Vertical axis), Yaw(y or Horizontal axis), or Roll(z or Horizontal axis)
        // Typical settings might be Pitch = 100, Yaw = 50, or Roll = 30. Experiment

        [RequiredField]
        public FsmFloat axisMultiplier;

        public bool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            everyFrame = true;
            axis = RotationAxis.Pitch;
            axisMultiplier = new FsmFloat { UseVariable = true };

        }

        public override void Awake()
        {
            Fsm.HandleFixedUpdate = true;
        }

        public override void OnEnter()
        {
            DoAddRelativeTorque();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnFixedUpdate()
        {
            DoAddRelativeTorque();
        }

        void DoAddRelativeTorque()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            if (go.rigidbody == null)
            {
                LogWarning("Missing rigid body: " + go.name);
                return;
            }

            switch (axis)
            {
                case RotationAxis.Pitch:
                    go.rigidbody.AddRelativeTorque(new Vector3(Input.GetAxis("Vertical") * axisMultiplier.Value * go.rigidbody.mass, 0, 0));
                    return;

                case RotationAxis.Roll:
                    go.rigidbody.AddRelativeTorque(new Vector3(0, Input.GetAxis("Horizontal") * axisMultiplier.Value * go.rigidbody.mass, 0));
                    return;

                case RotationAxis.Yaw:
                    go.rigidbody.AddRelativeTorque(new Vector3(0, 0, -Input.GetAxis("Horizontal") * axisMultiplier.Value * go.rigidbody.mass));
                    return;
            }

        }
    }

}
