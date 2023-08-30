#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Windows.Markup;

#endregion

namespace RAB_M04_RA
{
    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication app)
        {
            // 1. Create ribbon tab
            string tabName = "Revit Add-in Bootcamp";
            try
            {
                app.CreateRibbonTab(tabName);
            }
            catch (Exception)
            {
                Debug.Print("Tab already exists.");
            }

            // 2. Create ribbon panel 
            RibbonPanel panel = Utils.CreateRibbonPanel(app, tabName, "Revit Tools");
            RibbonPanel panel2 = app.CreateRibbonPanel("More Revit Tools");

            // 3. Create button data instances
            ButtonDataClass myButtonData1 = new ButtonDataClass("btn1", "Curves To Elements", ConvertCurvesToElements.GetMethod(), Properties.Resources.ConvertCurvesToElements_32, Properties.Resources.ConvertCurvesToElements_16, "Converts selected curves to elements");
            ButtonDataClass myButtonData2 = new ButtonDataClass("btn2", "Levels And Views", LevelsAndViews.GetMethod(), Properties.Resources.LevelsAndViews_32, Properties.Resources.LevelsAndViews_16, "Creates levels and views");

            ButtonDataClass myButtonData3 = new ButtonDataClass("btn3", "Curves To Elements", ConvertCurvesToElements.GetMethod(), Properties.Resources.ConvertCurvesToElements_32, Properties.Resources.ConvertCurvesToElements_16, "Converts selected curves to elements");
            ButtonDataClass myButtonData4 = new ButtonDataClass("btn4", "Levels And Views", LevelsAndViews.GetMethod(), Properties.Resources.LevelsAndViews_32, Properties.Resources.LevelsAndViews_16, "Creates levels and views");
            ButtonDataClass myButtonData5 = new ButtonDataClass("btn5", "Moving Day", MovingDay.GetMethod(), Properties.Resources.MovingDay_32, Properties.Resources.MovingDay_16, "Populates rooms with furniture");
            
            ButtonDataClass myButtonData6 = new ButtonDataClass("btn6", "Curves To Elements", ConvertCurvesToElements.GetMethod(), Properties.Resources.ConvertCurvesToElements_32, Properties.Resources.ConvertCurvesToElements_16, "Converts selected curves to elements");
            ButtonDataClass myButtonData7 = new ButtonDataClass("btn7", "Levels And Views", LevelsAndViews.GetMethod(), Properties.Resources.LevelsAndViews_32, Properties.Resources.LevelsAndViews_16, "Creates levels and views");
            
            ButtonDataClass myButtonData8 = new ButtonDataClass("btn8", "Curves To Elements", ConvertCurvesToElements.GetMethod(), Properties.Resources.ConvertCurvesToElements_32, Properties.Resources.ConvertCurvesToElements_16, "Converts selected curves to elements");
            ButtonDataClass myButtonData9 = new ButtonDataClass("btn9", "Levels And Views", LevelsAndViews.GetMethod(), Properties.Resources.LevelsAndViews_32, Properties.Resources.LevelsAndViews_16, "Creates levels and views");
            ButtonDataClass myButtonData10 = new ButtonDataClass("btn10", "Moving Day", MovingDay.GetMethod(), Properties.Resources.MovingDay_32, Properties.Resources.MovingDay_16, "Populates rooms with furniture");

            // 4. Create buttons
            PushButton myButton1 = panel.AddItem(myButtonData1.Data) as PushButton;
            PushButton myButton2 = panel.AddItem(myButtonData2.Data) as PushButton;

            // 5. Create a stacked button
            panel.AddStackedItems(myButtonData3.Data, myButtonData4.Data, myButtonData5.Data);

            // 6. Create split button - doesn't need an image since it splits
            SplitButtonData splitButtonData = new SplitButtonData("split1", "Split/rButton");
            SplitButton splitButton = panel.AddItem(splitButtonData) as SplitButton;
            splitButton.AddPushButton(myButtonData6.Data);
            splitButton.AddPushButton(myButtonData7.Data);

            // 7. Create pulldown button
            PulldownButtonData pullDownData = new PulldownButtonData("pulldown1", "More Tools");
            pullDownData.LargeImage = ButtonDataClass.BitmapToImageSource(Properties.Resources.MoreTools_32);
            pullDownData.Image = ButtonDataClass.BitmapToImageSource(Properties.Resources.MoreTools_16);

            PulldownButton pulldownButton = panel.AddItem(pullDownData) as PulldownButton;
            pulldownButton.AddPushButton(myButtonData8.Data);
            pulldownButton.AddPushButton(myButtonData9.Data);
            pulldownButton.AddPushButton(myButtonData10.Data);

            // NOTE:
            // To create a new tool, copy lines 35 and 39 and rename the variables to "btnData3" and "myButton3". 
            // Change the name of the tool in the arguments of line 

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }


    }
}
