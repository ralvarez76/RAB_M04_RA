#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

#endregion

namespace RAB_M04_RA
{
    [Transaction(TransactionMode.Manual)]
    public class LevelsAndViews : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // this is a variable for the Revit application
            UIApplication uiapp = commandData.Application;

            // this is a variable for the current Revit model
            Autodesk.Revit.DB.Document doc = uiapp.ActiveUIDocument.Document;

            // Declare a number variable and set it to 250
            double numberOfLevels = 250;

            // Declare a starting elevation variable and set it to 0
            double elevationStarting1 = 0;

            // Declare a floor height variable and set it to 15
            double floorHeight1 = 15;

            // create an empty list to store the level numbers divisible by 3
            List<double> levelNumbersDiv3 = new List<double>();

            // create an empty list to store the level numbers divisible by 5
            List<double> levelNumbersDiv5 = new List<double>();

            // create an empty list to store the level numbers divisible by 3 and 5
            List<double> levelNumbersDiv3And5 = new List<double>();

            // create an empty list to store the level numbers divisible by 3 and 5
            List<double> levelNumbersNotDiv = new List<double>();

            // Loop through the number 1 to the number variable to calculate level numbers
            for (double i = 1; i <= numberOfLevels; i++)
            {
                if ((i % 3 == 0) && !(i % 5 == 0))
                {
                    levelNumbersDiv3.Add(i);
                }
                else if ((i % 5 == 0) && !(i % 3 == 0))
                {
                    levelNumbersDiv5.Add(i);
                }
                else if ((i % 3 == 0) && (i % 5 == 0))
                {
                    levelNumbersDiv3And5.Add(i);
                }
                else
                {
                    levelNumbersNotDiv.Add(i);
                }
            }

            FilteredElementCollector planCollector = new FilteredElementCollector(doc);
            planCollector.OfClass(typeof(ViewFamilyType));

            ViewFamilyType floorPlanViewFamilyTypes = null;

            foreach (ViewFamilyType ViewFamilyTypes01 in planCollector)
            {
                if (ViewFamilyTypes01.ViewFamily == ViewFamily.FloorPlan)
                {
                    floorPlanViewFamilyTypes = ViewFamilyTypes01;
                    break;
                }
            }

            List<ViewFamilyType> floorPlanViewFamilyTypes01 = new List<ViewFamilyType> { floorPlanViewFamilyTypes };
                        
            ViewFamilyType ceilingPlanViewFamilyTypes = null;

            foreach (ViewFamilyType ViewFamilyTypes02 in planCollector)
            {
                if (ViewFamilyTypes02.ViewFamily == ViewFamily.CeilingPlan)
                {
                    ceilingPlanViewFamilyTypes = ViewFamilyTypes02;
                    break;
                }
            }

            List<ViewFamilyType> ceilingPlanViewFamilyTypes01 = new List<ViewFamilyType> { ceilingPlanViewFamilyTypes };

            // level creation transaction
            Transaction createLevelsAndViews1 = new Transaction(doc);
            createLevelsAndViews1.Start("Creating Levels and Views divisible by 3");

            // Loop through the level numbers that are divisible by 3 and create the levels
            foreach (double levelsDiv3 in levelNumbersDiv3)
            {
                // Create a level for each number divisible by 3
                Level newLevelsDiv3 = Level.Create(doc, (levelsDiv3 * floorHeight1) + elevationStarting1);
                newLevelsDiv3.Name = "LEVEL" + " " + levelsDiv3.ToString();

                ViewFamilyType floorPlanFamType = floorPlanViewFamilyTypes01[0];

                ViewPlan newFloorPlan = ViewPlan.Create(doc, floorPlanFamType.Id, newLevelsDiv3.Id);
                string strFloorPlan = newFloorPlan.Name;
                newFloorPlan.Name = "FIZZ_" + strFloorPlan.Remove(0, 5);              
            }

            createLevelsAndViews1.Commit();
            createLevelsAndViews1.Dispose();

            // level creation transaction
            Transaction createLevelsAndViews2 = new Transaction(doc);
            createLevelsAndViews2.Start("Creating Levels and Views divisible by 5");

