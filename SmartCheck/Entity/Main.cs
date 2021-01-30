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
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Frm = System.Windows.Forms;

namespace SmartCheck
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]

    public class Main : IExternalCommand
    {
        public static List<Check> lstChecks;
        public static Document _doc;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Variables necesarias en todos los Comandos Externos
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Application app = uiApp.Application;
            Document doc = uiDoc.Document;

            _doc = doc;
            Result rslt = Result.Cancelled;

            using (TransactionGroup tg = new TransactionGroup(doc, "Checks"))
            {
                try
                {
                    tg.Start();
                    Frm.DialogResult result = (new frmCheck(doc)).ShowDialog();
                    if (result == Frm.DialogResult.OK)
                    {
                        // OK form
                        rslt = Result.Succeeded;
                    }
                    else
                    {
                        // Cancel form
                        rslt = Result.Cancelled;
                    }
                    tg.Assimilate();
                }
                catch (Exception ex)
                {
                    Frm.MessageBox.Show("Error: " + ex.Message, Tools._title, Frm.MessageBoxButtons.OK, Frm.MessageBoxIcon.Error);
                    tg.RollBack();
                }
            };
            return rslt;
        }
    }
}
