using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Magio
{
    /// <summary>
    /// Master for all the effect on one object. Handles interaction and turns.
    /// </summary>
    public class MagioObjectMaster : MonoBehaviour
    {
        private float approxSize = 5;

        public float ApproxSize { get => approxSize; }
        public Vector3 ApproxCenter { get => approxCenter; }

        private Vector3 approxCenter;

        public List<MagioObjectEffect> magioObjects = new List<MagioObjectEffect>();

        private MagioObjectEffect currentEmissionOverlayer;
        private bool firstSetupDone = false;

        private void Start()
        {
            if (!firstSetupDone)
            {
                Setup();
            }
        }

        public void Setup()
        {
            CalculateApproxSize();

            magioObjects.Add(GetComponentInChildren<MagioObjectEffect>());
        }

        /// <summary>
        /// Calculates approx size of object from the renderer bounds.
        /// </summary>
        public void CalculateApproxSize()
        {
            List<Renderer> rends = GetComponentsInChildren<Renderer>().ToList();
            if (rends.Count <= 0)
            {
                approxSize = 1;
                return;
            }

            Bounds combinedRendBounds = rends[0].bounds;

            foreach (Renderer rend in rends)
            {
                if (rend != rends[0]) combinedRendBounds.Encapsulate(rend.bounds);
            }

            approxSize = combinedRendBounds.size.magnitude;
            approxCenter = combinedRendBounds.center;
        }


        public bool IsEmissionOverlayerEffectEnabled()
        {
            if (currentEmissionOverlayer && currentEmissionOverlayer.effectEnabled)
            {
                return true;
            }

            return false;
        }
    }
}

