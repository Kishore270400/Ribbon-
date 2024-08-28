using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using System.IO;

namespace PCTO3D
{
    public class PointCloud : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                
                RibbonPanel panel = CreateRibbonPanel(application);
                if (panel != null)
                {
                    string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

                    
                    PushButtonData buttonData = new PushButtonData("PCTO3D", "PCTO3D", thisAssemblyPath, "PCTO3D.Command");
                    PushButton button = panel.AddItem(buttonData) as PushButton;

                  
                    if (button != null)
                    {
                        button.ToolTip = "PCTO3D";

                        Uri uri = new Uri(Path.Combine(Path.GetDirectoryName(thisAssemblyPath), "Resources", "PCTO3D.ico"));
                        BitmapImage bitmap = new BitmapImage(uri);
                        button.LargeImage = bitmap;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during startup: {ex.Message}");
                return Result.Failed;
            }

            return Result.Succeeded;
        }

        private RibbonPanel CreateRibbonPanel(UIControlledApplication application)
        {
            string tabName = "Pointcloud";
            string panelName = "Pointcloud";

            try
            {
                
                application.CreateRibbonTab(tabName);
            }
            catch (Autodesk.Revit.Exceptions.ArgumentException)
            {
               
            }

            RibbonPanel ribbonPanel = null;
            try
            {
               
                ribbonPanel = application.CreateRibbonPanel(tabName, panelName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating ribbon panel: {ex.Message}");
                
                List<RibbonPanel> panels = application.GetRibbonPanels(tabName);
                ribbonPanel = panels.FirstOrDefault(p => p.Name == panelName);
            }

            return ribbonPanel;
        }
    }
}
