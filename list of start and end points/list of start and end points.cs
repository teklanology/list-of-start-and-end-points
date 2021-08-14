using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Tekla.Structures;
using Tekla.Structures.Drawing;
using TSD = Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using TSG = Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using TSM = Tekla.Structures.Model;
using TSMUI = Tekla.Structures.Model.UI;

namespace Tekla.Technology.Akit.UserScript
{
    static class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            Model model = new Model();
            TSM.UI.ModelObjectSelector modelObjectSelector = new TSMUI.ModelObjectSelector();

            ModelObjectEnumerator modelObjectEnumerator = null;

            // option one - will include columns in output
            //modelObjectEnumerator = model.GetModelObjectSelector().GetAllObjectsWithType(TSM.ModelObject.ModelObjectEnum.BEAM);
            
            // option two - based on saved object selection filter
            //modelObjectEnumerator = model.GetModelObjectSelector().GetObjectsByFilterName("BEAM");
            
            // option three - based on current selection in the model
            modelObjectEnumerator = modelObjectSelector.GetSelectedObjects();

            ListPoints(modelObjectEnumerator);
        }

        public static void ListPoints(ModelObjectEnumerator modelObjectEnumerator)
        {
            List<Point> points = new List<Point>();

            while (modelObjectEnumerator.MoveNext())
            {
                if (modelObjectEnumerator.Current is Beam)
                {
                    Beam beam = (Beam)modelObjectEnumerator.Current;

                    if (!points.Contains(beam.StartPoint))
                        points.Add(beam.StartPoint);

                    if (!points.Contains(beam.EndPoint))
                        points.Add(beam.EndPoint);
                }
            }

            foreach (var point in points)
                Console.WriteLine(point);
        }
    }
}
