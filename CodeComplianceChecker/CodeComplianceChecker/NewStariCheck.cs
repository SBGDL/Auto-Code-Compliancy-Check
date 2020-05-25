using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace CodeComplianceChecker
{
    //[Transaction(TransactionMode.Manual)]
    class CheckAllStairs
    {
        public void CheckMyStairs(Document doc, UIDocument uidoc)
        {
            Stairs stair;
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<ElementId> stairsIds = collector.WhereElementIsElementType().OfCategory(BuiltInCategory.OST_Stairs).ToElementIds();
            List<double> stairWidth = new List<double>();
           
            foreach (var stairId in stairsIds)
            {
                stair = doc.GetElement(stairId) as Stairs;
                
                //Autodesk.Revit.DB.Architecture.Stairs stair ;
                Autodesk.Revit.DB.Architecture.StairsRun sRun = doc.GetElement(stair.GetStairsRuns().First()) as Autodesk.Revit.DB.Architecture.StairsRun;
                double StairWidth = sRun.ActualRunWidth;
                stairWidth.Add(StairWidth);
            }
            
            StreamWriter file = new StreamWriter("Stairs.txt");
            foreach (var item in stairWidth)
            {
                file.WriteLine(item);

            }
        }
    }
}