            // Loop through the level numbers that are divisible by 5 and create the levels
            foreach (double levelsDiv5 in levelNumbersDiv5)
            {
                // Create a level for each number divisible by 5
                Level newLevelsDiv5 = Level.Create(doc, (levelsDiv5 * floorHeight1) + elevationStarting1);
                newLevelsDiv5.Name = "LEVEL" + " " + levelsDiv5.ToString();
                 
                ViewFamilyType ceilingPlanFamType = ceilingPlanViewFamilyTypes01[0];

                ViewPlan newCeilingPlan = ViewPlan.Create(doc, ceilingPlanFamType.Id, newLevelsDiv5.Id);
                string strCeilingPlan = newCeilingPlan.Name;
                newCeilingPlan.Name = "BUZZ_" + strCeilingPlan.Remove(0, 5);              
            }

            createLevelsAndViews2.Commit();
            createLevelsAndViews2.Dispose();

            // level creation transaction
            Transaction createLevelsAndViews3 = new Transaction(doc);
            createLevelsAndViews3.Start("Creating Levels and Views divisible by 3 and 5");

            // Loop through the level numbers that are divisible by 3 and 5 and create the levels
            foreach (double levelsDiv3And5 in levelNumbersDiv3And5)
            {
                // Create a level for each number divisible by 3 and 5
                Level newLevelsDiv3And5 = Level.Create(doc, (levelsDiv3And5 * floorHeight1) + elevationStarting1);
                newLevelsDiv3And5.Name = "LEVEL" + " " + levelsDiv3And5.ToString();

                ViewFamilyType floorPlanFamType = floorPlanViewFamilyTypes01[0];

                ViewPlan newFloorPlan = ViewPlan.Create(doc, floorPlanFamType.Id, newLevelsDiv3And5.Id);
                string strFloorPlan = newFloorPlan.Name;
                newFloorPlan.Name = "FIZZBUZZ_" + strFloorPlan.Remove(0, 5);

                // Get an available title block from document
                FilteredElementCollector titleBlockCollector = new FilteredElementCollector(doc);
                titleBlockCollector.OfClass(typeof(FamilySymbol));
                titleBlockCollector.OfCategory(BuiltInCategory.OST_TitleBlocks);

                FamilySymbol titleBlockFamSymbol = titleBlockCollector.FirstElement() as FamilySymbol;
                
                // Create a sheet view
                ViewSheet newSheetForDiv3And5 = ViewSheet.Create(doc, titleBlockFamSymbol.Id);
                newSheetForDiv3And5.Name = "FIZZBUZZ_" + newLevelsDiv3And5.Name.Remove(0, 5);
                newSheetForDiv3And5.SheetNumber = "FB-" + newLevelsDiv3And5.Name.Remove(0, 5);

                XYZ viewportInsPoint = new XYZ(1.5625, 1.25, 0);

                // Create viewport
                Viewport newViewPort = Viewport.Create(doc, newSheetForDiv3And5.Id, newFloorPlan.Id, viewportInsPoint);

            }

            createLevelsAndViews3.Commit();
            createLevelsAndViews3.Dispose();

            // level creation transaction
            Transaction createLevelsAndViews4 = new Transaction(doc);
            createLevelsAndViews4.Start("Creating Levels and Views not divisible");

            // Loop through the level numbers that are not divisible by 3 and/or 5 and create the levels
            foreach (double levelsNotDiv in levelNumbersNotDiv)
            {
                // Create a level for each number divisible by 3 and 5
                Level newLevelsNotDiv = Level.Create(doc, (levelsNotDiv * floorHeight1) + elevationStarting1);
                newLevelsNotDiv.Name = "LEVEL" + " " + levelsNotDiv.ToString();
            }

            createLevelsAndViews4.Commit();
            createLevelsAndViews4.Dispose();

            return Result.Succeeded;
        }
        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnCommand1";
            string buttonTitle = "Button 1";

            ButtonDataClass myButtonData1 = new ButtonDataClass(
                buttonInternalName,
                buttonTitle,
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                Properties.Resources.Blue_32,
                Properties.Resources.Blue_16,
                "This is a tooltip for Button 1");

            return myButtonData1.Data;
        }
        public static String GetMethod()
        {
            var method = MethodBase.GetCurrentMethod().DeclaringType?.FullName;
            return method;
        }
    }
}
