/*   SmartCheck License
*******************************************************************************
*                                                                             *
*    Copyright (c) 2021 Luciano Gorosito <lucianogorosito@hotmail.com>        *
*                                                                             *
*    This file is part of SmartCheck                                          *
*                                                                             *
*    SmartCheck is free software: you can redistribute it and/or modify       *
*    it under the terms of the GNU General Public License as published by     *
*    the Free Software Foundation, either version 3 of the License, or        *
*    (at your option) any later version.                                      *
*                                                                             *
*    SmartCheck is distributed in the hope that it will be useful,            *
*    but WITHOUT ANY WARRANTY; without even the implied warranty of           *
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the            *
*    GNU General Public License for more details.                             *
*                                                                             *
*    You should have received a copy of the GNU General Public License        *
*    along with this program.  If not, see <https://www.gnu.org/licenses/>.   *
*                                                                             *
*******************************************************************************
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace SmartCheck
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]

    public class App : IExternalApplication
    {
        // Obtener la ruta de la Addin actual
        public static string ExecutingAssemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

        public Result OnStartup(UIControlledApplication application)
        {
            // Agregar el Panel
            RibbonPanel ribbonPanel = application.CreateRibbonPanel("SmartCheck");

            // Agregar un botón
            PushButton button1 = ribbonPanel.AddItem(new PushButtonData("Boton1",
            "Run Checks", ExecutingAssemblyPath, "SmartCheck.Main")) as PushButton;

            // Establecer la imagen del Botón
            button1.LargeImage = new BitmapImage(
                new Uri("pack://application:,,,/SmartCheck;component/Resources/UniBim32.png"));
            button1.ToolTip = "Run a complete test to the model";
            
            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
